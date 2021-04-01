using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPosGetter : MonoBehaviour
{

    [SerializeField]
    Transform blockCenter;


    public Transform GetBlockCenter()
    {
        return blockCenter;
    }
}
