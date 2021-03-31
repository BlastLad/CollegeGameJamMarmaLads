using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    ParticleSystem projectileCollisionVFX;


    private void OnTriggerEnter(Collider other)
    {
        // No matter what the projectile collides with, it explodes into a particle effect
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;     //Turn off the trigger collider

        projectileCollisionVFX.Play();

        if (other.gameObject.tag == "Enemy") { return; }

        else if (other.gameObject.tag == "Player")
        {
            // When the projectile's box collider touches the player, we begin a coroutine play
            // A player animation and shortly reload the scene
            StartCoroutine(ReloadLevel(other.gameObject));
        }

        else
        {
            StartCoroutine(DestroyProjectile()); // this disables the projectile's trigger and then destroys
        }
    }


    IEnumerator DestroyProjectile()
    {
        // If the projectile hits anything besides the player, its trigger will turn off
        // And after the duration of the particle animation, it will destroy itself
        float timeTilDestroy = 1f;

        yield return new WaitForSeconds(timeTilDestroy); 
        Destroy(gameObject);
    }

    IEnumerator ReloadLevel(GameObject player)
    {
        float levelReloadBuffer = 1.5f;   // This is temporary - I would like to wait for the duration of player's "Get hit" animation
        // ADD --- PLAY "GET HIT" ANIMATION
        // ADD --- DISABLE PLAYER CONTROLS
        yield return new WaitForSeconds(levelReloadBuffer);     //We can make this the duration of the player's "Get hit" animation
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
