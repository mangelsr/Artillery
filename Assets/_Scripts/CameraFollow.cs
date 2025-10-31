using System;
using UnityEngine;

/**
 * @brief Camera system that follows target objects with smooth movement and constraints
 * Provides static access to camera objectives and notifies when objectives change
 */
public class CameraFollow : MonoBehaviour
{
    /**
     * @brief Static reference to the current objective GameObject the camera should follow
     */
    static GameObject _objective;

    [Header("Editor config")]
    /**
     * @brief Smoothing factor for camera movement (lower values = smoother movement)
     */
    [SerializeField] float smooth = 0.05f;

    /**
     * @brief Minimum X and Y coordinates the camera can move to
     */
    [SerializeField] Vector2 limitXY = Vector2.zero;

    [Header("Dynamic config")]
    /**
     * @brief Fixed Z position of the camera in world space
     */
    [SerializeField] float camZ;

    /**
     * @brief Action event that is invoked when the camera's objective changes
     * Other systems can subscribe to be notified when the camera target changes
     */
    public static Action onObjectiveChanged;

    /**
     * @brief Gets or sets the current camera objective
     * @value The GameObject for the camera to follow, or null to return to origin
     * @note Setting the objective triggers the onObjectiveChanged event
     */
    public static GameObject objective
    {
        get { return _objective; }
        set
        {
            _objective = value;
            onObjectiveChanged.Invoke();
        }
    }

    /**
     * @brief Initializes camera state and references
     * Resets objective to null and caches initial Z position
     */
    void Awake()
    {
        _objective = null;
        camZ = this.transform.position.z;
    }

    /**
     * @brief Updates camera position in FixedUpdate for smooth physics-based movement
     * Handles objective tracking, boundary clamping, and sleeping ball detection
     */
    void FixedUpdate()
    {
        Vector3 endPosition;

        if (objective == null)
        {
            endPosition = Vector3.zero;
        }
        else
        {
            endPosition = objective.transform.position;
            if (objective.tag == "Ball")
            {
                bool isSleeping = objective.GetComponent<Rigidbody>().IsSleeping();
                if (isSleeping)
                {
                    objective = null;
                    endPosition = Vector3.zero;
                    Cannon.isBlocked = true;
                    return;
                }
            }
        }
        endPosition.x = Mathf.Max(limitXY.x, endPosition.x);
        endPosition.y = Mathf.Max(limitXY.y, endPosition.y);

        endPosition = Vector3.Lerp(transform.position, endPosition, smooth);

        endPosition.z = camZ;

        transform.position = endPosition;

        Camera.main.orthographicSize = endPosition.y + 20; // 20 is the initial camera size
    }
}