using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponGreatSword : MonoBehaviour
{

    //player
    private GameObject player;
    
    //animation
    private PlayerCombatManager playerCombatManager;
    private Animator playerAnimator;
    public RuntimeAnimatorController runtimeAnimatorController;

    private string greatSwordAttack1 = "greatSword-attack01";
    private string greatSwordAttack2 = "greatSword-attack02";
    private string greatSwordAttack3 = "greatSword-attack03";

    private string greatSwordChargeAttackStartCharge = "greatSword-chargeAttack-startCharge";
    private string greatSwordChargeAttackPhase1 = "greatSword-chargeAttack-phase1";
    private string greatSwordChargeAttackPhase2 = "greatSword-chargeAttack-phase2";

    //VFX
    #region
    private PlayerVFXManager playerVFXManager;

    public GameObject combo1_spark;
    public Transform MeleeCombo1_sparkTransform;

    public GameObject combo1_1st_slashSmoke;
    public Transform MeleeCombo1_1st_slashSmokeTransform;

    public GameObject combo1_1st;
    public Transform MeleeCombo1_1stTransform;

    public GameObject combo1_2nd;
    public Transform MeleeCombo1_2ndTransform;

    public GameObject combo1_2nd_slashSmoke;
    public Transform MeleeCombo1_2nd_slashSmokeTransform;

    public GameObject combo1_3rd;
    public Transform MeleeCombo1_3rdTransform;

    public GameObject combo1_3rd_slash;
    public Transform MeleeCombo1_3rd_slashTransform;

    public GameObject combo1_3rd_slashSmoke;
    public Transform MeleeCombo1_3rd_slashSmokeTransform;

    public GameObject combo1_3rd_ground;
    public Transform MeleeCombo1_3rd_groundTransform;

    public GameObject charge_energyin_level1;
    public Transform MeleeCharge_energyin_level1Transform;
    
    public GameObject charge_energyin_level2;
    public Transform MeleeCharge_energyin_level2Transform;
    #endregion

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


        //assign weapon animation to playervfxmanager
        #region
        //assign VFX for animations
        playerVFXManager.weapon = gameObject;

        playerVFXManager.MeleeCombo1_spark = combo1_spark;
        playerVFXManager.MeleeCombo1_sparkTransform = MeleeCombo1_sparkTransform;

        playerVFXManager.MeleeCombo1_1st_slashSmoke = combo1_1st_slashSmoke;
        playerVFXManager.MeleeCombo1_1st_slashSmokeTransForm = MeleeCombo1_1st_slashSmokeTransform;

        playerVFXManager.MeleeCombo1_1st = combo1_1st;
        playerVFXManager.MeleeCombo1_1stTransform = MeleeCombo1_1stTransform;

        playerVFXManager.MeleeCombo1_2nd = combo1_2nd;
        playerVFXManager.MeleeCombo1_2ndTransform = MeleeCombo1_2ndTransform;

        playerVFXManager.MeleeCombo1_2nd_slashSmoke = combo1_2nd_slashSmoke;
        playerVFXManager.MeleeCombo1_2nd_slashSmokeTransForm = MeleeCombo1_2nd_slashSmokeTransform;

        playerVFXManager.MeleeCombo1_3rd = combo1_3rd;
        playerVFXManager.MeleeCombo1_3rdTransform = MeleeCombo1_3rdTransform;

        playerVFXManager.MeleeCombo1_3rd_slash = combo1_3rd_slash;
        playerVFXManager.MeleeCombo1_3rd_slashTransform = MeleeCombo1_3rd_slashTransform;

        playerVFXManager.MeleeCombo1_3rd_slashSmoke = combo1_3rd_slashSmoke;
        playerVFXManager.MeleeCombo1_3rd_slashSmokeTransForm = MeleeCombo1_3rd_slashSmokeTransform;

        playerVFXManager.MeleeCombo1_3rd_ground = combo1_3rd_ground;
        playerVFXManager.MeleeCombo1_3rd_groundTransform = MeleeCombo1_3rd_groundTransform;

        playerVFXManager.MeleeCharge_Energyin_level1 = charge_energyin_level1;
        playerVFXManager.MeleeCharge_Energyin_level1Transform = MeleeCharge_energyin_level1Transform;

        playerVFXManager.MeleeCharge_Energyin_level2 = charge_energyin_level2;
        playerVFXManager.MeleeCharge_Energyin_level2Transform = MeleeCharge_energyin_level2Transform;
        #endregion

    }


    private void Update()
    {
        HandleMeleeCombo1();
        HandleCharge();
    }



    private void HandleMeleeCombo1()
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
    private void HandleCharge()
    {
        if (playerCombatManager.triggerCharge == true)
        {
            playerCombatManager.chargeTime += Time.deltaTime;
            performCharge(playerCombatManager.chargeTime);
        }

        if(playerCombatManager.endCharge == true)
        {
            playerCombatManager.endCharge = false;
            StartCoroutine(ChargeAttack(playerCombatManager.chargeTime));
            playerCombatManager.chargeTime = 0;
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

    private void performCharge(float chargeTime)
    {
        //play animation and set up flagger base on chargeTime
        if(chargeTime >= 0.5f)
        {
            if(playerCombatManager.lastChargeAnimation == "")
            {
                playerCombatManager.startAttack();
                playerAnimator.CrossFade(greatSwordChargeAttackStartCharge, 0.1f);
                playerCombatManager.lastChargeAnimation = greatSwordChargeAttackStartCharge;
            }
            
            if(chargeTime > 2.5f)
            {
                playerVFXManager.playMeleeCharge1_energyin_level2();
                Destroy(playerVFXManager.charge_energyin_level1);

            }
        }
    }

    IEnumerator ChargeAttack(float chargeTime)
    {
        if (chargeTime >= 0.5f & chargeTime < 1.2f)
        {
            playerCombatManager.disableRoll();
            yield return new WaitForSeconds(1.2f - chargeTime);
            playerAnimator.CrossFade(greatSwordChargeAttackPhase1, 0.1f);
            playerCombatManager.lastChargeAnimation = greatSwordChargeAttackPhase1;
        }
        else if(chargeTime >= 1.2f & chargeTime < 2.5f)
        {
            playerCombatManager.disableRoll();
            playerAnimator.CrossFade(greatSwordChargeAttackPhase1, 0.1f);
            playerCombatManager.lastChargeAnimation = greatSwordChargeAttackPhase1;
        }
        else if(chargeTime > 2.5f)
        {
            playerCombatManager.disableRoll();
            playerAnimator.CrossFade(greatSwordChargeAttackPhase2, 0.1f);
            playerCombatManager.lastChargeAnimation = greatSwordChargeAttackPhase2;
        }
    }

}
