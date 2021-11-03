using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(HairGrower))]
public class BodyHairCounter : MonoBehaviour
{
    [SerializeField] private int _hairCount;

    private Player _player;
    private HairGrower _hairGrower;
    public int MaxHairQuantity;

    public int HairCount => _hairCount;

    public event UnityAction HairCountChanged;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _hairGrower = GetComponent<HairGrower>();
        MaxHairQuantity = _hairGrower.GrowingPlacesCount;
        _hairCount = 23;
        HairCountChanged?.Invoke();
    }

    private void Start()
    {
        HairCountChanged?.Invoke();
    }

    private void Increase()
    {
        _hairCount++;
        HairCountChanged?.Invoke();

        if (_hairCount == MaxHairQuantity)
        {
            _player.Die();
        }
    }

    private void Decrease()
    {
        _hairCount--;

        if (_hairCount < 1)
            _hairCount = 0;

        HairCountChanged?.Invoke();
    }

    private void OnEnable()
    {
        _player.PositiveItemCollected += OnPositiveItemCollected;
        _player.NegativeItemCollected += OnNegativeItemCollected;
        DuctTapeHair.HairStucked += OnHairStucked;
        DuctTapeHair.HairFelled += OnHairFelled;
    }

    private void OnDisable()
    {
        _player.PositiveItemCollected -= OnPositiveItemCollected;
        _player.NegativeItemCollected -= OnNegativeItemCollected;
        DuctTapeHair.HairStucked -= OnHairStucked;
        DuctTapeHair.HairFelled -= OnHairFelled;
    }

    private void OnPositiveItemCollected()
    {
        Decrease();
    }

    private void OnNegativeItemCollected()
    {
        Increase();
    }

    private void OnHairStucked()
    {
        Increase();
    }

    private void OnHairFelled()
    {
        Decrease();
    }
}
