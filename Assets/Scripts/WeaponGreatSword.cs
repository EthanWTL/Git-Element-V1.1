using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponGreatSword : MonoBehaviour
{

    public PlayerCombatManager playerCombatManager;
    public Animator playerAnimator;
    public RuntimeAnimatorController runtimeAnimatorController;

    private string greatSwordAttack1 = "greatSword-attack01";
    private string greatSwordAttack2 = "greatSword-attack02";
    private string greatSwordAttack3 = "greatSword-attack03";

    


    private void Start()
    {
        OnEquip();
    }

    public void OnEquip()
    {
        playerAnimator.runtimeAnimatorController = runtimeAnimatorController;
    }


    private void Update()
    {

        if (playerCombatManager.triggerAttack == false)
        {
            return;
        }

        if (playerCombatManager.triggerAttack == true)
        {

            playerCombatManager.triggerAttack = false;
            performAttack();
        }
    }

    private void performAttack()
    {
        if(playerCombatManager.lastAttack == "")
        {
            setUpAttack();
            doAttack(greatSwordAttack1);
            return;
        }
        else if(playerCombatManager.lastAttack == greatSwordAttack1 & playerCombatManager.canCombo == true)
        {
            setUpAttack();
            doAttack(greatSwordAttack2);
            return;
        }
        else if(playerCombatManager.lastAttack == greatSwordAttack2 & playerCombatManager.canCombo == true)
        {
            setUpAttack();
            doAttack(greatSwordAttack3);
            return;
        }
        
    }

    private void setUpAttack()
    {
        playerCombatManager.turnOffCanCombo();
        playerCombatManager.disableRoll();
    }

    private void doAttack(string attackName)
    {
        playerCombatManager.startAttack();
        playerAnimator.CrossFade(attackName, 0.2f);
        playerCombatManager.lastAttack = attackName;
    }


}
