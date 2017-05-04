using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IxtabInvisible : StateMachineBehaviour {

	IxtabAI ai;

	float invisTimer = 0.0f;
	float maxTimer = 10.0f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ai = animator.transform.parent.GetComponent<IxtabAI>();
		ai.GetComponent<Animator>().SetBool("Invisible", true);
		ai.setInvisible(true);
		invisTimer = 0.0f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.IsName("Invisible_2")) {
			if (invisTimer > maxTimer) {
				animator.SetTrigger("Choke");
			} else {
				invisTimer += Time.deltaTime;
			}
		}
		if (Random.Range(0.0f, 1.0f) > 0.9f) {
			ai.Wander();
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.IsName("Invisible_2")){
			ai.GetComponent<Animator>().SetBool("Invisible", false);
			ai.setInvisible(false);
		}
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
