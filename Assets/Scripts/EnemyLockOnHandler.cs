using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOnHandler : MonoBehaviour
{
    public GameObject lockOnIndicator;
    public bool isLockedOn = false;

    private Vector3 startingPosition;
    float floatSpeed = 4f;
    float floatDistance = 0.3f;
    float rotationSpeed = 180f;

    private void Start()
    {
        startingPosition = lockOnIndicator.transform.position;
    }

    private void Update()
    {
        handleLockOnCheck();
        handleIndicatorFloating();
    }

    void handleLockOnCheck()
    {
        if (isLockedOn)
        {
            lockOnIndicator.SetActive(true);
        }
        else
        {
            lockOnIndicator.SetActive(false);
        }
    }

    void handleIndicatorFloating()
    {
        if (isLockedOn)
        {
            float newY = startingPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatDistance;
            lockOnIndicator.transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            lockOnIndicator.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        } 
    }
}
