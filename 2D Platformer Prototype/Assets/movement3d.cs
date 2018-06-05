using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement3d : MonoBehaviour
{
    Rigidbody rb3d;
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
    
    // Update is called once per frame
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        rb3d.freezeRotation = true;

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
        gameObject.GetComponent<Rigidbody>().velocity = new Vector2(moveX * moveVelocity, gameObject.GetComponent<Rigidbody>().velocity.y);

    }
    void playerjump()
    {
        if (!isGrounded)
        {
            return;
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpVelocity);
        }
    }
    void wallJump()
    {
        if (facingRight)
        {
            rb3d.AddForce(new Vector3(-wallJumpX, wallJumpY, 0));
        }
        else if (!facingRight)
        {
            rb3d.AddForce(new Vector3(wallJumpX, wallJumpY, 0));
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
        //if (col.gameObject.tag == "Ground")
        //{
        //    isGrounded = true;
        //}

    }
    void OnCollisionStay2D(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision col)
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
        //if (col.gameObject.tag == "Ground")
        //{
        //    isGrounded = false;
        //}




    }
}