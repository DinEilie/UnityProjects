using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFire : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime >= 1 && Input.GetButton("Fire1") != true){
            animator.SetBool("isFiring", false);
            GameObject.FindWithTag("Player").GetComponent<Shooter>().SetIsFiring(false);
        }
    }
}
