using UnityEngine;

public class ConfigCanvas : MonoBehaviour
{
    void Awake()
    {
        CameraFollow.onObjectiveChanged += ToggleCanvas;
    }

    void OnDestroy()
    {
        CameraFollow.onObjectiveChanged -= ToggleCanvas;
    }

    void ToggleCanvas()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
