using UnityEngine;
using System.Collections;

public class RecoilNow : StateMachineBehaviour {


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<AnimationControl>().setRecoiling(true);
    }



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Recoiling", false);
        animator.gameObject.GetComponent<AnimationControl>().setRecoiling(false);
    }

    }
