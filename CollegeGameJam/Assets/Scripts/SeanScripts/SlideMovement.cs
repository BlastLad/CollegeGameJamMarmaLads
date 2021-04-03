using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMovement : MonoBehaviour
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
    [SerializeField]
    GameObject objectToMove;
    [SerializeField]
    Transform positionToGoTo;

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

            if (Vector3.Distance(objectToMove.transform.position, pos1.position) < 0.1f)
            {
                // Debug.Log(pos1 + "Reached" + Vector2.Distance(transform.position, pos1.position));
                nextPos = pos2.position;
                if (hasDelay)
                {
                    isMoving = false;
                    StartCoroutine(DelayTimer(delayTime));
                }
            }
            else if (Vector3.Distance(objectToMove.transform.position, pos2.position) < 0.1f)
            {
                //  Debug.Log(pos1 + "Reached" + Vector2.Distance(transform.position, pos1.position));
                //Debug.Log(pos2 + " 2 Reached");
                nextPos = pos1.position;
               
                objectToMove.GetComponent<SnowBallMovement>().SetCanBeDestroyed(true);
                objectToMove.GetComponent<Rigidbody>().useGravity = true;
                SetIsMoving(false);
                objectToMove.transform.position = new Vector3(positionToGoTo.position.x, positionToGoTo.position.y, (int)positionToGoTo.position.z);
                EnableCollider();
                

            }
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, nextPos, speed * Time.deltaTime);




        }
    }


    private IEnumerator DelayTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        isMoving = true;
    }


    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetIsMoving(bool val)
    {
        isMoving = val;
    }


    public void SetObjectToMove(GameObject val)
    {
        objectToMove = val;
        SetIsMoving(true);
        StartCoroutine(DisableCollider(.7f, false));
        
    }

    private IEnumerator DisableCollider(float timeToWait, bool val)
    {
        yield return new WaitForSeconds(timeToWait);
        objectToMove.GetComponent<Collider>().enabled = val;

    }

    public void EnableCollider()
    {
        objectToMove.GetComponent<Collider>().enabled = true;
        objectToMove = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;    //So you can see the path the platform will take in scene view
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
