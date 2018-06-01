using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    [Range(1, 10)]
    public float jumpVelocity;
    //Ground check param
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    
    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            playerJump();       
        }
        
    }

    void playerJump()
    {
        if (!IsGrounded())
        {
            return;
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.70f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    
}
