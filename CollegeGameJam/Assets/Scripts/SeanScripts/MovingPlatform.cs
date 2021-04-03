using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("Move Pos2 anywhere to set where you want the platform to go :]")]
    public Transform pos1, pos2;          //The 2 positions the platform rebounds from
    public float speed;                   //Speed of the Platform
    public Transform startPos;            //Initial Position of the platform
    [SerializeField]
    bool isMoving = false;         //Wether or not the platform is moving or not
    [Tooltip("Wether or not the platform only moves 1 way or not")]
    public bool shouldRebound = false;          //Wether or not the platform only moves 1 way or not

    [SerializeField]
    GameObject parentObj;                 //The parent transform Object so the pos1 and pos 2 do not move with the platform this script is attached to

    private string playerString = "Player";


    Vector3 initalPosition;

    Vector3 nextPos;
    [SerializeField]
    bool hasDelay = false;
    [SerializeField]
    float delayTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        initalPosition = transform.position;
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {

            if (Vector3.Distance(transform.position, pos1.position) < 0.1f)
            {
               // Debug.Log(pos1 + "Reached" + Vector2.Distance(transform.position, pos1.position));
                nextPos = pos2.position;
                if (hasDelay)
                {
                    isMoving = false;
                    StartCoroutine(DelayTimer(delayTime));
                }
            }
            else if (Vector3.Distance(transform.position, pos2.position) < 0.1f)
            {
              //  Debug.Log(pos1 + "Reached" + Vector2.Distance(transform.position, pos1.position));
                //Debug.Log(pos2 + " 2 Reached");
                if (shouldRebound)
                {                   
                    nextPos = pos1.position;
                    if (hasDelay)
                    {
                        isMoving = false;
                        StartCoroutine(DelayTimer(delayTime));

                    }
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

            


        }
    }


    private IEnumerator DelayTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        isMoving = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(playerString) && other.transform.position.y > transform.position.y)
        {
            other.collider.transform.SetParent(transform);

         
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(playerString))
        {
            other.collider.transform.SetParent(null);
        }
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetIsMoving(bool val)
    {
        isMoving = val;
    }

    void OnDrawGizmos()
    {       
        Gizmos.color = Color.green;    //So you can see the path the platform will take in scene view
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
