using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 lockOnTarget;

    public PlayerController playerController;

    public float smoothTime = 0.3f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if(playerController.lockOnTarget != null)
            {
                handleLockOnCamera();
            }
            else
            {
                handleNormalCamera();
            }
            
        }
    }

    void handleLockOnCamera()
    {
        lockOnTarget = (target.position + playerController.lockOnTarget.transform.position) / 2f;
        transform.position = Vector3.SmoothDamp(transform.position, lockOnTarget + offset, ref velocity, smoothTime);
    }

    void handleNormalCamera()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
