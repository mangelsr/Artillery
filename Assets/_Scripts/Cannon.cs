using UnityEngine;

public class Cannon : MonoBehaviour
{

    public static bool isBlocked;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject particlesPrefab;

    GameObject cannonTip;
    float rotation;

    void Start()
    {
        cannonTip = transform.Find("CannonTip").gameObject;
    }

    void Update()
    {
        rotation += Input.GetAxis("Horizontal") * GameManager.RotationSpeed;
        if (rotation <= 90 && rotation >= 0)
        {
            transform.eulerAngles = new Vector3(rotation, 90, 0.0f);
        }

        if (rotation > 90) rotation = 90;
        if (rotation < 0) rotation = 0;

        if (!isBlocked && GameManager.ShootsPerGame > 0 && Input.GetKeyDown(KeyCode.Space))
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
            isBlocked = true;
        }
    }
}
