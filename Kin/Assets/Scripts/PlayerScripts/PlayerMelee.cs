using UnityEngine;
using System.Collections;

public class PlayerMelee : MonoBehaviour {

    // Attack hitboxes for 4 directions
    public GameObject rightAttackBox;
	public GameObject leftAttackBox;
	public GameObject upperAttackBox;
	public GameObject lowerAttackBox;

    private bool facingRight;
    private bool facingLeft;
    private bool facingUp;
    private bool facingDown;

    private bool attacking = false;
    private float attackTimer = 0;
    // How long hitbox is enabled
    public float attackCoolDown = 0.2f;
    public KeyCode attackKey;

    public int damage = 20;

    void Awake()
    {
		rightAttackBox.SetActive(false);
		leftAttackBox.SetActive(false);
		upperAttackBox.SetActive(false);
		lowerAttackBox.SetActive(false);

        rightAttackBox.GetComponent<MeleeAttackHitBox>().setDamage(damage);
        leftAttackBox.GetComponent<MeleeAttackHitBox>().setDamage(damage);
        upperAttackBox.GetComponent<MeleeAttackHitBox>().setDamage(damage);
        lowerAttackBox.GetComponent<MeleeAttackHitBox>().setDamage(damage);

        // Assuming player starts out facing forward
        facingRight = false;
        facingLeft = false;
        facingUp = false;
        facingDown = true;

    }
    
	
	// Update is called once per frame
	void Update () {

        // Get last direction from AvatarMvmController
        Vector2 lastMove = gameObject.GetComponent<AvatarMvmController>().lastMove;

        // We might want to change this to use the animator when that stuff is figured out
        if (lastMove.x == 0) // If last movement in y direction
        {
            if (lastMove.y < 0) {
                //Debug.Log("DOWN");
                facingRight = false;
                facingLeft = false;
                facingUp = false;
                facingDown = true;
            }
            else if (lastMove.y > 0)
            {
                //Debug.Log("UP");
                facingRight = false;
                facingLeft = false;
                facingUp = true;
                facingDown = false;
            }
        }
        else if (lastMove.y == 0) // If last movement in x direction
        {
            if (lastMove.x < 0)
            {
                //Debug.Log("LEFT");
                facingRight = false;
                facingLeft = true;
                facingUp = false;
                facingDown = false;
            }
            else if (lastMove.x > 0)
            {
                //Debug.Log("RIGHT");
                facingRight = true;
                facingLeft = false;
                facingUp = false;
                facingDown = false;
            }
        }

        if (Input.GetButtonDown("Attack") && !attacking && GetComponent<PlayerStamina>().hasStamina)
        {
            attacking = true;
			this.gameObject.GetComponent<FXHandler> ().playAtkLow ();
            attackTimer = attackCoolDown; // Start timer
            if (facingRight)
            {
				rightAttackBox.SetActive (true);
            }
            else if (facingLeft)
            {
				leftAttackBox.SetActive (true);
            }
            else if (facingUp)
            {
				upperAttackBox.SetActive (true);
            }
            else if (facingDown)
            {
				lowerAttackBox.SetActive (true);
            }
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;

            }
            else
            {
                attacking = false;
				rightAttackBox.SetActive (false);
				leftAttackBox.SetActive (false);
				upperAttackBox.SetActive (false);
				lowerAttackBox.SetActive (false);
            }
        }

    }
}
