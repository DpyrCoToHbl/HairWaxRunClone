using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LevelCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private SceneLoader _sceneLoader;

    private int _currentLevelNumber = 1;

    public int CurrentLevelNumber => _currentLevelNumber;

    private void Start()
    {
        SaveSystem.LoadLevel();
        ShowLevelNumber();
    }

    private void OnEnable()
    {
        _sceneLoader.NextButtonClicked += OnNextButtonClicked;
    }

    private void OnDisable()
    {
        _sceneLoader.NextButtonClicked -= OnNextButtonClicked;
    }

    private void OnNextButtonClicked()
    {
        _currentLevelNumber++;
        SaveSystem.SaveLevel(this);
        ShowLevelNumber();
    }

    private void ShowLevelNumber()
    {
        _currentLevelText.text = $"Level {_currentLevelNumber}";
    }

}
