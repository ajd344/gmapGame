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
  
    //Wall jumping params
    bool wallJumpAllowed;
    public float wallJumpForce;
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
        if(wallJumpAllowed && Input.GetButtonDown("Jump"))
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
       GetComponent<Rigidbody2D>().AddForce(Vector2.up * wallJumpForce);
       

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
            wallJumpAllowed = true;
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
    
   
}