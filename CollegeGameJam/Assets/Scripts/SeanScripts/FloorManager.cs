using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FloorManager : MonoBehaviour
{

    public Transform[] floorLevelCameras;

    [SerializeField]
    GameObject cinemachineCameraObj;

    GameObject playerObj;

    [SerializeField]
    int blockHeight = 5;
    


    
    // Start is called before the first frame update
    void Start()
    {
        playerObj = PlayerCore.Instance.groundTarget.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj.transform.position.y - this.transform.position.y >= blockHeight)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + blockHeight, this.transform.position.x);
        }
        else if (playerObj.transform.position.y - this.transform.position.y <= -blockHeight)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - blockHeight, this.transform.position.x);
        }
    }


}
