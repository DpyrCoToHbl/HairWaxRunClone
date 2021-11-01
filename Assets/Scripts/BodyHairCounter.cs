using System.Collections;
using System.Collections.Generic;
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

    public void AddHair()
    {
        _hairCount++;
        HairCountChanged?.Invoke();

        if (_hairCount == MaxHairQuantity)
        {
            _player.Die();
        }
    }

    public void RemoveHair()
    {

        if (_hairCount != 0)
            _hairCount--;

        HairCountChanged?.Invoke();
    }

    private void OnEnable()
    {
        _player.PositiveItemCollected += OnPositiveItemCollected;
        _player.NegativeItemCollected += OnNegativeItemCollected;
    }

    private void OnDisable()
    {
        _player.PositiveItemCollected -= OnPositiveItemCollected;
        _player.NegativeItemCollected -= OnNegativeItemCollected;
    }

    private void OnPositiveItemCollected()
    {
        RemoveHair();
    }

    private void OnNegativeItemCollected()
    {
        AddHair();
    }
}
