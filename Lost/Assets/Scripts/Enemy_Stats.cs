using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour {

	private Player_StatManager PSM;
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
		else if(enemyCurrentHealth > enemyMaxHealth)
		{
			enemyCurrentHealth = enemyMaxHealth;
		}
	}

	public void doDamage(int damage)
	{
		enemyCurrentHealth -= damage;
	}

	public void healEnemy(int health)
	{
		enemyCurrentHealth += health;

	}
}
