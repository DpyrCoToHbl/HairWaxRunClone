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

    public event UnityAction NextButtonClicked;

    public void OnNextButtonClick()
    {
        NextButtonClicked?.Invoke();
        SceneManager.LoadScene(GetRandomLevel());
    }

    public void OnContinueButtonClick()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    private int GetRandomLevel()
    {
        List<int> scenes = new List<int> { SceneIndexHolder.FirstLevel, SceneIndexHolder.SecondLevel, SceneIndexHolder.ThirdLevel };
        int sceneIndex = scenes[Random.Range(0, scenes.Count -1)];

        return sceneIndex;
    }
}
