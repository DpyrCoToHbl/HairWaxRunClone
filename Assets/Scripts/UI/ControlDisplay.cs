using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _controlImage;
    [SerializeField] private PlayerMover _playerMover;

    private void OnEnable()
    {
        _playerMover.Moving += OnMoving;
    }

    private void OnDisable()
    {
        _playerMover.Moving -= OnMoving;
    }

    private void OnMoving()
    {
        _controlImage.SetActive(false);
    }
}
