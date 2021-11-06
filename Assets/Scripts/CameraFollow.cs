using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;

    private Vector3 _offset = new Vector3(0, -6, 10);
    private Vector3 _targetCurrentEulerAngles;
    private float _targetCurrentEulerAngleY;
    private const float _speed = 7;
    private const float _viewCorrective = 5;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _targetCurrentEulerAngles = _target.transform.localEulerAngles;
        _targetCurrentEulerAngleY = _targetCurrentEulerAngles.y;
        Vector3 pointView = new Vector3(_target.position.x, _target.position.y + _viewCorrective, _target.position.z);
        transform.LookAt(pointView);
        transform.position = Vector3.Slerp(transform.position, _target.position - _offset, _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, _target.position);
    }

    private void OnEnable()
    {
        _player.Lose += OnLose;
        _playerMover.CheckpointReached += OnCheckpointReached;
    }

    private void OnDisable()
    {
        _player.Lose -= OnLose;
        _playerMover.CheckpointReached -= OnCheckpointReached;
    }

    private void OnLose()
    {
        StartCoroutine(ChangeOffsetvalue());
    }

    private IEnumerator ChangeOffsetvalue()
    {
        Vector3 newOffset = new Vector3(0, -15, -7);
        float delay = 1.5f;
        yield return new WaitForSeconds(delay);
        _offset = newOffset;
    }

    private void OnCheckpointReached()
    {
        StartCoroutine(RotateView());
    }

    private IEnumerator RotateView()
    {
        float delay = 0.1f;
        float targetLeftRotationMinEulerAnglesY = 260;
        float targetLeftRotationMaxEulerAnglesY = 360;
        float targetRightRotationMinEulerAnglesY = 20;
        float targetRightRotationMaxEulerAnglesY = 70;

        yield return new WaitForSeconds(delay);

        if (_targetCurrentEulerAngleY == Mathf.Clamp(_targetCurrentEulerAngleY, targetLeftRotationMinEulerAnglesY, targetLeftRotationMaxEulerAnglesY))
            SwitchOffset(_offset.x*-1, _offset.z*-1);

        if (_targetCurrentEulerAngleY == Mathf.Clamp(_targetCurrentEulerAngleY, targetRightRotationMinEulerAnglesY, targetRightRotationMaxEulerAnglesY))
            SwitchOffset(_offset.x, _offset.z);
    }

    private void SwitchOffset(float offsetX, float offsetZ)
    {
        var tempOffset = offsetX;
        _offset.x = offsetZ;
        _offset.z = tempOffset;
    }
}
