using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallMovement : MonoBehaviour
{
    [SerializeField]
    float pushSpeed = 2f;
    [SerializeField]
    float rayLength = 5.5f;

    [SerializeField]
    LayerMask WallDetection;
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



    private void Awake()
    {
        spawnPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
                snowBallMoving = false;
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
            rayFirePosition = new Vector3(transform.position.x + 2.5f, transform.position.y + 2.5f, transform.position.z - 2.5f);

            if (!Physics.Raycast(rayFirePosition, Vector3.forward, rayLength, WallDetection))//MIGHT NEED TO FIRE 2 RAYS DUE TO HALF BLOCKS BEING 2.5 high, WILL KNOW FOR SURE ONCE BUILT
            {
                targetCenterPosition = transform.position + (Vector3.forward * unitToBlockRatio);
                startingPosition = transform.position;
                snowBallMoving = true;
            }
         
        }

    }


}
