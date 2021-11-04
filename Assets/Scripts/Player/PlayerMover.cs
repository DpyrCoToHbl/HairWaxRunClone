using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BodyHairCounter _bodyHairCounter;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _path;
    [SerializeField] private LayerMask _groundLayer;

    private Transform _target;
    private Transform[] _checkpoints;
    private Rigidbody _rigidbody;
    private int _currentCheckpointIndex;
    private int _defaultSpeed = 7;
    private int _angle = 60;
    private float _rotationSpeed = 5;
    private float _strafeSpeed = 2;
    private float _jumpForce = 18;
    private bool _isMoving;
    private bool _isGrounded;
    private bool _isRotationLocked;
    private bool _isMovementAllowed;

    public bool IsCheckpointReached { get; private set; }

    public event UnityAction CheckpointReached;
    public event UnityAction Moving;
    public event UnityAction Grounded;
    public event UnityAction Floating;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _isMovementAllowed = true;
        _speed = _defaultSpeed;
        _checkpoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _checkpoints[i] = _path.GetChild(i);
        }

        _target = _checkpoints[_currentCheckpointIndex];
    }

    private void Update()
    {
        CheckGround();
    }


    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<JumpPad>())
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        if (other.gameObject.GetComponent<Checkpoint>())
        {
            _currentCheckpointIndex++;
            _target = GetNextCheckpoint();
            IsCheckpointReached = true;
            CheckpointReached?.Invoke();
            StartCoroutine(SwitchReachedTrigget());
        }
    }

    private Transform GetNextCheckpoint()
    {
        if (_currentCheckpointIndex < _checkpoints.Length)
            return _checkpoints[_currentCheckpointIndex].transform;

        return null;
    }

    private void Move()
    {
        if (!_isMoving && Input.GetKey(KeyCode.Mouse0) && _isMovementAllowed)
        {
            _isMoving = true;
            Moving?.Invoke();
        }

        if (_isMoving)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

            if (!_isRotationLocked)
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.rotation *= Quaternion.AngleAxis(-_angle * Time.deltaTime, Vector3.up);
                    transform.Translate(Vector3.left * _strafeSpeed * Time.deltaTime);
                }

                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.rotation *= Quaternion.AngleAxis(_angle * Time.deltaTime, Vector3.up);
                    transform.Translate(Vector3.right * _strafeSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void Rotate()
    {
        if (IsCheckpointReached)
        {
            var direction = _target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }

        if (_player.IsFinished)
        {
            Quaternion rotationY = Quaternion.AngleAxis(0.5f, Vector3.up);
            transform.rotation *= rotationY;
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics.CheckBox(transform.position, new Vector3(1, 1, 1), Quaternion.identity, _groundLayer, QueryTriggerInteraction.Ignore);

        if (!_isGrounded)
            Floating?.Invoke();

        if (_isGrounded)
            Grounded?.Invoke();
    }

    private void DisableMovement()
    {
        _isMoving = false;
    }

    private void OnEnable()
    {
        _player.FinishLineReached += OnFinishLineReached;
        _player.Lose += OnLose;
        _player.SteppedOnDuctTape += OnMatReached;
        _player.GotOffDuctTape += OnGotOffDuctTape;
    }

    private void OnDisable()
    {
        _player.FinishLineReached -= OnFinishLineReached;
        _player.Lose -= OnLose;
        _player.SteppedOnDuctTape -= OnMatReached;
        _player.GotOffDuctTape -= OnGotOffDuctTape;
    }

    private void OnFinishLineReached()
    {
        DisableMovement();
        ProhibitMovement();
    }

    private void OnMatReached(GameObject ductTape, string name)
    {
        _isRotationLocked = true;
        StartCoroutine(ChangeSpeed());
        transform.rotation = Quaternion.LookRotation(ductTape.gameObject.transform.forward);
    }

    private void OnGotOffDuctTape()
    {
        StartCoroutine(ChangeSpeed());
        _isRotationLocked = false;
    }

    private void OnLose()
    {
        DisableMovement();
        ProhibitMovement();
    }

    private IEnumerator SwitchReachedTrigget()
    {
        yield return new WaitForSeconds(1);
        IsCheckpointReached = false;
    }

    private IEnumerator ChangeSpeed()
    {
        _speed = 0;
        DisableMovement();
        yield return new WaitForSeconds(1.8f);
        _speed = _defaultSpeed;
        _isMoving = true;
    }

    private void ProhibitMovement()
    {
        _isMovementAllowed = false;
    }
}