using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll; 
    private Animator anim;

    [SerializeField]private LayerMask jumpableGroud; 
    [SerializeField]private float moveSpeed= 7f;
    [SerializeField]private float jumpForce= 14f; 
    private float dirX = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll= GetComponent<BoxCollider2D>();
        anim= GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

       if (Input.GetButtonDown("Jump") && IsGrounded()) 
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
            if (dirX > 0f)
            {
                anim.SetBool("running", true);
                sprite.flipX= false;
            }
            else if (dirX < 0f)
            {
                anim.SetBool("running", true);
                sprite.flipX= true;
            }
            else
            {
                anim.SetBool("running", false);
            }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGroud);
    }

}
