using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _continueButton;

    private int _currentSceneIndex;
    private int _nextSceneIndex;

    public event UnityAction NextButtonClicked;

    private void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void OnNextButtonClick()
    {
        NextButtonClicked?.Invoke();

        do
        {
            _nextSceneIndex = GetRandomLevel();
        }
        while (_nextSceneIndex == _currentSceneIndex);

        SceneManager.LoadScene(_nextSceneIndex);
    }

    public void OnContinueButtonClick()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }

    private int GetRandomLevel()
    {
        List<int> scenes = new List<int> { SceneIndexHolder.FirstLevel, SceneIndexHolder.SecondLevel, SceneIndexHolder.ThirdLevel };
        int sceneIndex = scenes[Random.Range(0, scenes.Count - 1)];
        return sceneIndex;
    }
}
