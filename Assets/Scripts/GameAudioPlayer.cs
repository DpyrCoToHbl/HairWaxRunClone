using System.Collections;
using UnityEngine;

public class GameAudioPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _positiveItemSound;
    [SerializeField] private AudioClip _negativeItemSound;
    [SerializeField] private AudioClip _rollOnRedMatSound;
    [SerializeField] private AudioClip _rollOnGreenMatSound;


    private void OnEnable()
    {
        _player.PositiveItemCollected += OnPositiveItemCollected;
        _player.NegativeItemCollected += OnNegativeItemCollected;
        _player.SteppedOnGreenDuctTape += OnGreenMatTriggerEnter;
        _player.SteppedOnRedDuctTape += OnRedMatTriggerEnter;
    }

    private void OnDisable()
    {
        _player.PositiveItemCollected -= OnPositiveItemCollected;
        _player.NegativeItemCollected -= OnNegativeItemCollected;
        _player.SteppedOnGreenDuctTape -= OnGreenMatTriggerEnter;
        _player.SteppedOnRedDuctTape -= OnRedMatTriggerEnter;
    }

    private void OnPositiveItemCollected()
    {
        _audioSource.PlayOneShot(_positiveItemSound);
    }

    private void OnNegativeItemCollected()
    {
        _audioSource.PlayOneShot(_negativeItemSound);
    }

    private void OnGreenMatTriggerEnter()
    {
        StartCoroutine(PlayWithDelay(_rollOnGreenMatSound));
    }

    private void OnRedMatTriggerEnter()
    {
        StartCoroutine(PlayWithDelay(_rollOnRedMatSound));
    }

    private IEnumerator PlayWithDelay(AudioClip audioClip)
    {
        float delay = 2;
        yield return new WaitForSeconds(delay);
        _audioSource.PlayOneShot(audioClip);
    }

}
