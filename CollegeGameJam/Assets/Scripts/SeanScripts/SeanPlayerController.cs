using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeanPlayerController : MonoBehaviour
{
    public static SeanPlayerController Instance { get; private set; }

    [SerializeField]
    private float walkSpeed = 0;                       //walkSpeed for the player
    [SerializeField]
    private float turnSpeed = 0;                      //turnspeed for rotation
    PlayerActionMap playerActionMap;                  //!!!! PlayerActionMap field
    public Vector3 currentPosition;                  //Player's current postion value
    private Vector3 inputVector = new Vector3(0, 0);   //The input recieved by InputObject
    private Rigidbody rb;
    
    public LayerMask groundLayers;                  //detection for ground layers
    [SerializeField]
    private float jumpForce = 7f;                   //jump force of the player
    public CapsuleCollider col;                      //sphere collider for ground detection


    [Tooltip("ATTACH the Main Camera object from the scene")]
    public Transform cameraLocation; //Reference to stage Camera

    public float playerGravity = 1;
    public float gravityMod = 5;
    public float linearDrag = 4;


    Animator anim;
    string IsRunningStr = "isRunning";
    string IsJumpingStr = "isJumping";

    [Header("Audio")]
    [SerializeField]
    AudioClip[] footsteps;
    [SerializeField]
    float footstepVolume = 1f;
    [SerializeField]
    AudioClip jumpSFX;
    [SerializeField]
    float jumpSFXVolume = 1f;

    public bool canMove { get; set; }

    private void Awake()
    {
        Instance = this;
        playerActionMap = new PlayerActionMap();    // Creation of new PlayerActionMap C# Script that will be used for all called events
        rb = GetComponent<Rigidbody>();             // Reference to RigidBody
        col = GetComponent<CapsuleCollider>();       // Reference to the sphere collider for ground detection
        anim = GetComponent<Animator>();            //Reference to the animator
        canMove = true;

        playerActionMap.Default.Jump.started += ctx => Jump();
        playerActionMap.Default.CarrotPlace.started += ctx => GetComponent<PlayerCheckPointControls>().PlaceCarrot();
    }

    // Start is called before the first frame update
    void Start()
    {


        GameManager.Instance.SetSpawnPosition(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.rotation = Rotation; //To Rotate the player as necessary [NOT PART OF INPUT SYSTEM]
        }

        GravityController();
        
    }

    private void FixedUpdate()
    {
        Vector3 position = rb.position;

        CheckForRunAnimation();
        CheckForFallAnimation();

        if (!IsGrounded())
        {
            walkSpeed = 7.25f;
        }
        else
        {
            walkSpeed = 6;
        }

        if (canMove)
        {
            transform.position += MoveForwardBasedOnCamera(inputVector) * walkSpeed * Time.fixedDeltaTime;
        }



       // rb.MovePosition(position);
    }

    
    private Vector3 Direction { get; set; }
    public void OnMovement(InputAction.CallbackContext context)    //The function the unity event system calls Uses attached Player Input component
    {
        inputVector.x = context.ReadValue<Vector2>().x;                      //gets the vectors values passed in by the context of the call , keyboard, gamepad etc
        inputVector.z = context.ReadValue<Vector2>().y;

        Vector3 cameraLocationForward = cameraLocation.forward;               //Get's camera position
        Vector3 cameraLocationRight = cameraLocation.right;
         
        cameraLocationForward.y = 0;                                         //Sets the y to 0
        cameraLocationRight.y = 0;

        cameraLocationForward = cameraLocationForward.normalized;            //To ensure they at least have a value of 1 
        cameraLocationRight = cameraLocationRight.normalized;


        Direction = (cameraLocationForward * inputVector.z + cameraLocationRight * inputVector.x);      //Determines direction based on camera for the rotation
    }


    public Vector3 MoveForwardBasedOnCamera(Vector3 val)               //Determines what is forward based on the Camera's current position
    {
        Vector3 cameraLocationForward = cameraLocation.forward;
        Vector3 cameraLocationRight = cameraLocation.right;

        cameraLocationForward.y = 0;
        cameraLocationRight.y = 0;

        cameraLocationForward = cameraLocationForward.normalized; //Same as above
        cameraLocationRight = cameraLocationRight.normalized;

        Vector3 inputVectorCam = (cameraLocationForward * inputVector.z + cameraLocationRight * inputVector.x);
        return inputVectorCam;
    }

    public void Jump()                                      //checks if player is grounded, then jumps if grounded
    {
        if (IsGrounded())
        {
            AudioSource.PlayClipAtPoint(jumpSFX, 0.9f * Camera.main.transform.position
                + 0.1f * transform.position, jumpSFXVolume);
            anim.SetBool(IsJumpingStr, true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    private void CheckForFallAnimation()
    {
        if (!IsGrounded() && rb.velocity.y < 1)
        {
            anim.SetBool(IsJumpingStr, false);
        }
    }

    private void CheckForRunAnimation()
    {
        if(inputVector==Vector3.zero)
        {
            anim.SetBool(IsRunningStr, false);
        }
        else if (!IsGrounded())
        {
            anim.SetBool(IsRunningStr, false);
        }
        else
        {
            anim.SetBool(IsRunningStr, true);
        }
    }

    void GravityController()
    {
        if (IsGrounded())
        {
            GetComponent<PlayerGravity>().gravityScale = 0.3f;
            rb.drag = linearDrag;
        }
        else
        {
            GetComponent<PlayerGravity>().gravityScale = playerGravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < Mathf.Epsilon)
            {
                GetComponent<PlayerGravity>().gravityScale = playerGravity * gravityMod;
            }

        }
    }

    private bool IsGrounded()                               //determines whether the player is grounded
    {
        return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }

    private void OnEnable()
    {
        playerActionMap.Enable();
    }

    private void OnDisable()
    {
        playerActionMap.Disable();
    }

   /* private void OnCollisionEnter(Collision other)
    {
        if (!IsGrounded() && other.gameObject.CompareTag("Ground"))
        {
            checkForCollision(other);
        }
    }

    public void checkForCollision(Collision other)
    {
        Debug.Log(other.gameObject.name + "hit this1");
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), other.transform.position, 50f, groundLayers))
        {
            Debug.Log(other.gameObject.name + "hit this");
           // rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }*/


    private Quaternion Rotation => Quaternion.LookRotation(RotationDirection);            //Functions to detemine the players local rotation

    private Vector3 RotationDirection =>
        Vector3.RotateTowards(transform.forward, Direction, turnSpeed * Time.deltaTime, 0);


    public PlayerActionMap GetPlayerActionMap()
    {
        return playerActionMap;
    }

    public void Footstep()
    {
        if(IsGrounded())
        {
            AudioSource.PlayClipAtPoint(footsteps[Random.Range(0, footsteps.Length)], 0.9f * Camera.main.transform.position
        + 0.1f * transform.position, footstepVolume);
        }

    }

}
