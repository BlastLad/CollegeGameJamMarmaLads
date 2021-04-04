using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallMovement : MonoBehaviour
{
    [SerializeField]
    float pushSpeed = 2f;
    [SerializeField]
    float pushTime = 1.1f;
    [SerializeField]
    float rayLength = 5.5f;

    [SerializeField]
    LayerMask WallDetection;
    [SerializeField]
    LayerMask TopSlide;
    [SerializeField]
    LayerMask PlayerLayer;


    Vector3 spawnPosition;

    Vector3 startingPosition;
    Vector3 rayFirePosition;
    Vector3 targetCenterPosition;

    bool snowBallMoving = false;
    int unitToBlockRatio = 5;
    bool canBeDestroyed = true;


    string PlayerString = "Player";
    string ProjectileString = "SnowBall";


    [SerializeField]
    Transform[] cardinalDirections;
    [SerializeField]
    Vector3[] directionVectors;
    [SerializeField]
    AudioSource snowBallAudio;
    [SerializeField]
    AudioSource snowBallRoll;
    [SerializeField]
    AudioClip pushSfx;
    [SerializeField]
    AudioClip endPushSfx;

    Animator playerAnim;



    private void Awake()
    {
        spawnPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = FindObjectOfType<SeanPlayerController>().GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        


        //Debug.DrawRay(rayFirePosition, Vector3.forward, Color.cyan);

        if (snowBallMoving)
        {
           
            if (Vector3.Distance(startingPosition, transform.position) > unitToBlockRatio)
            {
                transform.position = targetCenterPosition;
                GetComponent<BoxCollider>().enabled = false;
                snowBallMoving = false;
                PostPushCheck();
                return;
            }


            transform.position += (targetCenterPosition - startingPosition) * pushSpeed * Time.deltaTime;
            return;


        }

        if ((transform.position.y - spawnPosition.y) <= -unitToBlockRatio && canBeDestroyed == true)
        {
            DestroySnowBall();
        }
    }



    public void DestroySnowBall()
    {
        GetComponentInParent<CentralSnowBallManager>().SpawnNewSnowBall();

        Destroy(this.gameObject);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(PlayerString))
        {
            
            if (snowBallMoving == false && other.transform.position.y <= this.transform.position.y + 2.5f)
            {
                StartCoroutine(BeginPush(pushTime, other.gameObject));
            }
         
        }
        else if (other.gameObject.CompareTag(ProjectileString)) {
            DestroySnowBall();
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(PlayerString))
        {

            StopAllCoroutines();
            playerAnim.SetBool("isPushing", false);
        }
    }

    private IEnumerator BeginPush(float timeToWait, GameObject other)
    {
        StartCoroutine(pushWait(0.22f));
        playerAnim.SetBool("isPushing", true);
        yield return new WaitForSeconds(timeToWait);
        DeterminePushDirection(other);
        snowBallAudio.PlayOneShot(endPushSfx);
        

    }

    private IEnumerator pushWait(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        snowBallAudio.PlayOneShot(pushSfx);
    }

    private void PostPushCheck()
    {
        GetComponent<BoxCollider>().enabled = true;
       
    }


    private void DeterminePushDirection(GameObject other)
    {
        rayFirePosition = new Vector3(transform.position.x + 2.5f, transform.position.y + 2.5f, transform.position.z - 2.5f);
        Vector3 direction = other.transform.position - rayFirePosition;

        RaycastHit hit;
        Ray ray = new Ray(rayFirePosition, direction);

        //Raycast(rayFirePosition, direction, rayLength, PlayerLayer);
        Vector3 MovementDirection = Vector3.forward;
        float minDistance = 100;

        if ((Physics.Raycast(ray, out hit)))
        {
            Debug.Log("TARGET HIT" + direction.normalized.x + " " + (int)direction.normalized.y);




            for (int i = 0; i < cardinalDirections.Length; i++)
            {
                float distance = Vector3.Distance(hit.transform.position, cardinalDirections[i].position);
                Debug.Log(distance);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    MovementDirection = directionVectors[i];
                    Debug.Log("THIS IS THE DIRECTION " + directionVectors[i]);
                }
            }

        }


        if (!Physics.Raycast(rayFirePosition, MovementDirection, rayLength, WallDetection))//MIGHT NEED TO FIRE 2 RAYS DUE TO HALF BLOCKS BEING 2.5 high, WILL KNOW FOR SURE ONCE BUILT
        {
            if (Physics.Raycast(rayFirePosition, MovementDirection, rayLength, TopSlide))
            {
                RaycastHit hitForSlide;
                Ray raySlide = new Ray(rayFirePosition, MovementDirection);
                if ((Physics.Raycast(raySlide, out hitForSlide))) {
                    if (hitForSlide.collider.gameObject.CompareTag("TopSlide"))
                    {
                        hitForSlide.collider.gameObject.GetComponent<SlideController>().AddToTrack(this.gameObject);
                    }
                }
                snowBallRoll.Play();
            }
            else
            {
                targetCenterPosition = transform.position + (MovementDirection * unitToBlockRatio);
                startingPosition = transform.position;
                snowBallMoving = true;
                snowBallRoll.Play();
            }
        }
    }


    public void SetCanBeDestroyed(bool val)
    {

        if (val == true)
        {
            spawnPosition = transform.position;
        }

        canBeDestroyed = val;
    }


}
