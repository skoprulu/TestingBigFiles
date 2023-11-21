using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    public static AxeThrow Instance { set; get; }   // For reach another scripts

    PlayerController _playerController;
    Rigidbody rb;
    GameObject textGameObject;          // Access Dynamic Text Object
    public GameObject tPCam;            // Third Person Camera
    public GameObject aimCam;           // Aim Camera

    public float rotationSpeed = 1000.0f;   // Axe rotation Speed
    public float throwSpeed = 100.0f;       // Axe Throw Speed
    [HideInInspector] public bool axeThrown;    // Checking Axe Thrown
    bool axeRotate;                         // Activate rotation after throw



    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerController = PlayerController.Instance;
        textGameObject = gameObject.transform.GetChild(0).gameObject;

        rb = GetComponent<Rigidbody>();

        // Hierarchy sets
        aimCam.SetActive(false);
        textGameObject.SetActive(false);

        // Axe start parameter sets
        rb.isKinematic = true;
        rb.useGravity = false;
        axeRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        AxeThrowAim();

        if (textGameObject != null)
        {
            textGameObject.transform.LookAt(tPCam.gameObject.transform, Vector3.up);
        }
    }

    void AxeThrowAim()
    {

        if (Input.GetMouseButton(1))
        {
            // Activate Aim Camera
            aimCam.SetActive(true);
            tPCam.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                // AxeThrow animation start
                _playerController.anim.SetTrigger("ThorwAxe");
            }
        }
        else
        {
            // Activate Third Person Camera
            tPCam.SetActive(true);
            aimCam.SetActive(false);
        }

        // Start rotation
        if (axeRotate)
        {
            transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
        }

    }

    // Throw Axe on animation event
    public void ThrowAxe()
    {
        axeThrown = true;
        rb.isKinematic = false;
        rb.transform.parent = null;
        axeRotate = true;
        // Axe Throw Force
        rb.AddForce(_playerController.gameObject.transform.TransformDirection(Vector3.forward) * throwSpeed, ForceMode.Impulse);

    }


    // Axe Stops on Collision
    private void OnCollisionEnter(Collision collision)
    {
        axeRotate = false;
        rb.isKinematic = true;

        textGameObject.SetActive(true);

    }

}
