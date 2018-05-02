using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGolemMovement : MonoBehaviour 
{
	//Mini Golem components
	private Rigidbody2D rbody;
	private Animator anim;

	//Reference to player for player to boss distance calculation
	private GameObject playerRef;

	//Mini Golem speed
	public float speed;
	private float agroRadius = 10f;

	//Mini Golem alive status and timers
	private float inactiveTime = 3.0f;
	private float inactiveCounter;
	private bool inactiveStarted;

	//Mini Golem direction
	private int direction_x;
	private int direction_y;

	void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		playerRef = GameObject.Find("Player");
		inactiveCounter = inactiveTime;
	}

	void Update()
	{
		//If health reaches 0, start respawn time and inactive animation / settings
		if(GetComponent<Enemy_Stats>().enemyCurrentHealth <= 0 && !inactiveStarted)
		{
			setInactive();
		}

		if(transform.parent.gameObject.GetComponent<Animator>().GetBool("IsDead"))
		{
			Destroy(gameObject);
		}
		//If Mini golem is not dead, apply force in direction of the player... constantly
		if(!anim.GetBool("IsDead") && !anim.GetBool("Respawning"))
		{
			rbody.AddForce(new Vector2 (direction_x,direction_y)  * speed);

			double agroDistanceX = (double) Mathf.Abs(playerRef.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x);
			double agroDistanceY = (double) Mathf.Abs(playerRef.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y);

			//Handles direction of player, uses same formula as Golem
			if(agroDistanceX < agroRadius && agroDistanceY < agroRadius)
			{
				switch(agroDistanceX > agroDistanceY)
				{
					case true:
						//Right
						if(playerRef.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x > 0)
						{
							direction_x = 1;
							direction_y = 0;
						}
						//Left
						else
						{
							direction_x = -1;
							direction_y = 0;
						}
						break;
					case false:
						//Up
						if(playerRef.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y > 0)
						{
							direction_x = 0;
							direction_y = 1;
						}
						//Down
						else
						{
							direction_x = 0;
							direction_y = -1;
						}
						break;
				}
				anim.SetBool("IsMoving",true);
			}
			anim.SetFloat("MiniGolem_x",direction_x);
			anim.SetFloat("MiniGolem_y",direction_y);
		}
		//If mini golem is dead, begin timer for respawning
		else
		{
			inactiveCounter -= Time.deltaTime;
			if(inactiveCounter < 0 && !transform.parent.gameObject.GetComponent<Animator>().GetBool("IsDead"))
			{
				GetComponent<Enemy_Stats>().enemyCurrentHealth = GetComponent<Enemy_Stats>().enemyMaxHealth;
				anim.SetBool("Respawning",true);
				anim.SetBool("IsDead",false);
				inactiveStarted = false;
			}
		}
	}

	//Helper function for death animation and respawning
	private void setInactive()
	{
		inactiveCounter = inactiveTime;
		rbody.velocity = Vector2.zero;
		anim.SetBool("IsDead",true);
		inactiveStarted = true;
	}

	private void setActive()
	{
		anim.SetBool("Respawning",false);
	}
}
