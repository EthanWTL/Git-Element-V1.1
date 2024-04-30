using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    //global varibles
    public Animator animator;
    public PlayerAnimatorController playerAnimatorController;

    //input varibles
    public Vector2 move;

    //character specific variables
    public float speed;
    public float lockOnSpeed = 5f;

    //lock on related
    public GameObject lockOnTarget = null;

    //action related
    public bool isActing = false;

    //roll related
    public bool isRolling = false;
    private bool rollInput = false;
    private Vector3 rollStartPosition;
    private Vector3 rollEndPosition;
    private Vector3 rollDirection;
    private float rollDistance = 7f;
    private Vector3 rollVelocity;
    private float rollVelocityMultiplier = 5f;
    private float rollSmoothTime = 0.3f;


    //receive the left joystick movement from the player
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        rollInput = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleLockOn();
        AttemptRoll();
        translateRollingPlayer();
    }


    //using the user input to move the Player, set animation
    public void HandleMove()
    {
        if(lockOnTarget == null & isActing == false)
        {
            Vector3 movement = new Vector3(move.x, 0f, move.y);

            //rotate player
            rotatePlayer(movement);

            //move player
            translatePlayer(movement);

            //set animation accordingly
            setAnimation(movement);
        }
    }

    private void HandleLockOn()
    {
        if (lockOnTarget != null & isActing == false)
        {
            //move the player with certain speed;
            Vector3 movement = new Vector3(move.x, 0f, move.y);
            translateLockOnPlayer(movement);

            //rotate player to look at objects direction
            rotateLockOnPlayer(movement);

            //manipulate the animation
            setLockOnAnimation(movement);
        }
    }

    private void AttemptRoll()
    {
        if (rollInput == false)
        {
            return;
        }

        rollInput = false;

        if (isActing == true)
        {
            return;
        }

        isRolling = true;

        HandleRoll();
    }

    private void HandleRoll()
    {
        //handle rotation
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        rollDirection = movement.normalized;
        Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
        player.transform.rotation = playerRotation;

        //calculate rolling related variables
        rollStartPosition = player.transform.position;
        rollEndPosition = player.transform.position + rollDirection * rollDistance;
        rollVelocity = rollDirection * rollVelocityMultiplier;

        //play the animation
        playerAnimatorController.PlayActionAnimation("fist-roll-forward", true);
    }

    private void translatePlayer(Vector3 movement)
    {
        if (movement.magnitude < 0.3f & movement.magnitude > 0)
        {
            float multiplier = 0.3f / movement.magnitude;
            Vector3 minimumMovement = new Vector3(movement.x * multiplier, 0f, movement.z * multiplier);
            transform.Translate(minimumMovement * speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        
    }

    private void translateRollingPlayer()
    {
        if(isRolling == false)
        {
            return;
        }

        player.transform.position = Vector3.SmoothDamp(player.transform.position, rollEndPosition, ref rollVelocity, rollSmoothTime);
    }


    private void translateLockOnPlayer(Vector3 movement)
    {
        if (movement.magnitude < 0.5f & movement.magnitude > 0)
        {
            float multiplier = 0.5f / movement.magnitude;
            Vector3 minimumMovement = new Vector3(movement.x * multiplier, 0f, movement.z * multiplier);
            transform.Translate(minimumMovement * lockOnSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(movement * lockOnSpeed * Time.deltaTime, Space.World);
        }
    }

    private void rotatePlayer(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
        }
    }

    private void rotateLockOnPlayer(Vector3 movementInput)
    {

            Vector3 lockOnDirection = lockOnTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(lockOnDirection.x,0f, lockOnDirection.z)), 0.15f);
        
    }

    private void setAnimation(Vector3 movement)
    {
        if(movement.magnitude == 0)
        {
            animator.SetFloat("movingSpeed", -1);
        }
        else
        {
            animator.SetFloat("movingSpeed", movement.magnitude);
        }

    }

    private void setLockOnAnimation(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            animator.SetBool("isLockingOnMoving", true);

            float angle = calculateAngle(movement);
            animator.SetFloat("lockOnMovingDegree", angle);
        }
        else
        {
            animator.SetBool("isLockingOnMoving", false);
        }
            
    }

    private float calculateAngle(Vector3 movement)
    {
        Vector2 directionToObject = new Vector2(lockOnTarget.transform.position.x - transform.position.x, lockOnTarget.transform.position.z - transform.position.z);
        Vector2 movement2D = new Vector2(movement.x, movement.z);

        float angle = Vector2.SignedAngle(directionToObject, movement2D);

        if (angle >= -180 & angle <= -135)
        {
            angle = -angle;
        }

        return angle;

    }

   

}
