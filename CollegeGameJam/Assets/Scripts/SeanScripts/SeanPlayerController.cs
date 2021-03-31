using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeanPlayerController : MonoBehaviour
{
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
    public SphereCollider col;                      //sphere collider for ground detection


    [Tooltip("ATTACH the Main Camera object from the scene")]
    public Transform cameraLocation; //Reference to stage Camera
    private void Awake()
    {
        playerActionMap = new PlayerActionMap();    // Creation of new PlayerActionMap C# Script that will be used for all called events
        rb = GetComponent<Rigidbody>();             // Reference to RigidBody
        col = GetComponent<SphereCollider>();       // Reference to the sphere collider for ground detection

        playerActionMap.Default.Jump.started += ctx => Jump();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Rotation; //To Rotate the player as necessary [NOT PART OF INPUT SYSTEM]
    }

    private void FixedUpdate()
    {
        Vector3 position = rb.position;

        position = position + MoveForwardBasedOnCamera(inputVector) *walkSpeed * Time.fixedDeltaTime;

        rb.MovePosition(position);
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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



    private Quaternion Rotation => Quaternion.LookRotation(RotationDirection);            //Functions to detemine the players local rotation

    private Vector3 RotationDirection =>
        Vector3.RotateTowards(transform.forward, Direction, turnSpeed * Time.deltaTime, 0);
}
