using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
	public Vector3 position;
	private Vector3 velocity = new Vector3(1.0f, 0.0f, 0.0f);
	public float speed = 0.0f;
	private float maxSpeed = 30.0f;
	private float slowDown = 0.97f;
	private float speedIncrement = 10.0f;

	private float angularSpeed = 90.0f;
	private float angle = 0.0f;

	private Vector3 worldSize = new Vector3(10.0f, 10.0f, 0.0f);

	private bool useSlowdown = true;

	//public GameObject gameInfo;
	//private TerrainInfo terrainInfo;

	// Use this for initialization
	void Start ()
	{
        /*
		if( null == gameInfo)
		{
			Debug.Log("Error: " + gameObject.name + " needs a GameObject that cointains a TerrainInfo component");
			Debug.Break();
		}
		terrainInfo = gameInfo.GetComponent<TerrainInfo>();
		if(null == terrainInfo)
		{
			Debug.Log("Error: gameInfo provided to " + gameObject.name + " needs a terrainInfo component");
			Debug.Break();
		}

		worldSize = terrainInfo.size;

        */
		if(gameObject.tag != "Player")
		{
			speed = Random.Range(5.0f, 35.0f);
			velocity = new Vector3(-1.0f, 0.0f, 0.0f);
			float halfX = worldSize.x / 2.0f;
			float halfY = worldSize.y / 2.0f;
			position = new Vector3(Random.Range(-halfX, halfX), Random.Range(-halfY, halfY), 0.0f);
			angle = Random.Range(0.0f, 360.0f);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (Input.GetKeyDown(KeyCode.W))
		if(gameObject.tag == "Player")
		{
			//Is it moving or slowing down?
			if(Input.GetKey(KeyCode.W))
			{
				speed += speedIncrement * Time.deltaTime;
			}
			else if(useSlowdown)
			{
				speed *= slowDown;
			}
			
			//Check max and min speed
			if(speed > maxSpeed)
			{
				speed = maxSpeed;
			}
			else if(speed < 0.01f)
			{
				speed = 0.0f;
			}
			
			//Increment angle or orientation
			if(Input.GetKey(KeyCode.A))
			{
				angle += angularSpeed * Time.deltaTime;
			}
			else if(Input.GetKey(KeyCode.D))
			{
				angle -= angularSpeed * Time.deltaTime;
			}
			
			//Keep the angle between 0 and 360
			if(angle > 360.0f)
			{
				angle -= 360.0f;
			}
			else if (angle < 0.0f)
			{
				angle += 360.0f;
			}
		}
		
		transform.position = position;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
		
		position += transform.rotation * velocity * speed * Time.deltaTime;
		
		//CheckBoundry();
	}

	public Vector3 GetDirection()
	{
		return Vector3.Normalize(Quaternion.Euler(0.0f, angle, 0.0f) * velocity);
	}
	
    /*
	// Position the object inside of the terrain
	void CheckBoundry()
	{	
		//Check within X
		if(position.x > worldSize.x / 2.0f)
			position.x = -worldSize.x / 2.0f;
		else if(position.x < -worldSize.x / 2.0f)
			position.x = worldSize.x / 2.0f;
		
		//check within Y
		if(position.y > worldSize.y / 2.0f)
			position.y = -worldSize.y / 2.0f;
		else if(position.y < -worldSize.y / 2.0f)
			position.y = worldSize.y / 2.0f;
	}
    */

}
