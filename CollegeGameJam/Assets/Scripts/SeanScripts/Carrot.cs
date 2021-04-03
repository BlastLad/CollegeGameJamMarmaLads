using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{

    string PlayerString = "Player";

    int carrotNum = 1;
    // Start is called before the first frame update
    [SerializeField]
    GameObject childObj;
    AudioSource carrotAudio;


    private void Awake()
    {
        carrotAudio = GetComponent<AudioSource>();
    }

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
        carrotAudio.Play();
        GetComponent<Collider>().enabled = false;
        childObj.SetActive(false);
        //Destroy(this.gameObject); //may need to be a coruotine depending on audio
        StartCoroutine(destroycarrot(1f));

    }


    private IEnumerator destroycarrot(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(this.gameObject);
    }
}
