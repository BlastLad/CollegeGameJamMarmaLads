using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAIBezierCurve : MonoBehaviour
{

    [Tooltip("This changes the angle the projectile is thrown")][SerializeField]
    float firingAngle = 45f;

    [Tooltip("This changes the force of the throw")][SerializeField]
    float gravity = 35f;

    [Tooltip("This changes the delay of the next throw")][SerializeField]
    float throwDelay = 4f; //CHANGE TO ANIMATION DURATION

    [Tooltip("Enter the prefab of the projectile you want to fire")][SerializeField]
    GameObject projectile;

    [Tooltip("Enter the transform point from which the projectile is fired")][SerializeField]
    Transform firingPoint;

    [HideInInspector]
    public bool canFireProjectiles;
    private Transform targetTransform;  // Found in start method

    private void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        canFireProjectiles = true;
        StartCoroutine(LaunchProjectiles());
    }

    private void Update()
    {
        RotateToTarget();
    }

    IEnumerator LaunchProjectiles()
    {
        //A method to throw projectiles on a loop toward the target's transform

        while(canFireProjectiles)
        {
            yield return new WaitForSeconds(throwDelay);

            GameObject newProjectile = Instantiate(projectile, firingPoint.position, Quaternion.identity) as GameObject;

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
        }
    }

    private void RotateToTarget()
    {
        Vector3 lookPoint = new Vector3
                    (targetTransform.position.x, transform.position.y, targetTransform.position.z);
        transform.LookAt(lookPoint);
    }
}
