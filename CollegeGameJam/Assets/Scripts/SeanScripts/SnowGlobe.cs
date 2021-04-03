using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowGlobe : MonoBehaviour
{

    string PlayerString = "Player";
    [SerializeField]
    int LevelToLoad;
    
    
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

        }
    }


    public void LoadLevel()
    {
        //Load level from build. Use GameManager?
    }
}
