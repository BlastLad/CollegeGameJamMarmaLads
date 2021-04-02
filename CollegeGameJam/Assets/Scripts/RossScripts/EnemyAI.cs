using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [Header("Attack")]
    [Tooltip("This changes the angle the projectile is thrown")]
    [SerializeField]
    float firingAngle = 45f;

    [Tooltip("This changes the force of the throw")]
    [SerializeField]
    float gravity = 35f;

    [Tooltip("The duration of this animation")]
    [SerializeField]
    float animLength = 1.81f;

    [Tooltip("The duration of this animation")]
    [SerializeField]
    float throwDelay = 2.5f;

    [Tooltip("Enter the prefab of the projectile you want to fire")]
    [SerializeField]
    GameObject projectile;

    [Tooltip("Enter the transform point from which the projectile is fired")]
    [SerializeField]
    Transform firingPoint;

    [Header("Crosshairs")]
    [SerializeField]
    GameObject crosshairs;

    [SerializeField] float crosshairYOffset = 4.5f;

    private Transform targetTransform;  // Found in start method
    [HideInInspector]
    public bool canFireProjectiles;
    private bool isFlying;

    Animator enemyAnimator;
    string IsThrowing = "isThrowing";

    [Header("Audio Clips")]
    [SerializeField]
    AudioClip throwSFX;
    [SerializeField]
    float throwSFXVolume = 10f;

    private void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        canFireProjectiles = true;
        isFlying = false;
        enemyAnimator = GetComponentInParent<Animator>();

        StartCoroutine(LaunchProjectiles());
    }

    private void Update()
    {
        RotateToTarget();
        ShowCrosshairs();
    }

    IEnumerator LaunchProjectiles()
    {
        //A method to throw projectiles on a loop toward the target's transform

        while (canFireProjectiles)
        {
            enemyAnimator.SetBool(IsThrowing, true);
            yield return new WaitForSeconds(animLength);
            enemyAnimator.SetBool(IsThrowing, false);

            isFlying = true;

            GameObject newProjectile = Instantiate(projectile, firingPoint.position, Quaternion.identity) as GameObject;

            AudioSource.PlayClipAtPoint(throwSFX, 0.9f * Camera.main.transform.position
                + 0.1f * transform.position, throwSFXVolume);   //This code makes the sound volume more adjustable

            float targetDistance = Vector3.Distance(newProjectile.transform.position, targetTransform.position); // Calculate distance to player
            float projectileVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity); // Calculate velocity needed to hit target

            //Extract the x and y component of the velocity
            float velocityX = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float velocityY = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            float flightDuration = targetDistance / velocityX;    // calculate flight time

            newProjectile.transform.rotation = Quaternion.LookRotation(targetTransform.position - newProjectile.transform.position); // Get rotation

            float elapseTime = 0f;

            while (elapseTime < flightDuration)
            {
                newProjectile.transform.Translate(0, (velocityY - (gravity * elapseTime)) * Time.deltaTime, velocityX * Time.deltaTime);

                elapseTime += Time.deltaTime;
                yield return null;
            }
            StartCoroutine(newProjectile.GetComponent<Projectile>().DestroyProjectile());
            isFlying = false;
            yield return new WaitForSeconds(throwDelay);
        }
    }

    private void RotateToTarget()
    {
        Vector3 lookPoint = new Vector3
                    (targetTransform.position.x, transform.position.y, targetTransform.position.z);
        transform.LookAt(lookPoint);
    }

    private void ShowCrosshairs()
    {
        Vector3 aboveHead = new Vector3(targetTransform.position.x, targetTransform.position.y + crosshairYOffset, targetTransform.position.z);
        crosshairs.transform.position=aboveHead;
        if(!isFlying)
        {
            crosshairs.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            crosshairs.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
