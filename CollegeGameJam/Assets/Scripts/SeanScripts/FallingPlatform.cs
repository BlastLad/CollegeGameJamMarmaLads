using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    string PlayerString = "Player";

    [SerializeField]
    [Tooltip("How many times the player can go on the platform before it begins falling")]
    int durability = 1;



    [SerializeField]
    float fallTime = 1.25f;

    bool hasFallen = false;

    Rigidbody rb;
    [SerializeField]
    Collider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)//A little finicky as it sometimes detect if the player rubs agianst the wall
    {
        if (other.gameObject.CompareTag(PlayerString))
        {
            SubtractFromDurability();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (hasFallen && other.gameObject.layer == 8 && other.transform.position.y < transform.position.y)//LAYER 8 IS Ground
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }


    private void SubtractFromDurability()
    {
        durability -= 1;

        //Change Model based on durability????


        if (durability <= 0)
        {
            StartCoroutine(StartFall(fallTime));
        }
    }

    private IEnumerator StartFall(float timeToWait)
    {

        //sound effect???
        //Change model????

        yield return new WaitForSeconds(timeToWait);

        rb.constraints = RigidbodyConstraints.None;

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationY |
        RigidbodyConstraints.FreezeRotationZ |
        RigidbodyConstraints.FreezePositionX |
        RigidbodyConstraints.FreezePositionZ;

        triggerCollider.enabled = false;

        hasFallen = true;



    }
}
