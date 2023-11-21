using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { set; get; }


    [HideInInspector] public CharacterController _characterController;
    public Transform cam;               // Main Cam For Ref
    public GameObject tPCam;            // Third Person Cam

    public Animator anim;               // Character animation
    AxeAnimationEvents _axeAnimationEvents;
    AxeThrow _axeThrow;

    // Character Input Values
    public float speed = 6.0f;          // Character Speed
    public float animSpeed = 1.0f;      // For control animation speed (Default = 1.0f)
    public float turnSmoothTime = 0.1f; // For character rotate smoothly

    float turnSmoothVelocity;           // For Smooth Turn ref

    bool IsMoving;                      // Character moving check for animations

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _axeAnimationEvents = AxeAnimationEvents.Instance;
        _axeThrow = AxeThrow.Instance;
        _characterController = GetComponent<CharacterController>();

        IsMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        AnimationController();
        RunControl();

        // If Axe Still on Hand
        if (!_axeThrow.axeThrown)
        {
            AxeAnimControl();
        }
        
    }

    void CharacterMove()
    {
        if (_axeAnimationEvents.axeAnimPlaying) { return; } // Checking equip axe animation

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float turnAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, turnAngle, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            _characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

            IsMoving = true;
        }
        else { IsMoving = false; }
    }

    void AnimationController()
    {
        if (!IsMoving) { anim.SetBool("Running", false); }
        else
        {
            anim.SetBool("Running", true);
            anim.speed = animSpeed;
        }
    }

    // Character sprint
    void RunControl()
    {
        if (_axeAnimationEvents.axeAnimPlaying)
        {
            animSpeed = 1.0f;
            speed = 6.0f;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift) && IsMoving)
        {
            animSpeed = 1.334f;
            speed = 10.0f;
        }
        else
        {
            animSpeed = 1.0f;
            speed = 6.0f;
        }
    }

    void AxeAnimControl()
    {
        if ((Input.GetKeyDown(KeyCode.R)))
        {
            if (!_axeAnimationEvents.axeTaken)
            {
                anim.SetTrigger("EquipAxe");
            }
            else
            {
                anim.SetTrigger("UnEquipAxe");
            }
        }
    }


}
