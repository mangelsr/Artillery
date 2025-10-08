using UnityEngine;
using UnityEngine.Events;


public class Objective : MonoBehaviour
{
    [SerializeField] UnityEvent GameWon;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion")
        {
            GameWon.Invoke();
            Destroy(gameObject);
        }
    }
}
