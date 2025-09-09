using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // The `Drag` property on the Rigid Body simulate air resistance
    // `AngularDrag` same resistance during rotation

    [SerializeField] GameObject explosionParticles;

    void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;

        if (collisionTag == "Ground")
        {
            Invoke("Explode", 3);
        }

        if (collisionTag == "Obstacle" || collisionTag == "Objective")
        {
            Explode();
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
