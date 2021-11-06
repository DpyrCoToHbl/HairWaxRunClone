using System.Collections;
using UnityEngine;

public class GameAudioPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collectPositiveItemSound;
    [SerializeField] private AudioClip _collectNegativeItemSound;
    [SerializeField] private AudioClip _rollOnRedMatSound;
    [SerializeField] private AudioClip _rollOnGreenMatSound;


    private void OnEnable()
    {
        _player.PositiveItemCollected += OnPositiveItemCollected;
        _player.NegativeItemCollected += OnNegativeItemCollected;
        _player.SteppedOnDuctTape += OnSteppedOnDuctTape;
    }

    private void OnDisable()
    {
        _player.PositiveItemCollected -= OnPositiveItemCollected;
        _player.NegativeItemCollected -= OnNegativeItemCollected;
        _player.SteppedOnDuctTape -= OnSteppedOnDuctTape;
    }

    private void OnPositiveItemCollected()
    {
        _audioSource.PlayOneShot(_collectPositiveItemSound);
    }

    private void OnNegativeItemCollected()
    {
        _audioSource.PlayOneShot(_collectNegativeItemSound);
    }

    private void OnSteppedOnDuctTape(GameObject ductTape, string name)
    {
        if (name == "Green")
            StartCoroutine(PlayWithDelay(_rollOnGreenMatSound));
        else
            StartCoroutine(PlayWithDelay(_rollOnRedMatSound));
    }

    private IEnumerator PlayWithDelay(AudioClip audioClip)
    {
        float delay = 2;
        yield return new WaitForSeconds(delay);
        _audioSource.PlayOneShot(audioClip);
    }

}
