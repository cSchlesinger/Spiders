using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls players movement
public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rBody;

    public float velocity;
    public float walkSpeed;
    private float currentSpeed;
    private float forwardSpeed;
    private float backwardsSpeed;
    public float increment;
    private float crouchSpeed;
    private float crouchForwardSpeed;
    private float crouchBackwardSpeed;

    public static int animNum;
    public bool jumping;

    private Animator animator;

    // Use this for initialization
    void Start() {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        forwardSpeed = walkSpeed + increment;
        backwardsSpeed = walkSpeed - increment - 5;
        crouchSpeed = walkSpeed / 1.5f;
        crouchForwardSpeed = crouchSpeed + (increment / 3f);
        crouchBackwardSpeed = crouchSpeed - (increment / 3f) - 5;
        currentSpeed = walkSpeed;
        animNum = 0;
        jumping = false;
    }

    // Update is called once per frame
    void Update() {

        if (rBody.velocity.y == 0)
        {            
            jumping = false;
        }
        else
        {
            jumping = true;
        }

        ConstantMovement();

        CheckInput();


    }

    public void ConstantMovement()
    {
        transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
    }

    public void CheckInput()
    {
        Crouch();

        Jump();

        //walk check
        if (Input.GetKey(KeyCode.D))
        {
            currentSpeed = forwardSpeed;            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentSpeed = backwardsSpeed;           
        }
    }

    public void Crouch()
    {
        if (Input.GetKey(KeyCode.S))
        {           
            currentSpeed = crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            currentSpeed = crouchForwardSpeed;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            currentSpeed = crouchBackwardSpeed;            
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    public void Jump()
    {
        //jump check
        if (Input.GetKeyDown(KeyCode.W) && (rBody.velocity.y < .5 && rBody.velocity.y > -.3))
        {
            rBody.AddRelativeForce(Vector2.up * rBody.mass * (velocity * 200) * Time.deltaTime);
            //jumping = true;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    
}
