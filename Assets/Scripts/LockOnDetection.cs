using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnDetection : MonoBehaviour
{
    public PlayerController playerController;
    public List<GameObject> detectedObjects = new List<GameObject>();
    public Animator animator;

    public Transform aim;
    private Vector3 velocity = Vector3.zero;
    private Vector3 aimInitialOffset;

    private void Start()
    {
        aimInitialOffset = aim.localPosition - transform.localPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            detectedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        detectedObjects.Remove(other.gameObject);
    }

    public void OnLockOn(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            handleLockOnStart();
        }
        else if (context.canceled)
        {
            handleLockOnFinish();
        }
    }

    private void Update()
    {
        if (playerController.lockOnTarget != null)
        {
            setUpHeadAim(playerController.lockOnTarget.transform.position);
        }
        else
        {
            resetHeadAim();
        }
    }

    private void handleLockOnStart()
    {
        playerController.lockOnTarget = FindNearestLockOnableObject();

        if (playerController.lockOnTarget != null)
        {
            animator.SetBool("isLockingOn", true);
            playerController.lockOnTarget.GetComponent<EnemyLockOnHandler>().isLockedOn = true;
        }
    }
    private void handleLockOnFinish()
    {
        animator.SetBool("isLockingOn", false);

        if (playerController.lockOnTarget != null)
        {
            playerController.lockOnTarget.GetComponent<EnemyLockOnHandler>().isLockedOn = false;
            playerController.lockOnTarget = null;
        }
            
    }

    private GameObject FindNearestLockOnableObject()
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject target in detectedObjects)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestTarget = target;
                closestDistance = distance;
            }
        }
        return closestTarget;
    }

    private void setUpHeadAim(Vector3 position)
    {
        aim.position = Vector3.SmoothDamp(aim.position, position, ref velocity, 0.2f);
    }

    private void resetHeadAim()
    {
        aim.localPosition = Vector3.SmoothDamp(aim.localPosition, aimInitialOffset, ref velocity, 0.2f);
    }
}
