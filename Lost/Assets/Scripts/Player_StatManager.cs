using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StatManager : MonoBehaviour {

	//Inventory and Magic menu references
	public Inventory inventory;
	public MagicMenu magic;

	//Player movement and player particle system
	private PlayerMovement player_active;
	private ParticleSystem psystem;

	//Player active stats
	public int playerMaxHealth;
	public int playerCurrentHealth;
	public int playerMaxMP;
	public int playerCurrentMP;


	//Player passive stats
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
		gold = 100;
		experience_to_level = 100;

		psystem.Stop();
	}
	
	void Update () 
	{
		//If player health reaches 0, animate death
		if(playerCurrentHealth <= 0)
		{
			gameObject.GetComponent<Animator>().SetBool("IsDead",true);
		}
		//Limit health to max health amount
		else if(playerCurrentHealth > playerMaxHealth)
		{
			playerCurrentHealth = playerMaxHealth;
		}
		if(playerCurrentMP > playerMaxMP)
		{
			playerCurrentMP = playerMaxMP;
		}

		//If experience cap is reached, level up
		if(experience >= experience_to_level)
		{
			LevelUp();
		}

		//Open inventory window
		if(Input.GetKeyDown(KeyCode.I))
		{
			player_active.pause = !player_active.pause;
			inventory.ToggleInventory();
		}
		//Open magic menu window
		else if(Input.GetKeyDown(KeyCode.M))
		{
			player_active.pause = !player_active.pause;
			magic.ToggleMagicMenu();
		}


	}

	//Do damage to player
	public void doDamage(int damage)
	{
		playerCurrentHealth -= damage;
		//TO DO -- Add DEFENSE stat to player that affects damage taken
	}

	//Give health to player
	public void HealPlayer(int health)
	{
		playerCurrentHealth += health;
		Debug.Log("Healed for " + health + " hp");
	}

	//Give magic points to player
	public void HealMagic(int magic)
	{
		playerCurrentMP += magic;
		Debug.Log("Restored " + magic + " magic");
	}

	//Check if gold can be taken from player
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

	//Increase max health
	public void changeMaxHealth(int maxHealth)
	{
		playerMaxHealth += maxHealth;
		playerCurrentHealth = playerMaxHealth;
	}

	//Pickup Item when collided with
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Item" || other.tag == "QuestItem")
		{
			for(int i = 0; i < other.GetComponent<Item>().numberOfItems; i++)
			{
				inventory.AddItem(other.GetComponent<Item>());
			}
			Destroy(other.gameObject);
		}
		else if(other.tag == "Item_UseOnPickup")
		{
			other.GetComponent<Item>().UseItem();
			Destroy(other.gameObject);
		}
	}

	//Increase experience cap and add 1 magic point to player, play particle system
	private void LevelUp()
	{
		experience -= experience_to_level;
		level++;
		magic_points++;
		experience_to_level += experience_to_level*level;
		psystem.Play();

		//TO DO --- Display LEVEL UP on top of player w/ their level number
	}
}
