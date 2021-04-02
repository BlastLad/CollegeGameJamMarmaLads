﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryHandler : MonoBehaviour
{
    [Tooltip("Enter name of the scene in the build you want to load when enemy is touched.")]
    [SerializeField]
    string nextSceneToLoad;


    [SerializeField]
    AudioClip agitatedSFX;
    [SerializeField]
    float agitatedSFXVolume = 10f;

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
        AudioSource.PlayClipAtPoint(agitatedSFX, 0.9f * Camera.main.transform.position
            + 0.1f * transform.position, agitatedSFXVolume);
        float levelCompleteBuffer = 1.5f;       // Must be enough time to play animations and SFX

        // ADD --- Play victory animations and sound effects
        yield return new WaitForSeconds(levelCompleteBuffer);
        SceneManager.LoadScene(nextSceneToLoad);
    }
}