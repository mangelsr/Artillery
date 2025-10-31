using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Renders and manages a trail line that follows a target object
 * Creates a continuous line trail behind a moving object with configurable point density
 */
public class Trail : MonoBehaviour
{
    [Header("Editor configs")]
    /**
     * @brief Minimum distance required between trail points to add a new point
     * Prevents overcrowding the trail with too many closely spaced points
     */
    [SerializeField] float minDistanceBetweenPoints = 0.1f;

    /**
     * @brief Reference to the LineRenderer component used to draw the trail
     */
    LineRenderer line;

    /**
     * @brief The target object that the trail should follow
     */
    GameObject _lineObjective;

    /**
     * @brief List of Vector3 points that make up the trail path
     */
    List<Vector3> points;

    /**
     * @brief Gets or sets the target object for the trail to follow
     * @value The GameObject that the trail will track and follow
     * @note When set, resets the trail points and disables the line renderer temporarily
     */
    public GameObject lineObjective
    {
        get { return _lineObjective; }
        set
        {
            _lineObjective = value;
            if (_lineObjective != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    /**
     * @brief Gets the most recent point added to the trail
     * @return The last Vector3 point in the trail, or Vector3.zero if no points exist
     */
    public Vector3 LastPoint
    {
        get
        {
            if (points == null)
            {
                return Vector3.zero;
            }
            return points[points.Count - 1];
        }
    }

    /**
     * @brief Adds a new point to the trail at the current target object's position
     * Only adds the point if it meets the minimum distance requirement from the last point
     * Updates the LineRenderer with the new point
     */
    public void AddPoint()
    {
        Vector3 point = _lineObjective.transform.position;
        if (points.Count > 0 && (point - LastPoint).magnitude < minDistanceBetweenPoints)
        {
            return;
        }

        points.Add(point);

        line.positionCount = points.Count;
        line.SetPosition(points.Count - 1, LastPoint);
        line.enabled = true;
    }

    /**
     * @brief Updates the trail's target objective when the camera objective changes
     * Called by the CameraFollow.onObjectiveChanged event
     */
    void ObjectiveUpdate()
    {
        lineObjective = CameraFollow.objective;
    }

    /**
     * @brief Initializes the trail component
     * Gets the LineRenderer component, initializes the points list, and subscribes to objective change events
     */
    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
        CameraFollow.onObjectiveChanged += ObjectiveUpdate;
    }

    /**
     * @brief FixedUpdate is called at fixed time intervals
     * Adds trail points when following a ball object to ensure smooth physics-based trail
     */
    void FixedUpdate()
    {
        if (lineObjective != null && lineObjective.tag == "Ball")
        {
            AddPoint();
        }
    }

    /**
     * @brief Called when the trail object is destroyed
     * Unsubscribes from the objective change event to prevent memory leaks
     */
    void OnDestroy()
    {
        CameraFollow.onObjectiveChanged -= ObjectiveUpdate;
    }
}