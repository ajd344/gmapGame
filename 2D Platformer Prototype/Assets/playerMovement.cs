using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    private CharacterController controller;
    private Vector3 moveVector;
    private Vector3 lastMotion;
    private float jumpSpeed;
    private float speed;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public float pushPower = 2.0F;
    private Rigidbody2D rgd;
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
            if ((controller.collisionFlags & CollisionFlags.Above) != 0)
                print("touched the ceiling");
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
    

