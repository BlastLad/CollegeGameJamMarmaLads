using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    Vector3 SpawnPosition;
    Vector3 currentCheckPoint;
    GameObject currentSnowMan;
    [SerializeField]
    AudioClip levelLoadSfx;
    [SerializeField]
    AudioClip levelclearSfx;
    AudioSource gameManagerSource;
    

    [SerializeField]
    AudioSource mainCameraAudio;
    [SerializeField]
    AudioClip LevelTheme;
    [SerializeField]
    float loopStart;
    [SerializeField]
    float loopEnd;

    [SerializeField]
    int buildNum = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("ERROR MORE THAN ONE GAME MANAGER IN THIS SCENE");
        }
        else
        {
            Instance = this;
            if (mainCameraAudio != null)
            {
         
                PlaySoundInterval(0, loopEnd);
            }
        }


        gameManagerSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameManagerSource.PlayOneShot(levelLoadSfx);
    }

    public void PlayLevelClearSFX()
    {
        gameManagerSource.PlayOneShot(levelclearSfx);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCameraAudio != null)
        {
            if (mainCameraAudio.isPlaying == false)
            {
                mainCameraAudio.clip = LevelTheme;
                PlaySoundInterval(loopStart, loopEnd);
            }
        }
    }

    public void ReSpawn()
    {
        SeanPlayerController.Instance.transform.position = currentCheckPoint;
        SeanPlayerController.Instance.canMove = true;
    }

    public void SetSpawnPosition(Vector3 spawnPos)
    {
        Debug.Log("REACHED");
        SpawnPosition = spawnPos;
        currentCheckPoint = spawnPos;
    }


    public void SetCurrentCheckPoint(Vector3 spawnPos, GameObject snowMan)
    {

        if (currentSnowMan != null)
        {
            currentSnowMan.GetComponent<SnowManController>().carrot.SetActive(false);
            currentSnowMan.GetComponent<SnowManController>().isCarrotActive = false;

        }

        currentCheckPoint = spawnPos;
        currentSnowMan = snowMan;
    }


    void PlaySoundInterval(float fromSeconds, float toSeconds)
    {
        mainCameraAudio.time = fromSeconds;
        mainCameraAudio.Play();
        mainCameraAudio.SetScheduledEndTime(AudioSettings.dspTime + (toSeconds - fromSeconds));

    }


    public void ReloadLevel()
    {
       // Time.timeScale = 0f;
        SceneManager.LoadScene(buildNum);
    }

    public void NextLevel(int num)
    {
        SceneManager.LoadScene(num);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
