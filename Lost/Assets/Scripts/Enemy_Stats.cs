using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour {

	public int enemyMaxHealth;
	public int enemyCurrentHealth;
	public int goldReward;
	
	void Start () 
	{
		enemyCurrentHealth = enemyMaxHealth;
	}
	
	void Update () 
	{

		if(enemyCurrentHealth <= 0)
		{
			gameObject.GetComponent<Animator>().SetBool("IsDead",true);
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
