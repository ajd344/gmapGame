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
    [Range(1, 20)]
    public float jumpVelocity;

    //Ground Check
    public LayerMask groundLayer;
    public bool isGrounded;

    //Wall jumping params
    string previousWallName = "";
    public bool wallJumpAllowed;
    public float wallJumpY;
    public float wallJumpX;
    public float jumpDirection;
    public LayerMask wallLayer;
    
    
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

        //if (Input.GetButton("Jump"))
        //{
        //    playerJump();
        //}
        if ((IsWalledLeft() || IsWalledRight()) && Input.GetButtonDown("Jump"))
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
    //void playerJump()
    //{
    //    if (!isGrounded)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    //    }
    //}
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
    bool IsWalledRight()
    {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.right;
        float distance = .50f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, wallLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    bool IsWalledLeft()
    {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.left;
        float distance = .50f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, wallLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //Makes Player a child of platform to move along with platform
        if (col.transform.tag == "movingPlatform")
        {
            transform.parent = col.transform;
            isGrounded = true;
        }
        //Checks for available wall
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
        if (col.transform.tag == "Teleporter")
        {
            transform.position = col.transform.GetChild(0).position;
        }

    }
    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //Makes Player not a child of the moving platform
        if (col.transform.tag == "movingPlatform")
        {
            transform.parent = null;
            isGrounded = false; 
        }
        if (col.gameObject.tag == ("Wall"))
        {
            wallJumpAllowed = false;
        }
        




    }
}