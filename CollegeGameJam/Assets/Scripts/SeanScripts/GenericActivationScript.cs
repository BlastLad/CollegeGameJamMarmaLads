using System.Collections;
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


    AudioSource targetAudio;
    [SerializeField]
    AudioClip audioClipToPlay;

    [SerializeField]
    GameObject objectToRotate;
    bool isRotating = false;
    [SerializeField]
    float rpm;
    [SerializeField]
    float spintime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (activationStringOverride != "")
        {
            Debug.Log("Chanegd");
            activationString = activationStringOverride;
        }


        targetAudio = GetComponent<AudioSource>();
    }




    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(activationString))
        {
            ActivateObject();
            targetAudio.PlayOneShot(audioClipToPlay);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(activationString))
        {
            ActivateObject();
            targetAudio.PlayOneShot(audioClipToPlay);
        }
    }



    private void ActivateObject()
    {
        StopAllCoroutines();
        if (!objectToActivate.active)
        {
            objectToActivate.SetActive(true);   //If the object is something that is just being activated that occurs her
        }
        else
        {
            objectToActivate.GetComponent<ActivateInterface>().Activate();    //If the object is already activated then it activates a function that uses this interface on the Object. [All will have the xxxInter name]
                                                                                
        }

        isRotating = true;
        StartCoroutine(stopRotating());

    }


    private IEnumerator stopRotating()
    {
        yield return new WaitForSeconds(spintime);
        isRotating = false;
        objectToRotate.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
           objectToRotate.transform.Rotate(0, 6.0f * rpm * Time.deltaTime, 0);
        }
    }
}
