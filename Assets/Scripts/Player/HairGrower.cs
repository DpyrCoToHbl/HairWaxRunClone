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
    [SerializeField] private DuctTapeHairHolder _ductTapeHairHolder;

    private List<DuctTapeHair> _hairs;
    private Player _player;
    private BodyHairCounter _bodyHairCounter;
    private int _hairCount;
    private int _maxHairQuantity;
    private int _difference;

    public int GrowingPlacesCount => _growingPlaces.Count;

    private void Awake()
    {
        _hairs = _ductTapeHairHolder.GetHairsList();
        _player = GetComponent<Player>();
        _bodyHairCounter = GetComponent<BodyHairCounter>();
    }

    private void Start()
    {
        _hairCount = _bodyHairCounter.HairCount;
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
        if (_hairCount > 0)
        {
            var activeHairBlock = _growingPlaces.FirstOrDefault(hairBlock => hairBlock.activeSelf == true);
            activeHairBlock.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _player.NegativeItemCollected += OnNegativeItemCollected;
        _player.PositiveItemCollected += OnPositiveItemCollected;
        _bodyHairCounter.HairCountChanged += OnHairCountChanged;

        foreach (var hair in _hairs)
        {
            hair.HairStucked += OnHairStucked;
            hair.HairFelled += OnHairFelled;
        }
    }

    private void OnDisable()
    {
        _player.NegativeItemCollected -= OnNegativeItemCollected;
        _player.PositiveItemCollected -= OnPositiveItemCollected;

        foreach (var hair in _hairs)
        {
            hair.HairStucked -= OnHairStucked;
            hair.HairFelled -= OnHairFelled;
        }
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

    private void OnHairCountChanged()
    {
        _hairCount = _bodyHairCounter.HairCount;
    }
}
