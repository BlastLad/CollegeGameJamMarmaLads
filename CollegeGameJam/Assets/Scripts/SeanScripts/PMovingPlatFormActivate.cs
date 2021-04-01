using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovingPlatFormActivate : MonoBehaviour, ActivateInterface
{

    [SerializeField]
    GameObject MovingPlat;

    public void Activate()
    {
        MovingPlat.GetComponent<MovingPlatform>().SetIsMoving(true);
    }

}
