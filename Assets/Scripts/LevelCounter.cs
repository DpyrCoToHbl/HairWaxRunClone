using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private SaveSystem _saveSystem;

    private int _currentLevelNumber = 1;

    public event UnityAction<int> LevelNumberChanged;
    private void Start()
    {
        _saveSystem.Load();
        ShowLevelNumber();
    }

    private void OnEnable()
    {
        _sceneLoader.NextButtonClicked += OnNextButtonClicked;
        _saveSystem.Loaded += OnLoaded;
    }

    private void OnDisable()
    {
        _saveSystem.Loaded -= OnLoaded;
        _sceneLoader.NextButtonClicked += OnNextButtonClicked;
    }

    private void OnLoaded(int levelNumber, int sceneIndex)
    {
        _currentLevelNumber = levelNumber;
        ShowLevelNumber();
    }

    private void ShowLevelNumber()
    {
        _currentLevelText.text = $"Level {_currentLevelNumber}";
    }

    private void OnNextButtonClicked()
    {
        _currentLevelNumber++;
        LevelNumberChanged?.Invoke(_currentLevelNumber);
    }

}
