using System.Collections;
using UnityEngine;

/**
 * @brief Represents a projectile ball that can collide with objects and explode
 * Handles collision detection, automatic destruction, and visual effects
 */
public class Ball : MonoBehaviour
{
    // The `Drag` property on the Rigid Body simulate air resistance
    // `AngularDrag` same resistance during rotation

    /**
     * @brief Particle effect prefab to instantiate when the ball explodes
     */
    [SerializeField] GameObject explosionParticles;

    /**
     * @brief Time in seconds after which the ball will automatically destroy itself
     */
    [SerializeField] float autoDestroyTime = 10f;

    /**
     * @brief Reference to the coroutine handling automatic destruction
     */
    Coroutine autoDestroyCoroutine;

    /**
     * @brief Initializes the ball and starts the auto-destruction timer
     */
    void Start()
    {
        autoDestroyCoroutine = StartCoroutine(AutoDestroyAfterTime());
    }

    /**
     * @brief Handles collision events with other objects
     * Triggers different behaviors based on the collided object's tag
     * 
     * @param collision The collision data containing information about the collision
     */
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

    /**
     * @brief Coroutine that automatically destroys the ball after a specified time
     * Provides a safety mechanism to clean up balls that don't collide with anything
     * 
     * @return IEnumerator for coroutine execution
     */
    IEnumerator AutoDestroyAfterTime()
    {
        yield return new WaitForSeconds(autoDestroyTime);

        if (this != null && gameObject != null)
        {
            Explode();
        }
    }

    /**
     * @brief Stops the auto-destruction coroutine if it's running
     * Used when the ball collides with something and will be destroyed manually
     */
    void StopAutoDestroy()
    {
        if (autoDestroyCoroutine != null)
        {
            StopCoroutine(autoDestroyCoroutine);
            autoDestroyCoroutine = null;
        }
    }

    /**
     * @brief Triggers the explosion effect and cleans up the ball object
     * Instantiates particle effects, updates game state, and destroys the ball
     */
    void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Cannon.isBlocked = false;
        CameraFollow.objective = null;
        Destroy(gameObject);
    }
}