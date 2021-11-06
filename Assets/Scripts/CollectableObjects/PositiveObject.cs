using UnityEngine;

public class PositiveObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Destroy(gameObject);
        }
    }
}
