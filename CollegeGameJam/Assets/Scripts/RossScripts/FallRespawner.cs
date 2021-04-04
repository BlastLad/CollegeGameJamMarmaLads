using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallRespawner : MonoBehaviour
{
    string PlayerString = "Player";
    [SerializeField]
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlayerString)
        {
            GameManager.Instance.ReSpawn();
            audioSource.Play();
        }
    }
}
