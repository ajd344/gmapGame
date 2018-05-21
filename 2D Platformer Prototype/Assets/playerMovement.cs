using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    //Basic movement/jump
    [Range(1, 20)]
    public float moveVelocity = 10;
    public bool facingRight = true;
    public float moveX;

    //Ground Check
    public LayerMask groundLayer;

    //Wall jumping params
    string previousWallName = "";
    public bool wallJumpAllowed;
    public float wallJumpY;
    public float wallJumpX;
    public float jumpDirection;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

    }
    void FixedUpdate()
    {

    }
    void Update()
    {
        playerMove();
        if (wallJumpAllowed && Input.GetButtonDown("Jump"))
        {
            wallJump();
        }
        
    }


    void playerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");

        //animations
        //playerDirections
        if (moveX < 0.0f && facingRight == false)
        {
            flipPlayer();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            flipPlayer();
        }
        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * moveVelocity, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    }

    void wallJump()
    {
        if (facingRight)
        {
            rb.AddForce(new Vector2(-wallJumpX, wallJumpY));
        }
        else if (!facingRight)
        {
            rb.AddForce(new Vector2(wallJumpX, wallJumpY));
        }

        wallJumpAllowed = false;
    }
    void flipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Platform")
        {
            transform.parent = col.transform;
        }
        if (col.gameObject.tag == ("Wall"))
        {
            if (!previousWallName.Equals(col.gameObject.name))
            {
                wallJumpAllowed = true;
            }
            previousWallName = col.gameObject.name;
        }
        else
        {
            previousWallName = "";
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Platform")
        {
            transform.parent = null;
        }
        if (col.gameObject.tag == ("Wall"))
            wallJumpAllowed = false;
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = .50f;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

}