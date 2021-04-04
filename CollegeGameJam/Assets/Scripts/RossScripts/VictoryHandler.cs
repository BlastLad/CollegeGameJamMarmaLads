using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryHandler : MonoBehaviour
{

    Transitioner transitioner;


    [SerializeField]
    AudioClip goalSFX;
    [SerializeField]
    float goalSFXVolume=3;

    private void Start()
    {
        transitioner = FindObjectOfType<Transitioner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // When the player comes within the enemy's trigger radius, we will trigger LevelComplete()
        {
            StartCoroutine(LevelComplete());
        }
    }

    IEnumerator LevelComplete()
    {
        GetComponent<EnemyAI>().canFireProjectiles = false;
        AudioSource.PlayClipAtPoint(goalSFX, 0.9f * Camera.main.transform.position
            + 0.1f * transform.position, goalSFXVolume);
        float levelCompleteBuffer = 1.5f;       // Must be enough time to play animations and SFX

        // ADD --- Play victory animations and sound effects
        yield return new WaitForSeconds(levelCompleteBuffer);
        StartCoroutine(transitioner.LoadNextScene());
    }
}
