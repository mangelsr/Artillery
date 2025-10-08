using UnityEngine;
using UnityEngine.UI;

public class ShootForceSlider : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(
            delegate { ControlChanges(); }
        );
    }

    void FixedUpdate()
    {
        slider.value = GameManager.BallSpeed;
    }

    void ControlChanges()
    {
        GameManager.BallSpeed = (int)slider.value;
    }
}
