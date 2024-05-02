using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGreatSword : MonoBehaviour
{
    public RuntimeAnimatorController runtimeAnimatorController;
    public Animator playerAnimator;

    private void Start()
    {
        playerAnimator.runtimeAnimatorController = runtimeAnimatorController;
    }

}
