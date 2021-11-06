using System.Collections.Generic;
using UnityEngine;

public class DuctTapeHairHolder : MonoBehaviour
{
    [SerializeField] private List<DuctTapeHair> _ductTapeHairs = new List<DuctTapeHair>();

    public List<DuctTapeHair> GetHairsList()
    {
        return _ductTapeHairs;
    }
}
