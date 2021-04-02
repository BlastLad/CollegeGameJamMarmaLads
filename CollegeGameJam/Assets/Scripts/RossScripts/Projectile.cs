using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    ParticleSystem projectileCollisionVFX;

    [SerializeField]
    AudioClip playerImpactSFX;
    [SerializeField]
    float playerImpactVolume = 1f;
    [SerializeField]
    AudioClip groundImpactSFX;
    [SerializeField]
    float groundImpactVolume = 1f;


    private void OnCollisionEnter(Collision other)
    {
        // No matter what the projectile collides with, it explodes into a particle effect

        if (other.gameObject.tag == "Enemy") { return; }

        else if (other.gameObject.tag == "Player")
        {
            // When the projectile's box collider touches the player, we begin a coroutine play
            // A player animation and shortly reload the scene
            StartCoroutine(ReloadLevel(other.gameObject));
            AudioSource.PlayClipAtPoint(playerImpactSFX, 0.9f * Camera.main.transform.position
                + 0.1f * transform.position, playerImpactVolume);
        }

        else
        {
            AudioSource.PlayClipAtPoint(groundImpactSFX, 0.9f * Camera.main.transform.position
                + 0.1f * transform.position, groundImpactVolume);
            StartCoroutine(DestroyProjectile()); // this disables the projectile's trigger and then destroys
        }
    }


    public IEnumerator DestroyProjectile()
    {
        // If the projectile hits anything besides the player, its trigger will turn off
        // And after the duration of the particle animation, it will destroy itself
        float timeTilDestroy = 1.7f;
        HideSnowballAndPlayAnimation();

        yield return new WaitForSeconds(timeTilDestroy);
        Destroy(gameObject);
    }

    IEnumerator ReloadLevel(GameObject player)
    {
        HideSnowballAndPlayAnimation();
        SeanPlayerController.Instance.canMove = false;
        float levelReloadBuffer = 1.5f;   // This is temporary - I would like to wait for the duration of player's "Get hit" animation
        // ADD --- PLAY "GET HIT" ANIMATION
        // ADD --- DISABLE PLAYER CONTROLS
        yield return new WaitForSeconds(levelReloadBuffer);     //We can make this the duration of the player's "Get hit" animation
        Debug.Log("REACHED THIS ASWELL");

        GameManager.Instance.ReSpawn();
    }

    private void HideSnowballAndPlayAnimation()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        projectileCollisionVFX.Play();
    }
}
