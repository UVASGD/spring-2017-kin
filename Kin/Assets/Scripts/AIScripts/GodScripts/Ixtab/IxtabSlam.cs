using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IxtabSlam : StateMachineBehaviour {

	IxtabAI ai;

	private float curTimer = 0.0f;
	private float attackTime = 0.5f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ai = animator.transform.parent.GetComponent<IxtabAI>();
		animator.SetBool("SlamCD", false);
		ai.GetComponent<Animator>().SetBool("Attack3", true);
		ai.activateSlamCD();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ai.stopBoss();
		if (curTimer > attackTime) {
			//ai.slam();
			attackTime = float.MaxValue;
		} else {
			curTimer += Time.deltaTime;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ai.GetComponent<Animator>().SetBool("Attack3", false);
		curTimer = 0.0f;
		attackTime = 0.5f;
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
