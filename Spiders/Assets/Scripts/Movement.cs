using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
	public Vector3 position;
	private Vector3 velocity = new Vector3(2.0f, 0.0f, 0.0f);
	public float speed = 0.0f;
	private float maxSpeed = 7.0f;
	private float slowDown = .1f;
	private float speedIncrement = 7.0f;

	private float angularSpeed = 90.0f;
	private float angle = 0.0f;

	private Vector3 worldSize = new Vector3(10.0f, 10.0f, 0.0f);

	private bool useSlowdown = false;
    	

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (Input.GetKeyDown(KeyCode.W))
		if(gameObject.tag == "Player")
		{
			//Is it moving or slowing down?
			if(Input.GetKey(KeyCode.D))
			{
                if (Input.GetKey(KeyCode.A))
                {
                    speed = 0;
                }
				speed += speedIncrement * Time.deltaTime;
			}
			else if(Input.GetKey(KeyCode.A))
			{
                if (Input.GetKey(KeyCode.D))
                {
                    speed = 0;
                }
                speed -= speedIncrement * Time.deltaTime;
            }
            else
            {
                speed = 0.0f;
            }
			
			//Check max and min speed
			if(speed > maxSpeed)
			{
				speed = maxSpeed;
			}
            if(speed < -maxSpeed)
            {
                speed = -maxSpeed;
            }
			else if(speed < 0.01f && speed > -.01f)
			{
				speed = 0.0f;
			}
			
		}
		
		transform.position = position;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
		
		position += transform.rotation * velocity * speed * Time.deltaTime;
	
	}

	public Vector3 GetDirection()
	{
		return Vector3.Normalize(Quaternion.Euler(0.0f, angle, 0.0f) * velocity);
	}
	
   

}
