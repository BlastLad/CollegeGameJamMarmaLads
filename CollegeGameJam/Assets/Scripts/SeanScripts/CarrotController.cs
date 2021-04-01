using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrotController : MonoBehaviour
{
    public static CarrotController Instance { get; private set; }



    [SerializeField]
    int startingCarrots = 0;
    int currentCarrots;

    [SerializeField]
    Text carrotText;

    private void Awake()
    {
        if(Instance != null)
        {
          Debug.LogError("ERROR THERE ARE MULTIPLE CARROT CONTROLLERS IN THIS SCENE");
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCarrots = startingCarrots;
        carrotText.text = "" + currentCarrots;
    }

    public int GetCurrentCarrots()
    {
        return currentCarrots;
    }



    public void AddToCarrots(int numToAdd)//pos or negative
    {
        currentCarrots += numToAdd;

        carrotText.text = "" + currentCarrots;
    }
}
