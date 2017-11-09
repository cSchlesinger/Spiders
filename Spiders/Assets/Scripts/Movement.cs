using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
	public Vector3 position;
    public bool jumping;
    private Rigidbody2D rBody;
    private Vector3 velocity = new Vector3(2.0f, 0.0f, 0.0f);
	public float speed = 0.0f;
	private float maxSpeed = 5.0f;
	private float slowDown = .1f;
	private float speedIncrement = 14f;
    private float timeSinceDirectionChange;
    public bool faceForward = true;
	private Vector3 worldSize = new Vector3(10.0f, 10.0f, 0.0f);
    public int jumpsRemaining;
	private bool useSlowdown = false;
    private float lastJumpTime;
    private float jumpCoolDown = .25f;
    private float currentJumpTime;
   
    // Use this for initialization
    void Start ()
	{
        lastJumpTime = 0;
                
        jumpsRemaining = 3;
        rBody = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (rBody.velocity.y < .5 && rBody.velocity.y > -.5)
        {
            jumping = false;
        }
        else
        {
            jumping = true;
        }
        Jump();
        CheckInput();
        
        
	}
    public void Jump()
    {
        currentJumpTime = Time.fixedTime;
        //jump check
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0 && currentJumpTime - lastJumpTime >= jumpCoolDown)
        {
            if(jumpsRemaining == 3)
            {
                rBody.AddForce(Vector2.up * (700) );
                //jumping = true;
                jumpsRemaining--;
                lastJumpTime = currentJumpTime;
                return;
            }
            if(jumpsRemaining == 2)
            {
                rBody.AddForce(Vector2.up * (32000) * Time.deltaTime);
                jumpsRemaining--;
                lastJumpTime = currentJumpTime;
                return;
            }
            if(jumpsRemaining == 1)
            {
                rBody.AddForce(Vector2.up * (32000) * Time.deltaTime);
                jumpsRemaining--;
                lastJumpTime = currentJumpTime;
                return;
            }
        }
       
    }
        private void CheckInput()
    {
        
        if (gameObject.tag == "Player")
        {
            //Is it moving or slowing down?
            if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {                
                speed = 0;
                position.x += velocity.x * speed * Time.deltaTime;
                transform.position = new Vector3(position.x, transform.position.y, 0);
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A) && !jumping)
                {
                    speed = 0;
                }
                else
                {                    
                    speed += speedIncrement * Time.deltaTime;
                }
                faceForward = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.D) && !jumping)
                {
                    speed = 0;
                }
                else
                {
                    speed -= speedIncrement * Time.deltaTime;
                }
                faceForward = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(!jumping)
            {
                speed = 0.0f;
            }

            //Check max and min speed
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            if (speed < -maxSpeed)
            {
                speed = -maxSpeed;
            }
            else if (speed < 0.01f && speed > -.01f)
            {
                speed = 0.0f;
            }

        }

        //transform.position = position;       
        position.x += velocity.x * speed * Time.deltaTime;
        transform.position = new Vector3(position.x, transform.position.y, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("platform"))
        {
            jumpsRemaining = 3;
        }
    }

}
