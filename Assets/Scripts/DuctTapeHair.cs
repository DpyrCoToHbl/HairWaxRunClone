using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class DuctTapeHair : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public static UnityAction HairStucked;
    public static UnityAction HairFelled;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ToggleVisibility()
    {
        if (_meshRenderer.enabled == true)
        {
            _meshRenderer.enabled = false;
            HairStucked?.Invoke();
        }
        else
        {
            if (TryGetComponent(out BodyHairCounter bodyHairCounter))
            {
                    _meshRenderer.enabled = true;
                    HairFelled?.Invoke();
                if (bodyHairCounter.HairCount > 0)
                {
                }
            }
        }
    }
}
