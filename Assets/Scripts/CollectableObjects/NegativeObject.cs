using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Destroy(gameObject);
        }
    }
}
