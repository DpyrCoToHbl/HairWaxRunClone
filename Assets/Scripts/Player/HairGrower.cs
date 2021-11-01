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
    private int _difference;
    private int _lasstBodyHairQuantity;

    public int GrowingPlacesCount => _growingPlaces.Count;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _bodyHairCounter = GetComponent<BodyHairCounter>();
    }

    private void Start()
    {
        _difference = _bodyHairCounter.MaxHairQuantity - _bodyHairCounter.HairCount;

        foreach (var place in _growingPlaces)
        {
            Instantiate(_bodyHairPrefab, place.transform);
        }

        for (int i = 0; i < _difference; i++)
        {
            TryRemoveHair();
        }

        _bodyHairCounter.HairCountChanged += OnHairCountChanged;
        EqualizeQuantity();
        Debug.Log(_lasstBodyHairQuantity);
    }

    public void TryAddHair()
    {
        if (_bodyHairCounter.HairCount != _bodyHairCounter.MaxHairQuantity)
        {
            var inactiveHairBlock = _growingPlaces.FirstOrDefault(hairBlock => hairBlock.activeSelf == false);
            inactiveHairBlock.SetActive(true);
        }
    }

    public void TryRemoveHair()
    {
        if (_bodyHairCounter.HairCount >= 0)
        {
            var activeHairBlock = _growingPlaces.FirstOrDefault(hairBlock => hairBlock.activeSelf == true);
            activeHairBlock.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _player.NegativeItemCollected += OnNegativeItemCollected;
        _player.PositiveItemCollected += OnPositiveItemCollected;
    }

    private void OnDisable()
    {
        _player.NegativeItemCollected -= OnNegativeItemCollected;
        _player.PositiveItemCollected -= OnPositiveItemCollected;
        _bodyHairCounter.HairCountChanged -= OnHairCountChanged;
    }

    private void OnNegativeItemCollected()
    {
        //TryAddHair();
    }

    private void OnPositiveItemCollected()
    {
        //TryRemoveHair();
    }

    private void OnHairCountChanged()
    {
        //Debug.Log($"_lasstBodyHairQuantity - {_bodyHairCounter.HairCount}");

        if (_bodyHairCounter.HairCount < _lasstBodyHairQuantity)
        {
            TryRemoveHair();
        }

        if (_bodyHairCounter.HairCount > _lasstBodyHairQuantity)
        {
            TryAddHair();
        }

        EqualizeQuantity();
        //Debug.Log($"_lasstBodyHairQuantity - { _lasstBodyHairQuantity}");

    }

    private void EqualizeQuantity()
    {
        _lasstBodyHairQuantity = _bodyHairCounter.HairCount;
    }
}
