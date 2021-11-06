using UnityEngine;

[RequireComponent(typeof(Player))]
public class DropHairEffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.PositiveItemCollected += OnPositiveItemCollected;
    }

    private void OnDisable()
    {
        _player.PositiveItemCollected += OnPositiveItemCollected;
    }

    private void OnPositiveItemCollected()
    {
        if (_player.TryGetComponent(out BodyHairCounter bodyHairCounter))
        {
            if (bodyHairCounter.HairCount > 0)
                Instantiate(_particlePrefab, transform.position, transform.rotation);
        }
    }
}
