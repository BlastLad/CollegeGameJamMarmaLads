using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FloorManager : MonoBehaviour
{

    public Transform[] floorLevelCameras;

    [SerializeField]
    GameObject cinemachineCameraObj;
    [SerializeField]
    GameObject maincamera;
    GameObject playerObj;


    string playerString = "Player";

    [SerializeField]
    int blockHeight = 5;


    int maxHeight = 15;


    
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
            if (this.transform.position.y - maxHeight >= 0)
            {
                return;
            }

            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + blockHeight, this.transform.position.z);
        }
        else if (playerObj.transform.position.y - this.transform.position.y <= -blockHeight)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - blockHeight, this.transform.position.z);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerString))
        {
            maincamera.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Priority = 10;
            cinemachineCameraObj.GetComponent<CinemachineFreeLook>().Priority = 11;
            PlayerCore.Instance.GetComponent<StageCameraController>().cineMachineInputProvider = cinemachineCameraObj.GetComponent<CinemachineInputProvider>();
        }
    }


}
