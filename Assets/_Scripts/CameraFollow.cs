using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static GameObject _objective;

    [Header("Editor config")]
    [SerializeField] float smooth = 0.05f;
    [SerializeField] Vector2 limitXY = Vector2.zero;

    [Header("Dynamic config")]
    [SerializeField] float camZ;


    public static Action onObjectiveChanged;

    public static GameObject objective
    {
        get { return _objective; }
        set
        {
            _objective = value;
            onObjectiveChanged.Invoke();
        }
    }


    void Awake()
    {
        _objective = null;
        camZ = this.transform.position.z;
    }
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
