using UnityEngine;
using UnityEngine.InputSystem;

/**
 * @brief Controls cannon behavior including aiming, shooting, and input handling
 * Manages cannon rotation, ball projectile instantiation, and game state updates
 */
public class Cannon : MonoBehaviour
{
    /**
     * @brief Static flag indicating if the cannon is currently blocked from shooting
     */
    public static bool isBlocked;

    /**
     * @brief Prefab for the ball projectile to be instantiated when shooting
     */
    [SerializeField] GameObject ballPrefab;

    /**
     * @brief Prefab for particle effects to be played when shooting
     */
    [SerializeField] GameObject particlesPrefab;

    /**
     * @brief Audio clip to play when the cannon shoots
     */
    [SerializeField] AudioClip shotClip;

    /**
     * @brief Reference to the GameObject containing the shoot sound audio source
     */
    GameObject shootSound;

    /**
     * @brief AudioSource component for playing shoot sounds
     */
    AudioSource shootSource;

    /**
     * @brief Reference to the cannon tip GameObject where projectiles spawn
     */
    GameObject cannonTip;

    /**
     * @brief Current rotation angle of the cannon in degrees (0-90 range)
     */
    float rotation;

    /**
     * @brief Input system controls for cannon actions
     */
    CannonControls controls;

    /**
     * @brief Input action for aiming/rotating the cannon
     */
    InputAction aim;

    /**
     * @brief Input action for modifying shot force/ball speed
     */
    InputAction changeShotForce;

    /**
     * @brief Input action for triggering a shot
     */
    InputAction shotAction;

    /**
     * @brief Initializes the input controls system
     * Creates new instance of CannonControls input actions
     */
    void Awake()
    {
        controls = new CannonControls();
    }

    /**
     * @brief Enables input actions and sets up event listeners
     * Called when the GameObject becomes enabled and active
     */
    void OnEnable()
    {
        aim = controls.Cannon.Aim;
        changeShotForce = controls.Cannon.ModifyShootForce;
        shotAction = controls.Cannon.Shoot;
        aim.Enable();
        changeShotForce.Enable();
        shotAction.Enable();
        shotAction.performed += Shoot;
    }

    /**
     * @brief Initializes cannon components and references
     * Finds cannon tip and audio components in the scene
     */
    void Start()
    {
        cannonTip = transform.Find("CannonTip").gameObject;
        shootSound = GameObject.Find("ShootSound");
        shootSource = shootSound.GetComponent<AudioSource>();
    }

    /**
     * @brief Updates cannon state each frame
     * Handles rotation input and shot force modification
     * Clamps rotation to valid range (0-90 degrees)
     */
    void Update()
    {
        GameManager.BallSpeed += (int)changeShotForce.ReadValue<float>();

        rotation += aim.ReadValue<float>() * GameManager.RotationSpeed;
        if (rotation <= 90 && rotation >= 0)
        {
            transform.eulerAngles = new Vector3(rotation, 90, 0.0f);
        }

        if (rotation > 90) rotation = 90;
        if (rotation < 0) rotation = 0;
    }

    /**
     * @brief Handles the shoot action when triggered by input
     * Instantiates a ball projectile, applies velocity, and updates game state
     * 
     * @param context Input action callback context containing input data
     */
    void Shoot(InputAction.CallbackContext context)
    {
        if (GameManager.ShootsPerGame > 0 && !isBlocked)
        {
            GameObject temp = Instantiate(ballPrefab, cannonTip.transform.position, transform.rotation);
            CameraFollow.objective = temp;
            Rigidbody tempRB = temp.GetComponent<Rigidbody>();
            Vector3 shootDirection = transform.rotation.eulerAngles;
            shootDirection.y = 90 - shootDirection.x;
            tempRB.velocity = shootDirection.normalized * GameManager.BallSpeed;
            Vector3 particlesDirection = new Vector3(-90 + shootDirection.x, 90, 9);
            Instantiate(
                particlesPrefab, cannonTip.transform.position, Quaternion.Euler(particlesDirection), transform
            );
            GameManager.ShootsPerGame--;
            shootSource.PlayOneShot(shotClip);
            isBlocked = true;
        }
    }

    /**
     * @brief Disables input actions and cleans up event listeners
     * Called when the GameObject becomes disabled or inactive
     */
    void OnDisable()
    {
        if (shotAction != null)
        {
            shotAction.performed -= Shoot;
            shotAction.Disable();
        }
        if (aim != null) aim.Disable();
        if (changeShotForce != null) changeShotForce.Disable();
        if (controls != null) controls.Disable();

        if (CameraFollow.objective != null) CameraFollow.objective = null;
    }
}