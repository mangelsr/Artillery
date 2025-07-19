using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static int BallSpeed = 30;
    public static int ShootsPerGame = 10;
    public static float RotationSpeed = 1;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Debug.LogError("There is an existing instance of this class");
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
