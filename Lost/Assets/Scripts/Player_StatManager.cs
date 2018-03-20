using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StatManager : MonoBehaviour {

	public Inventory inventory;
	public MagicMenu magic;
	private PlayerMovement player_active;
	private ParticleSystem psystem;

	public int playerMaxHealth;
	public int playerCurrentHealth;
	private int displayedHealth;
	public int playerMaxMP;
	public int playerCurrentMP;
	private int displayedMP;

	public int experience;
	public int experience_to_level;
	public int gold;
	public int magic_points;
	public int level;

	void Start () 
	{
		player_active = GetComponent<PlayerMovement>();
		psystem = GetComponent<ParticleSystem>();

		playerCurrentHealth = playerMaxHealth;
		displayedHealth = playerCurrentHealth;

		gold = 100;
		experience_to_level = 100;

		psystem.Stop();
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

		if(experience >= experience_to_level)
		{
			LevelUp();
		}

		if(Input.GetKeyDown(KeyCode.I))
		{
			player_active.pause = !player_active.pause;
			inventory.ToggleInventory();
		}
		else if(Input.GetKeyDown(KeyCode.M))
		{
			player_active.pause = !player_active.pause;
			magic.ToggleMagicMenu();
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
		Debug.Log("Healed for " + health + " hp");
	}

	public bool ChargeGold(int subtractGold)
	{
		if(gold >= subtractGold)
		{
			gold -= subtractGold;
			Debug.Log("Removed " + subtractGold + " Gold");
			return true;
		}
		Debug.Log("Not enough Gold");
		return false;
		
	}

	public void changeMaxHealth(int maxHealth)
	{
		playerMaxHealth = maxHealth;
	}

	//Pickup Item
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Item" || other.tag == "QuestItem")
		{
			inventory.AddItem(other.GetComponent<Item>());
			Destroy(other.gameObject);
		}
	}

	private void LevelUp()
	{
		experience -= experience_to_level;
		level++;
		magic_points++;
		experience_to_level += experience_to_level*level;
		psystem.Play();
		//DISPLAY LEVEL UP
	}
}
