using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    public Animator enemyAnimator;

    public bool isIdling = false;
    public bool isWandering = false;
    public bool isPursuing = false;
    public bool isLockingOn = false;

    #region idle
    private float idleDuration;
    #endregion

    #region Wandering
    private float wanderingSpeed = 3f;
    private Vector3 wanderingDirection;
    private float wanderingDuration;
    #endregion

    private void Start()
    {
        StartCoroutine(idleTime());
    }

    private void Update()
    {
        HandleWander();
    }

    IEnumerator idleTime()
    {
        isIdling = true;
        initializingIdling();

        yield return new WaitForSeconds(idleDuration);

        isIdling = false;
        StartCoroutine(wanderTime());
    }
    IEnumerator wanderTime()
    {
        isWandering = true;
        initializingWandering();

        yield return new WaitForSeconds(wanderingDuration);

        isWandering = false;
        StartCoroutine(idleTime());

    }

    private void initializingIdling()
    {
        //wandering time
        idleDuration = Random.Range(0f, 3f);
        enemyAnimator.SetBool("isIdling", true);
    }

    private void initializingWandering()
    {
        //wandering direction
        float dirX = Random.Range(-1.0f, 1.0f);
        float dirZ = Random.Range(-1.0f, 1.0f);
        wanderingDirection = new Vector3(dirX, 0, dirZ).normalized;

        //wandering time
        wanderingDuration = Random.Range(2f, 3f);

        //animation
        setUpWanderingAnimation();
    }

    private void HandleWander()
    {
        if (isWandering == true)
        {
            translateWanderingEnemy();
            rotateWanderingEnemy();
        }
       
    }

    private void translateWanderingEnemy()
    {
        transform.Translate(wanderingDirection * wanderingSpeed * Time.deltaTime, Space.World);
    }

    private void rotateWanderingEnemy()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(wanderingDirection), 0.15f);
    }

    private void setUpWanderingAnimation()
    {
        enemyAnimator.SetBool("isIdling", false);
        enemyAnimator.CrossFade("sword-walk-forward", 0.2f);
    }

}
