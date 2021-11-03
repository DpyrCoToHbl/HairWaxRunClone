using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BodyHairCounter))]
public class HairGrower : MonoBehaviour
{
    [SerializeField] private List<GameObject> _growingPlaces;
    [SerializeField] private GameObject _bodyHairPrefab;

    private Player _player;
    private BodyHairCounter _bodyHairCounter;
    private int _maxHairQuantity;
    private int _difference;

    public int GrowingPlacesCount => _growingPlaces.Count;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _bodyHairCounter = GetComponent<BodyHairCounter>();
    }

    private void Start()
    {
        _maxHairQuantity = _bodyHairCounter.MaxHairQuantity;
        _difference = _bodyHairCounter.MaxHairQuantity - _bodyHairCounter.HairCount;

        foreach (var place in _growingPlaces)
        {
            Instantiate(_bodyHairPrefab, place.transform);
        }

        for (int i = 0; i < _difference; i++)
        {
            RemoveHair();
        }
    }

    private void AddHair()
    {
        if (_bodyHairCounter.HairCount < _maxHairQuantity)
        {
            var inactiveHairBlock = _growingPlaces.FirstOrDefault(hairBlock => hairBlock.activeSelf == false);
            inactiveHairBlock.SetActive(true);
        }
    }

    private void RemoveHair()
    {
        Debug.Log($"{_bodyHairCounter.HairCount}");
        if (_bodyHairCounter.HairCount != 0)
        {
            var activeHairBlock = _growingPlaces.FirstOrDefault(hairBlock => hairBlock.activeSelf == true);
            activeHairBlock.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _player.NegativeItemCollected += OnNegativeItemCollected;
        _player.PositiveItemCollected += OnPositiveItemCollected;
        DuctTapeHair.HairStucked += OnHairStucked;
        DuctTapeHair.HairFelled += OnHairFelled;
    }

    private void OnDisable()
    {
        _player.NegativeItemCollected -= OnNegativeItemCollected;
        _player.PositiveItemCollected -= OnPositiveItemCollected;
        DuctTapeHair.HairStucked -= OnHairStucked;
        DuctTapeHair.HairFelled -= OnHairFelled;
    }

    private void OnNegativeItemCollected()
    {
        AddHair();
    }

    private void OnPositiveItemCollected()
    {
        RemoveHair();
    }

    private void OnHairStucked()
    {
        AddHair();
    }

    private void OnHairFelled()
    {
        RemoveHair();
    }
}
