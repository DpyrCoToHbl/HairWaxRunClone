using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class DuctTapeHair : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public UnityAction HairStucked;
    public UnityAction HairFelled;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            ToggleVisibility(player.gameObject);
    }

    private void ToggleVisibility(GameObject gameObject)
    {
        if (_meshRenderer.enabled == true)
        {
            _meshRenderer.enabled = false;
            HairStucked?.Invoke();
        }
        else
        {
            if (gameObject.TryGetComponent(out BodyHairCounter bodyHairCounter))
            {
                if (bodyHairCounter.HairCount > 0)
                {
                    _meshRenderer.enabled = true;
                    HairFelled?.Invoke();
                }
            }
        }
    }
}
