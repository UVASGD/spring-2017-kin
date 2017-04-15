using UnityEngine;
using System.Collections;

public class AvatarMvmController : MonoBehaviour
{

    public float speed;
    public bool dr_stairs = false;
    public bool dl_stairs = false;

    Rigidbody2D rb;

    public Vector2 lastMove = new Vector2(0, 0);

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        
        if (!animator.GetBool("Dead"))
        {
            if (!dr_stairs && !dl_stairs)
            {
                var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                rb.velocity = ((Vector2)move.normalized) * speed;
            }
            else if (!dr_stairs && dl_stairs)
            {
                //right = up/right
                //left = down/left
                var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Horizontal"), 0);
                rb.velocity = ((Vector2)move.normalized) * speed;
            }
            else if (dr_stairs && !dl_stairs)
            {
                //right = down/right
                //left = up/left
                var move = new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Horizontal"), 0);
                rb.velocity = ((Vector2)move.normalized) * speed;
            }
            else
            {
                // shouldn't happen, move normally
                var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                rb.velocity = ((Vector2)move.normalized) * speed;
            }
        }
        lastMove = gameObject.GetComponent<AnimationControl>().lastMove;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "dr_stair")
        {
            dr_stairs = true;
        }
        if (other.gameObject.tag == "dl_stair")
        {
            dl_stairs = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "dr_stair")
        {
            dr_stairs = false;
        }
        if (other.gameObject.tag == "dl_stair")
        {
            dl_stairs = false;
        }
    }
}
