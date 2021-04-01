using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallRespawner : MonoBehaviour
{
    string PlayerString = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlayerString)
        {
            GameManager.Instance.ReSpawn();
        }
    }
}
