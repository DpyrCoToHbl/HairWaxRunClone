using UnityEngine;

public class MenuScreenDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private GameObject _looseScreenPanel;
    [SerializeField] private GameObject _levelCompletedScreenPanel;
    [SerializeField] private GameObject _hairnessBar;
    [SerializeField] private GameObject _controlTutorialImage;

    private void OnEnable()
    {
        _player.Lose += OnLoose;
        _player.FinishLineReached += OnFinishLineReached;
        _playerMover.Moving += OnMoving;
    }

    private void OnDisable()
    {
        _player.Lose -= OnLoose;
        _player.FinishLineReached -= OnFinishLineReached;
        _playerMover.Moving -= OnMoving;
    }

    private void OnLoose()
    {
        _looseScreenPanel.SetActive(true);
        _hairnessBar.SetActive(false);
    }

    private void OnFinishLineReached()
    {
        _levelCompletedScreenPanel.SetActive(true);
        _hairnessBar.SetActive(false);
    }

    private void OnMoving()
    {
        _controlTutorialImage.SetActive(false);
    }
}
