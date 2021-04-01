using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    Vector3 SpawnPosition;
    Vector3 currentCheckPoint;
    
    
    
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
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetSpawnPosition(Vector3 spawnPos)
    {
        SpawnPosition = spawnPos;
        currentCheckPoint = spawnPos;
    }


    public void SetCurrentCheckPoint(Vector3 spawnPos)
    {
        currentCheckPoint = spawnPos;
    }
}
