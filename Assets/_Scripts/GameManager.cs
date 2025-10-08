using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int _ballSpeed = 48;
    [SerializeField] int _shootsPerGame = 5;
    [SerializeField] float _rotationSpeed = 1;

    public static int BallSpeed
    {
        get => Instance._ballSpeed;
        set => Instance._ballSpeed = Mathf.Clamp(value, 25, 70);
    }

    public static int ShootsPerGame
    {
        get => Instance._shootsPerGame;
        set => Instance._shootsPerGame = Mathf.Max(-1, value);
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

    void Update()
    {
        if (ShootsPerGame <= 0)
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        GameObject winCanvas = FindObjectByName("WinCanvas");
        if (winCanvas != null) winCanvas.SetActive(true);
    }

    void LoseGame()
    {
        GameObject loseCanvas = FindObjectByName("LoseCanvas");
        if (loseCanvas != null) loseCanvas.SetActive(true);
    }

    GameObject FindObjectByName(string objectName)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == objectName && obj.scene == gameObject.scene)
            {
                return obj;
            }
        }
        return null;
    }
}