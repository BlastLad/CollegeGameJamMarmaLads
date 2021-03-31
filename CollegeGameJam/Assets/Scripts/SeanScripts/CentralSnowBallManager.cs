using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralSnowBallManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject snowBallPrefab;

    [SerializeField]
    GameObject currentSnowBall;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSnowballManager()//Something to be called by other scipts if the Thing is not initally live
    {
        if (currentSnowBall != null)
        {
            Destroy(currentSnowBall);
        }
        else
        {
            SpawnNewSnowBall();
        }


    }


    public void SpawnNewSnowBall()
    {
        
        currentSnowBall = Instantiate(snowBallPrefab, transform.position, Quaternion.identity);
        currentSnowBall.transform.parent = transform;
    }
}
