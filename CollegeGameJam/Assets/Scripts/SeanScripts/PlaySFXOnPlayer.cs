using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXOnPlayer : MonoBehaviour
{


    string PlayerString ="Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerString)) {
            GetComponent<AudioSource>().Play();

            //other.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.up * 15, ForceMode.Impulse);
        }
    }
}
