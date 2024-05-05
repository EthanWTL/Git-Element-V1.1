using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatManager : MonoBehaviour
{
    public PlayerController playerController;

    public bool isAttacking = false;

    public bool canCombo = false;
    public string lastAttack = "";
    public bool triggerAttack = false;



    public void OnAttack(InputAction.CallbackContext context)
    {
        if(playerController.isRolling == true | playerController.isDodging == true)
        {
            return;
        }

        triggerAttack = true;
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

    public void resetAttackSession()
    {
        lastAttack = "";
        canCombo = false;
        isAttacking = false;
    }


}
