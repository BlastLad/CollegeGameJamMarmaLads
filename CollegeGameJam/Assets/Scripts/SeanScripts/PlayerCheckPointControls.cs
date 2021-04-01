using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPointControls : MonoBehaviour
{

    GameObject currentSnowMan;
    [SerializeField]
    bool isNearSnowMan = false;
 
    // Start is called before the first frame update
    void Start()
    {

       
    }

    public void SetCheckPoint(GameObject newSnowMan)
    {
        currentSnowMan = newSnowMan;
        isNearSnowMan = true;
    }


    public void RemoveSnowMan()
    {
        currentSnowMan = null;
        isNearSnowMan = false;
    }
    public void PlaceCarrot()
    {
        
        if (isNearSnowMan && CarrotController.Instance.GetCurrentCarrots() > 0)
        {
            Debug.Log("Carrot called");
            if (!currentSnowMan.GetComponent<SnowManController>().isCarrotActive)
            {
                CarrotController.Instance.AddToCarrots(-1);
                currentSnowMan.GetComponent<SnowManController>().BuildSnowMan();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
