using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private LevelCounter _levelCounter;
    [SerializeField] private SceneLoader _sceneLoader;

    private int _levelCount;
    private int _sceneIndex;
    private const string LevelCountKey = "levelCount";
    private const string SceneIndexKey = "sceneIndex";

    public event UnityAction<int, int> Loaded;

    public void Save()
    {
        PlayerPrefs.SetInt(LevelCountKey, _levelCount);
        PlayerPrefs.SetInt(SceneIndexKey, _sceneIndex);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(LevelCountKey) && PlayerPrefs.HasKey(SceneIndexKey))
        {
            _levelCount = PlayerPrefs.GetInt(LevelCountKey);
            _sceneIndex = PlayerPrefs.GetInt(SceneIndexKey);
            Loaded?.Invoke(_levelCount, _sceneIndex);
        }
    }

    private void OnEnable()
    {
        _levelCounter.LevelNumberChanged += OnLevelNumberrChanged;
        _sceneLoader.SceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        _levelCounter.LevelNumberChanged -= OnLevelNumberrChanged;
        _sceneLoader.SceneChanged -= OnSceneChanged;
    }

    private void OnLevelNumberrChanged(int levelCount)
    {
        _levelCount = levelCount;
        Save();
    }

    private void OnSceneChanged(int nextSceneIndex)
    {
        _sceneIndex = nextSceneIndex;
        Save();
    }

}
