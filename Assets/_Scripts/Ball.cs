using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // The `Drag` property on the Rigid Body simulate air resistance
    // `AngularDrag` same resistance during rotation

    [SerializeField] GameObject explosionParticles;
    [SerializeField] float autoDestroyTime = 10f;

    Coroutine autoDestroyCoroutine;

    void Start()
    {
        autoDestroyCoroutine = StartCoroutine(AutoDestroyAfterTime());
    }

    void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;

        if (collisionTag == "Ground")
        {
            Invoke("Explode", 3);
            StopAutoDestroy();
        }

        if (collisionTag == "Obstacle" || collisionTag == "Objective")
        {
            Explode();
            StopAutoDestroy();
        }
    }

    IEnumerator AutoDestroyAfterTime()
    {
        yield return new WaitForSeconds(autoDestroyTime);

        if (this != null && gameObject != null)
        {
            Explode();
        }
    }

    void StopAutoDestroy()
    {
        if (autoDestroyCoroutine != null)
        {
            StopCoroutine(autoDestroyCoroutine);
            autoDestroyCoroutine = null;
        }
    }

    void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Cannon.isBlocked = false;
        CameraFollow.objective = null;
        Destroy(gameObject);
    }
}
