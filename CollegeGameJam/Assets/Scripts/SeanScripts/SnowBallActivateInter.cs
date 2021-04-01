using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallActivateInter : MonoBehaviour, ActivateInterface
{
    public void Activate()
    {
        GetComponent<CentralSnowBallManager>().ActivateSnowballManager();
    }
}
