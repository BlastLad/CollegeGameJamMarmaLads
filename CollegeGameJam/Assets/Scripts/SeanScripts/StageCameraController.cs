using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class StageCameraController : MonoBehaviour
{
    //A Script for the controlling of the camera in all Diorama Scenes


    PlayerActionMap playerActionMap;


    [SerializeField]
    InputActionReference controllerActionMap;
    [SerializeField]
    InputActionReference mouseActionMap;

    
    public CinemachineInputProvider cineMachineInputProvider;
    
  


    private void Awake()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        playerActionMap = gameObject.GetComponent<SeanPlayerController>().GetPlayerActionMap();

        playerActionMap.Default.MouseButtonActivate.started += ctx => ActivateMouseCameraControl();
        playerActionMap.Default.MouseButtonActivate.canceled += ctx => DisableMouseCameraControl();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


    private void ActivateMouseCameraControl()
    {
        cineMachineInputProvider.XYAxis = mouseActionMap;

  
        cineMachineInputProvider.gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 1.6f;
    }

    public void DisableMouseCameraControl()
    {
        cineMachineInputProvider.XYAxis = controllerActionMap;

     
        cineMachineInputProvider.gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 2;
    }

   
}
