using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManController : MonoBehaviour
{

    string PlayerString = "Player";


    public Transform playerSpawnLocation;
    [SerializeField]
    AudioClip carrotSFX;
    [SerializeField]
    float carrotSFXVolume = 1f;

    public GameObject carrot;
    public bool isCarrotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerString))
        {
            other.gameObject.GetComponent<PlayerCheckPointControls>().SetCheckPoint(this.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerString))
        {
            other.gameObject.GetComponent<PlayerCheckPointControls>().RemoveSnowMan();
        }
    }


    public void BuildSnowMan()
    {
        GameManager.Instance.SetCurrentCheckPoint(playerSpawnLocation.position, this.gameObject);
        carrot.SetActive(true);
        isCarrotActive = true;
        //FX
        GetComponentInChildren<Animator>().SetTrigger("reached");
        AudioSource.PlayClipAtPoint(carrotSFX, 0.9f * Camera.main.transform.position
                + 0.1f * transform.position, carrotSFXVolume);
    }

}
