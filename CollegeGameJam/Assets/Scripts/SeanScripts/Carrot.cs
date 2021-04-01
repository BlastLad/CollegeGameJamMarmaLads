using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{

    string PlayerString = "Player";

    int carrotNum = 1;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerString))
        {
            CollectCarrot();
        }
    }

    private void CollectCarrot()
    {
        CarrotController.Instance.AddToCarrots(carrotNum);

        Destroy(this.gameObject); //may need to be a coruotine depending on audio

    }
}
