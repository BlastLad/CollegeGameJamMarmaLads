using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Parameters
    [Header("Movement")]
    private float moveSpeed;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 9f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    //Cached References
    [SerializeField] private CharacterController controller; //use this game object
    [SerializeField] private Transform cam; //use main camera

    void Start()
    {
        
    }


    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        //check for player input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            GetMoveSpeed();
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void GetMoveSpeed()
    {
        //if player is holding the SPACEBAR, move speed is sprinting
        if(Input.GetKey(KeyCode.Space))
        {
            moveSpeed = sprintSpeed;
        }
        else
            //if not, player is walking
            moveSpeed = walkSpeed;
    }
}
