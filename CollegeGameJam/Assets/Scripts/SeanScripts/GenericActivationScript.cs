﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActivationScript : MonoBehaviour
{

    string activationString = "SnowBall";

    [SerializeField]
    GameObject objectToActivate;

    [Tooltip("Add a Tag name here to override the required tag for the activation collision evet DEFAULT [SnowBall]")]
    [SerializeField]
    string activationStringOverride;

    
    
    // Start is called before the first frame update
    void Start()
    {
        if (activationStringOverride != "")
        {
            Debug.Log("Chanegd");
            activationString = activationStringOverride;
        }
    }




    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(activationString))
        {
            ActivateObject();
        }
    }



    private void ActivateObject()
    {
        if (!objectToActivate.active)
        {
            objectToActivate.SetActive(true);   //If the object is something that is just being activated that occurs her
        }
        else
        {
            objectToActivate.GetComponent<ActivateInterface>().Activate();    //If the object is already activated then it activates a function that uses this interface on the Object. [All will have the xxxInter name]
                                                                                
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}