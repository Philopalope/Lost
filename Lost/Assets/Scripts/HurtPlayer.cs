using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	//Damage amount
	public int damage;

	//When collided with player, do damage
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			//If object colliding is enemy
			if(gameObject.tag == "Enemy")
			{
				//Check if enemy is dead
				if(!gameObject.GetComponent<Animator>().GetBool("IsDead"))
				{
					other.gameObject.GetComponent<Player_StatManager>().doDamage(damage);
				}
			}
			//If object colliding is enemy's magic
			else if(gameObject.tag == "EnemyMagic")
			{
				other.gameObject.GetComponent<Player_StatManager>().doDamage(damage);
			}
		}
	}
}
