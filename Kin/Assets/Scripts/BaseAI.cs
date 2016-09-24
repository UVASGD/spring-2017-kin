using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour {

	// Although it would be helpful for others who may look at your code, your code does not need to be this thouroughly
	// commented. In some ways, it makes the code harder to read at a glance, but easier to understand overall.

	/// <summary>
	/// AI states. Fill in with more states in the future, please.
	/// Idle: not detected player
	/// Detected: detected player
	/// 
	/// </summary>
	protected enum AIStates {
		IdleState,
		DetectedState
	}

	/// <summary>
	/// The radius in which the AI is aware of the player and acts with that knowledge in mind.
	/// </summary>
	public float awarenessRadius;

	/// <summary>
	/// The target object for the AI to focus on.
	/// </summary>
	public GameObject targetObject;

	/// <summary>
	/// The current state of the AI.
	/// </summary>
	AIStates curState;

	/// <summary>
	/// The speed at which the AI moves.
	/// </summary>
	public float speed = 1.0f;

	/// <summary>
	/// The Rigidbody which controls the movement of the AI.
	/// </summary>
	protected Rigidbody2D rb;


	/** One thing to keep in mind; Start and Update are meant to be overridden, but you should still call base.Start()
	and base.Update() before running the rest of your Start and Update methods. **/


	// This method is run right about when the object is created. It should be where you intialize variables and other
	// things that may depend on certain objects being available.
	// It's also a good place to throw errors, like I have done here.
	protected virtual void Start() {
		// This is the syntax for getting components from GameObjects. Remember the parenthesis.
		rb = gameObject.GetComponent<Rigidbody2D>();

		// This is how you should do error reporting. Unless the bug is game breaking, don't crash the game, just report an error.
		// Also, try to include some kind of identifying information (even name may not be enough).
		if (rb == null) {
			Debug.LogError("AI has no RigidBody. AI name is " + gameObject.name + "!");
		}
		if (targetObject == null) {
			Debug.LogError("AI has no target. AI name is " + gameObject.name + "!");
		}

		// Sets the AI to a default state.
		curState = AIStates.IdleState;
	}


	protected virtual void Update() {
		if (curState == AIStates.DetectedState) {
			// Do stuff as if the player was detected.
		} else if (curState == AIStates.IdleState) {
			// Do stuff as if the player was too far away.
			// For minions, this might mean stand still.
			// For bosses and NPCs, this might mean patrol a given path.
		}
	}

	/// <summary>
	/// Moves towards the target at a constant speed (set by public speed variable).
	/// </summary>
	protected void MoveTowardsTarget() {
		rb.velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * speed;
	}
}
