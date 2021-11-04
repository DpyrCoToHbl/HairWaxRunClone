using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(HairGrower))]
public class BodyHairCounter : MonoBehaviour
{
    [SerializeField] private int _hairCount;
    [SerializeField] private DuctTapeHairHolder _ductTapeHairHolder;
    
    private Player _player;
    private List<DuctTapeHair> _hairs;
    private HairGrower _hairGrower;
    public int MaxHairQuantity;

    public int HairCount => _hairCount;

    public event UnityAction HairCountChanged;

    private void Awake()
    {
        _hairs = _ductTapeHairHolder.GetHairsList();
        _player = GetComponent<Player>();
        _hairGrower = GetComponent<HairGrower>();
        MaxHairQuantity = _hairGrower.GrowingPlacesCount;
        _hairCount = 23;
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
        if (_hairCount > 0)
            _hairCount--;

        HairCountChanged?.Invoke();
    }

    private void OnEnable()
    {
        _player.PositiveItemCollected += OnPositiveItemCollected;
        _player.NegativeItemCollected += OnNegativeItemCollected;

        foreach (var hair in _hairs)
        {
            hair.HairStucked += OnHairStucked;
            hair.HairFelled += OnHairFelled;
        }
    }

    private void OnDisable()
    {
        _player.PositiveItemCollected -= OnPositiveItemCollected;
        _player.NegativeItemCollected -= OnNegativeItemCollected;

        foreach (var hair in _hairs)
        {
            hair.HairStucked -= OnHairStucked;
            hair.HairFelled -= OnHairFelled;
        }
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
