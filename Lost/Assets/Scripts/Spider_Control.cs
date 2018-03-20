using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Control : MonoBehaviour 
{
	public float Enemy_Speed = 1.1f;

	private Rigidbody2D Erbody;
	private Animator Eanim;
	private Collider2D collide;

	public Collider2D AreaConfinement;
	private Vector2 minWalkPoint;
	private Vector2 maxWalkPoint;
	private bool Enemy_Confined = false;

	private bool isWalking;
	private int direction_x;
	private int direction_y;
	private int direction_raw;

	private float walkTime;
	private float walkCounter;
	private float waitTime;
	private float waitCounter;
	private float despawnCounter;
	private float despawnTime = 1.5f;

	void Start () 
	{
		Eanim = GetComponent<Animator>();
		Erbody = GetComponent<Rigidbody2D>();
		collide = GetComponent<Collider2D>();
		if(AreaConfinement != null)
		{
			minWalkPoint = AreaConfinement.bounds.min;
			maxWalkPoint = AreaConfinement.bounds.max;
			Enemy_Confined = true;
		}

		waitCounter = waitTime;
		walkCounter = walkTime;
		despawnCounter = despawnTime;
		RandomDirection();
	}
	
	void FixedUpdate () 
	{
		if(!Eanim.GetBool("IsDead"))
		{
			if(isWalking)
			{
				walkCounter -= Time.deltaTime;

				switch(direction_raw)
				{
					case 0:	//Up
						Erbody.velocity = new Vector2(0,Enemy_Speed);
						if(Enemy_Confined && transform.position.y > maxWalkPoint.y)
						{
							isWalking = false;
							Eanim.SetBool("SpiderMoving",false);
							waitCounter = waitTime;
						}
						direction_x = 0;
						direction_y = 1;
						break;
					case 1:	//Down
						Erbody.velocity = new Vector2(0,-Enemy_Speed);
						if(Enemy_Confined && transform.position.y < minWalkPoint.y)
						{
							isWalking = false;
							Eanim.SetBool("SpiderMoving",false);
							waitCounter = waitTime;
						}
						direction_x = 0;
						direction_y = -1;
						break;
					case 2:	//Left
						Erbody.velocity = new Vector2(-Enemy_Speed,0);
						if(Enemy_Confined && transform.position.x < minWalkPoint.x)
						{
							isWalking = false;
							Eanim.SetBool("SpiderMoving",false);
							waitCounter = waitTime;
						}
						direction_x = -1;
						direction_y = 0;
						break;
					case 3:	//Right
						Erbody.velocity = new Vector2(Enemy_Speed,0);
						if(Enemy_Confined && transform.position.x > maxWalkPoint.x)
						{
							isWalking = false;
							Eanim.SetBool("SpiderMoving",false);
							waitCounter = waitTime;
						}
						direction_x = 1;
						direction_y = 0;
						break;
				}
				if(walkCounter < 0)
				{
					isWalking = false;
					Eanim.SetBool("SpiderMoving",false);
					waitCounter = waitTime;
				}
			}
			else
			{
				waitCounter -= Time.deltaTime;
				Erbody.velocity = Vector2.zero;
				if(waitCounter < 0)
				{
					RandomDirection();
				}
			}
			Eanim.SetFloat("Spider_x",direction_x);
			Eanim.SetFloat("Spider_y",direction_y);
		}
		else
		{
			collide.enabled = false;
			Erbody.velocity = Vector2.zero;
			despawnCounter -= Time.deltaTime;
			if(despawnCounter < 0)
			{
				//ADD PARTICLE ON DESTROY
				Destroy(gameObject);
			}
		}
	}

	public void RandomDirection()
	{
		direction_raw = Random.Range(0,4);
		walkTime = Random.Range(0.1f,1.0f);
		waitTime = Random.Range(0.5f,3.0f);
		isWalking = true;
		Eanim.SetBool("SpiderMoving",true);
		walkCounter = walkTime;
	}
}