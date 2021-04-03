using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddToTrack(GameObject snowBall)
    {
        snowBall.GetComponent<SnowBallMovement>().SetCanBeDestroyed(false);
        snowBall.GetComponent<Rigidbody>().useGravity = false;
        GetComponent<SlideMovement>().SetObjectToMove(snowBall);
        
    }
}
