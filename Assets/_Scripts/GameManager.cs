using UnityEngine;

/**
 * @brief Main game controller that manages game state, configuration, and win/lose conditions.
 * Implements singleton pattern to provide global access to game settings.
 */
public class GameManager : MonoBehaviour
{
    /**
     * @brief Singleton instance of the GameManager class
     */
    public static GameManager Instance { get; private set; }

    /**
     * @brief The speed at which balls move in the game (range: 25-70)
     */
    [SerializeField] int _ballSpeed = 48;

    /**
     * @brief Number of shots allowed per game (-1 for unlimited shots)
     */
    [SerializeField] int _shootsPerGame = 5;

    /**
     * @brief Speed of rotation for game objects (range: 0.1-5.0)
     */
    [SerializeField] float _rotationSpeed = 1;

    /**
     * @brief Gets or sets the ball speed with clamping between 25 and 70
     * @value The current ball speed value
     */
    public static int BallSpeed
    {
        get => Instance._ballSpeed;
        set => Instance._ballSpeed = Mathf.Clamp(value, 25, 70);
    }

    /**
     * @brief Gets or sets the number of shots per game (-1 for unlimited)
     * @value The number of shots allowed
     */
    public static int ShootsPerGame
    {
        get => Instance._shootsPerGame;
        set => Instance._shootsPerGame = Mathf.Max(-1, value);
    }

    /**
     * @brief Gets or sets the rotation speed with clamping between 0.1 and 5.0
     * @value The current rotation speed
     */
    public static float RotationSpeed
    {
        get => Instance._rotationSpeed;
        set => Instance._rotationSpeed = Mathf.Clamp(value, 0.1f, 5f);
    }

    /**
     * @brief Initializes the singleton instance during object awakening
     * Ensures only one instance of GameManager exists in the scene
     */
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

    /**
     * @brief Called every frame to update game state
     * Checks if the player has run out of shots and triggers game over if so
     */
    void Update()
    {
        if (ShootsPerGame <= 0)
        {
            LoseGame();
        }
    }

    /**
     * @brief Triggers the win condition and displays the win canvas
     */
    public void WinGame()
    {
        GameObject winCanvas = FindObjectByName("WinCanvas");
        if (winCanvas != null) winCanvas.SetActive(true);
    }

    /**
     * @brief Triggers the lose condition and displays the lose canvas
     * Called automatically when shots run out or manually when needed
     */
    void LoseGame()
    {
        GameObject loseCanvas = FindObjectByName("LoseCanvas");
        if (loseCanvas != null) loseCanvas.SetActive(true);
    }

    /**
     * @brief Finds a GameObject by name in the current scene
     * @param objectName The name of the GameObject to find
     * @return The found GameObject, or null if not found
     */
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