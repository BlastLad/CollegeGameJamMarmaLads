using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageCameraController : MonoBehaviour
{
    //A Script for the controlling of the camera in all Diorama Scenes


    [SerializeField]
    private float runSpeed = 0;
    [SerializeField]
    private float turnSpeed = 0;
    CameraActionMap cameraActionMap;
    public Vector3 currentPosition;
    public Transform cameraLocation;
    private Vector3 inputVector = new Vector3(0, 0);
    private Rigidbody rb;


    private void Awake()
    {
        cameraActionMap = new CameraActionMap();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private Vector3 Direction { get; set; }
    public void OnMovement(InputAction.CallbackContext context)
    {

        inputVector.x = context.ReadValue<Vector2>().x;
        inputVector.z = context.ReadValue<Vector2>().y;

        Vector3 cameraLocationForward = cameraLocation.forward;
        Vector3 cameraLocationRight = cameraLocation.right;

        cameraLocationForward.y = 0;
        cameraLocationRight.y = 0;

        cameraLocationForward = cameraLocationForward.normalized;
        cameraLocationRight = cameraLocationRight.normalized;


        //Direction = new Vector3(inputVector.x, 0, inputVector.z);
        Direction = (cameraLocationForward * inputVector.z + cameraLocationRight * inputVector.x);
    }
}
