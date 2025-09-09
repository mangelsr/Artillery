using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion") Destroy(gameObject);
    }

}
