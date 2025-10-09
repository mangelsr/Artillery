using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

    [Header("Editor configs")]
    [SerializeField] float minDistanceBetweenPoints = 0.1f;

    LineRenderer line;
    GameObject _lineObjective;
    List<Vector3> points;


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

    void ObjectiveUpdate()
    {
        lineObjective = CameraFollow.objective;
    }

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
        CameraFollow.onObjectiveChanged += ObjectiveUpdate;
    }

    void FixedUpdate()
    {
        if (lineObjective != null && lineObjective.tag == "Ball")
        {
            AddPoint();
        }
    }

    void OnDestroy()
    {
        CameraFollow.onObjectiveChanged -= ObjectiveUpdate;
    }

}
