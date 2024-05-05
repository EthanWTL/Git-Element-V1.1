using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponGreatSword : MonoBehaviour
{

    //player
    public GameObject player;
    
    //animation
    public PlayerCombatManager playerCombatManager;
    public Animator playerAnimator;
    public RuntimeAnimatorController runtimeAnimatorController;

    private string greatSwordAttack1 = "greatSword-attack01";
    private string greatSwordAttack2 = "greatSword-attack02";
    private string greatSwordAttack3 = "greatSword-attack03";

    //VFX
    public PlayerVFXManager playerVFXManager;

    public GameObject combo1_1st;
    public Transform MeleeCombo1_1stTransform;

    public GameObject combo1_2nd;
    public Transform MeleeCombo1_2ndTransform;
    
    public GameObject combo1_3rd;
    public Transform MeleeCombo1_3rdTransform;

    public GameObject combo1_3rd_slash;
    public Transform MeleeCombo1_3rd_slashTransform;





    private void Start()
    {
        OnEquip();
    }

    public void OnEquip()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombatManager = player.GetComponent<PlayerCombatManager>();
        playerAnimator = player.GetComponent<Animator>();
        playerVFXManager = player.GetComponent<PlayerVFXManager>();

        //override animator
        playerAnimator.runtimeAnimatorController = runtimeAnimatorController;

        //assign VFX for animations
        playerVFXManager.weapon = gameObject;

        playerVFXManager.MeleeCombo1_1st = combo1_1st;
        playerVFXManager.MeleeCombo1_1stTransform = MeleeCombo1_1stTransform;
        
        playerVFXManager.MeleeCombo1_2nd = combo1_2nd;
        playerVFXManager.MeleeCombo1_2ndTransform = MeleeCombo1_2ndTransform;

        playerVFXManager.MeleeCombo1_3rd = combo1_3rd;
        playerVFXManager.MeleeCombo1_3rdTransform = MeleeCombo1_3rdTransform;

        playerVFXManager.MeleeCombo1_3rd_slash = combo1_3rd_slash;
        playerVFXManager.MeleeCombo1_3rd_slashTransform = MeleeCombo1_3rd_slashTransform;
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
