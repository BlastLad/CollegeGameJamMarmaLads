using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitioner : MonoBehaviour
{
    Animator animator;

    [Tooltip("Enter the name of the scene you want to load after this")][SerializeField]
    string nextSceneToLoad;

    [SerializeField]
    AudioClip levelFadeOutSFX;
    [SerializeField]
    float levelFadeOutSFXVolume;

    float sceneTransitionTime = 2.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator LoadNextScene()
    {
        animator.SetTrigger("start");
        AudioSource.PlayClipAtPoint(levelFadeOutSFX, Camera.main.transform.position, levelFadeOutSFXVolume);
        yield return new WaitForSeconds(sceneTransitionTime);
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
