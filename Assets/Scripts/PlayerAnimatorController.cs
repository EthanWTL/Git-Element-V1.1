using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    public PlayerController playerController;

    public void PlayActionAnimation(string targetAnimation, bool isActing)
    {
        playerController.animator.CrossFade(targetAnimation, 0.2f);
        playerController.isActing = isActing;
    }

}
