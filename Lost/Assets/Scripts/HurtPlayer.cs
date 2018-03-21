using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	public int damage;

	//When collided with player, do damage
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.name == "Player")
		{
			other.gameObject.GetComponent<Player_StatManager>().doDamage(damage);
		}
	}
}
