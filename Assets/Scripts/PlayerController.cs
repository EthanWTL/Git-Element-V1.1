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

    //lock on related //For Future note: add the isLockingOn flag for the player
    public GameObject lockOnTarget = null;

    //action related
    public bool isActing = false;

    //roll related
    public bool isRolling = false;
    private bool rollInput = false;
    private Vector3 rollEndPosition;
    private Vector3 rollDirection;
    private float rollDistance = 10f;
    private Vector3 rollVelocity;
    private float rollVelocityMultiplier = 5f;
    private float rollSmoothTime = 0.3f;

    //dodge related
    public bool isDodging = false;
    private Vector3 dodgeEndPosition;
    private Vector3 dodgeDirection;
    private float dodgeDistance = 4f;
    private Vector3 dodgeVelocity;
    private float dodgeVelocityMultiplier = 10f;
    private float dodgeSmoothTime = 0.1f;


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
        translateDodgingPlayer();


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
        //is no input for the roll or dodge, do nothing
        if (rollInput == false)
        {
            return;
        }

        //once there is input for attemp roll, reset the rollinput flag
        rollInput = false;

        //if player is acting, then don't do anything
        if (isActing == true)
        {
            return;
        }

        //check we should roll or dodge, set up the rolling or dodging flag
        RollOrDodge();
    }

    private void RollOrDodge()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        if (lockOnTarget != null)
        {

            float angle = calculateAngle(movement);
            if (angle < 30 & angle > -30)
            {
                isRolling = true;
                HandleRoll(movement);
            }
            else
            {
                isDodging = true;
                HandleDodge(angle, movement);
            }
        }
        else
        {
            if (move == Vector2.zero)
            {
                isDodging = true;
                HandleDodge(180f, -transform.forward);
            }
            else
            {
                isRolling = true;
                HandleRoll(movement);
            }

        }
    }

    private void HandleRoll(Vector3 movement)
    {
        //handle rotation
        rollDirection = movement.normalized;
        Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
        player.transform.rotation = playerRotation;

        //calculate rolling related variables
        rollEndPosition = player.transform.position + rollDirection * rollDistance;
        rollVelocity = rollDirection * rollVelocityMultiplier;

        //play the animation
        isActing = true;
        animator.CrossFade("fist-diveRoll-forward", 0.2f);
    }

    private void HandleDodge(float dodgeAngle, Vector3 movement)
    {
        //no rotation
        //calculate dodge related variables
        dodgeDirection = movement.normalized;
        dodgeEndPosition = player.transform.position + dodgeDirection * dodgeDistance;
        dodgeVelocity = dodgeDirection * dodgeVelocityMultiplier;

        //play related animation and set flag
        isActing = true;
        //set the currect animation base on the angle.
        setDodgeAnimation(dodgeAngle);
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

    private void translateDodgingPlayer()
    {

        if(isDodging == false)
        {
            return;
        }

        player.transform.position = Vector3.SmoothDamp(player.transform.position, dodgeEndPosition, ref dodgeVelocity, dodgeSmoothTime);
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

    private void setDodgeAnimation(float dodgeAngle)
    {
        if (dodgeAngle >= 30f & dodgeAngle <= 105f)
        {
            animator.CrossFade("fist-dodge-left",0.2f);
        } else if (dodgeAngle >= -105f & dodgeAngle <= -30)
        {
            animator.CrossFade("fist-dodge-right",0.2f);
        }
        else
        {
            animator.CrossFade("fist-dodge-backward", 0.2f);
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
