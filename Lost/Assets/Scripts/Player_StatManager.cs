using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StatManager : MonoBehaviour {

	public int playerMaxHealth;
	public int playerCurrentHealth;
	private int displayedHealth;
	public int playerMaxMP;
	public int playerCurrentMP;
	private int displayedMP;

	public int experience;
	public int gold;

	//Item List
	public int[] item_list;

	void Start () 
	{
		playerCurrentHealth = playerMaxHealth;
		displayedHealth = playerCurrentHealth;
		gold = 100;
	}
	
	void Update () 
	{

		if(playerCurrentHealth <= 0)
		{
			gameObject.GetComponent<Animator>().SetBool("IsDead",true);
		}
		else if(playerCurrentHealth > playerMaxHealth)
		{
			playerCurrentHealth = playerMaxHealth;
		}
	}

	public void doDamage(int damage)
	{
		displayedHealth -= damage;
		while(playerCurrentHealth != displayedHealth)
		{
			if(displayedHealth < playerCurrentHealth)
			{
				playerCurrentHealth--;
			}
		}
	}

	public void HealPlayer(int health)
	{
		playerCurrentHealth += health;

	}

	public void changeMaxHealth(int maxHealth)
	{
		playerMaxHealth = maxHealth;
	}
}
