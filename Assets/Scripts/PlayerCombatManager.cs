using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatManager : MonoBehaviour
{
    public PlayerController playerController;

    public bool isAttacking = false;

    //meleeCombo1
    public bool canCombo = false;
    public string lastAttack = "";
    public bool triggerAttack = false;

    //meleeCharge1
    public string lastChargeAnimation = "";
    public bool triggerCharge = false;
    public bool endCharge = false;
    public float chargeTime;



    public void OnAttack(InputAction.CallbackContext context)
    {
        if(playerController.isRolling == true | playerController.isDodging == true)
        {
            return;
        }

        triggerAttack = true;
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            chargeTime = 0;
            triggerCharge = true;
            
        }
        else if (context.canceled)
        {
            triggerCharge = false;
            endCharge = true;
        }
        
    }


    public void resetAttackSession()
    {
        lastAttack = "";
        canCombo = false;
        isAttacking = false;
    }


    public void resetChargeAttack()
    {
        triggerCharge = false;
        endCharge = false;
        chargeTime = 0;
    }

    public void resetPlayerCombatManager()
    {
        lastAttack = "";
        canCombo = false;
        isAttacking = false;

        lastChargeAnimation = "";
        triggerCharge = false;
        endCharge = false;
        chargeTime = 0f;
    }


    public void turnOnCanCombo()
    {
        canCombo = true;
    }

    public void turnOffCanCombo()
    {
        canCombo = false;
    }

    public void startAttack()
    {
        isAttacking = true;
    }

    public void endAttack()
    {
        isAttacking = false;
    }

    public void enableRoll()
    {
        playerController.canRoll = true;

    }

    public void disableRoll()
    {
        playerController.canRoll = false;
    }



    public void disableMove()
    {
        playerController.canMove = false;
    }

    public void enableMove()
    {
        playerController.canMove = true;
    }


}
