using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _continueButton;

    private int _currentSceneIndex;
    private int _nextSceneIndex;

    public event UnityAction NextButtonClicked;
    public event UnityAction<int> SceneChanged;

    private void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        _saveSystem.Loaded += OnLoaded;
    }

    private void OnDisable()
    {
        _saveSystem.Loaded += OnLoaded;
    }

    public void OnNextButtonClick()
    {
        _nextSceneIndex = GetRandomLevel();
        SceneChanged?.Invoke(_nextSceneIndex);
        NextButtonClicked?.Invoke();
        Load(_nextSceneIndex);
    }

    public void OnContinueButtonClick()
    {
        Load(_currentSceneIndex);
    }

    private void OnLoaded(int levelNumber, int sceneIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneIndex)
        {
            Load(sceneIndex);
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }

    private int GetRandomLevel()
    {
        List<int> scenes = new List<int> { SceneIndexHolder.FirstLevel, SceneIndexHolder.SecondLevel, SceneIndexHolder.ThirdLevel };
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndex++;

        if (sceneIndex > scenes.Count - 1)
            sceneIndex = 0;

        return sceneIndex;
    }

    private void Load(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
