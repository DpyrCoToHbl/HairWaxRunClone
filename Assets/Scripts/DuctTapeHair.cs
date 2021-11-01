using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class DuctTapeHair : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BodyHairCounter bodyHairCounter))
        {
            ToggleVisibility(bodyHairCounter);
        }
    }

    private void ToggleVisibility(BodyHairCounter bodyHairCounter)
    {
        if (_meshRenderer.enabled == true)
        {
            _meshRenderer.enabled = false;
            bodyHairCounter.AddHair();

            if(TryGetComponent(out HairGrower hairGrower))
            {
                hairGrower.TryAddHair();
                Debug.Log("Added");
            }
        }
        else
        {
            _meshRenderer.enabled = true;
            bodyHairCounter.RemoveHair();

            if (TryGetComponent(out HairGrower hairGrower))
            {
                hairGrower.TryRemoveHair();
                Debug.Log("Deleted");
            }
        }
    }
}
