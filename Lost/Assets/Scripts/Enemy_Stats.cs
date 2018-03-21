using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour {

	//Player stat manager
	private Player_StatManager PSM;

	//Enemy stats
	public int enemyMaxHealth;
	public int enemyCurrentHealth;
	public int gold_reward;
	public int experience_reward;
	private bool rewards_given;

	void Start () 
	{
		enemyCurrentHealth = enemyMaxHealth;
		PSM = FindObjectOfType<Player_StatManager>();
		rewards_given = false;
	}
	
	void Update () 
	{
		//If enemy is dead, animate death and give reward
		if(enemyCurrentHealth <= 0)
		{
			gameObject.GetComponent<Animator>().SetBool("IsDead",true);
			if(!rewards_given)
			{
				PSM.gold += gold_reward;
				PSM.experience += experience_reward;
				rewards_given = true;
			}
		}
		//Health cannot exceed max health (for self healing monsters)
		else if(enemyCurrentHealth > enemyMaxHealth)
		{
			enemyCurrentHealth = enemyMaxHealth;
		}
	}

	//Remove health from enemy
	public void doDamage(int damage)
	{
		enemyCurrentHealth -= damage;
	}

	//Give health to enemy
	public void healEnemy(int health)
	{
		enemyCurrentHealth += health;

	}
}
