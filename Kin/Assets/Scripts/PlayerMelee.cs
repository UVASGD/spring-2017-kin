using UnityEngine;
using System.Collections;

public class PlayerMelee : MonoBehaviour {

    // Attack hitboxes for 4 directions
    public Collider2D rightAttackBox;
    public Collider2D leftAttackBox;
    public Collider2D upperAttackBox;
    public Collider2D lowerAttackBox;

    private bool facingRight;
    private bool facingLeft;
    private bool facingUp;
    private bool facingDown;

    private bool attacking = false;
    private float attackTimer = 0;
    // How long hitbox is enabled
    public float attackCoolDown = 0.2f;
    public KeyCode attackKey = KeyCode.Z;


    void Awake()
    {
        rightAttackBox.enabled = false;
        leftAttackBox.enabled = false;
        upperAttackBox.enabled = false;
        lowerAttackBox.enabled = false;

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
                Debug.Log("DOWN");
                facingRight = false;
                facingLeft = false;
                facingUp = false;
                facingDown = true;
            }
            else if (lastMove.y > 0)
            {
                Debug.Log("UP");
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
                Debug.Log("LEFT");
                facingRight = false;
                facingLeft = true;
                facingUp = false;
                facingDown = false;
            }
            else if (lastMove.x > 0)
            {
                Debug.Log("RIGHT");
                facingRight = true;
                facingLeft = false;
                facingUp = false;
                facingDown = false;
            }
        }

        if (Input.GetKeyDown(attackKey) && !attacking)
        {
            attacking = true;
            attackTimer = attackCoolDown; // Start timer
            if (facingRight)
            {
                rightAttackBox.enabled = true;
            }
            else if (facingLeft)
            {
                leftAttackBox.enabled = true;
            }
            else if (facingUp)
            {
                upperAttackBox.enabled = true;
            }
            else if (facingDown)
            {
                lowerAttackBox.enabled = true;
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
                rightAttackBox.enabled = false;
                leftAttackBox.enabled = false;
                upperAttackBox.enabled = false;
                lowerAttackBox.enabled = false;
            }
        }

    }
}
