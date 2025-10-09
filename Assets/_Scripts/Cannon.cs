using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{

    public static bool isBlocked;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject particlesPrefab;
    [SerializeField] AudioClip shotClip;
    GameObject shootSound;
    AudioSource shootSource;
    GameObject cannonTip;
    float rotation;

    CannonControls controls;
    InputAction aim;
    InputAction changeShotForce;
    InputAction shotAction;

    void Awake()
    {
        controls = new CannonControls();
    }

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

    void Start()
    {
        cannonTip = transform.Find("CannonTip").gameObject;
        shootSound = GameObject.Find("ShootSound");
        shootSource = shootSound.GetComponent<AudioSource>();
    }

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
