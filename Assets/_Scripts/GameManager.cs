using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int _ballSpeed = 30;
    [SerializeField] int _shootsPerGame = 10;
    [SerializeField] float _rotationSpeed = 1;

    public static int BallSpeed
    {
        get => Instance._ballSpeed;
        set => Instance._ballSpeed = Mathf.Max(0, value);
    }

    public static int ShootsPerGame
    {
        get => Instance._shootsPerGame;
        set => Instance._shootsPerGame = Mathf.Max(0, value);
    }

    public static float RotationSpeed
    {
        get => Instance._rotationSpeed;
        set => Instance._rotationSpeed = Mathf.Clamp(value, 0.1f, 5f);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is an existing instance of this class");
        }
    }
}