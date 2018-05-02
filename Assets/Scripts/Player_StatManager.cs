using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StatManager : MonoBehaviour {

	//Inventory, Attack, and Magic menu references
	public Inventory inventory;
	public MagicMenu magic;
	public Attack attack;

	//Player movement and player particle system
	private PlayerMovement player_active;
	private ParticleSystem psystem;

	//Player active stats
	public int playerMaxHealth;
	public int playerCurrentHealth;
	public int playerMaxMP;
	public int playerCurrentMP;
	public bool shield;
	public bool damage_taken;

	//Player passive stats
	public int experience;
	public int experience_to_level;
	public int gold;
	public int magic_points;
	public int level;

	private int magic_cost;

	private bool unlocked_1;
	private bool unlocked_2;
	private	bool unlocked_3;

	private float damageTime = 1.0f;
	private float damageCounter;
	private int flashCounter;

	public MagicSelection magicSelected;
	public int tab_selected;

	void Start () 
	{
		player_active = GetComponent<PlayerMovement>();
		psystem = GetComponent<ParticleSystem>();

		playerCurrentHealth = playerMaxHealth;
		playerCurrentMP = playerMaxMP;
		gold = 100;
		experience_to_level = 100;

		unlocked_1 = false;
		unlocked_2 = false;
		unlocked_3 = false;

		psystem.Stop();
	}
	
	void Update () 
	{
		//If player health reaches 0, animate death
		if(playerCurrentHealth <= 0)
		{
			gameObject.GetComponent<Animator>().SetBool("IsDead",true);
		}
		//If player takes daamge and is not dead, flash transparancy and grant temporary immunity
		else if(damage_taken)
		{
			damageCounter -= Time.deltaTime;
			if(damageCounter < 0)
			{
				damage_taken = false;
				GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
			}
			else
			{
				flashCounter++;
				if(flashCounter > 5 && flashCounter < 10)
				{
					GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
				}
				else if(flashCounter < 5)
				{
					GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
				}
				else if(flashCounter >= 10)
				{
					flashCounter = 0;
				}
				
			}
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

		//Change magic selections
		if(Input.GetKeyDown(KeyCode.Alpha1) && unlocked_1)
		{
			tab_selected = 1;
			attack.ChangeAttack(tab_selected,magicSelected);
			magic_cost = 0;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2) && unlocked_2)
		{
			tab_selected = 2;
			attack.ChangeAttack(tab_selected,magicSelected);
			magic_cost = 25;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3) && unlocked_3)
		{
			tab_selected = 3;
			attack.ChangeAttack(tab_selected,magicSelected);
			magic_cost = 70;
		}
	}

	//Do damage to player
	public void doDamage(int damage)
	{
		if(!damage_taken)
		{
			if(!shield)
			{
				playerCurrentHealth -= damage;
			}
			else
			{
				shield = false;
			}
			damageCounter = damageTime;
			damage_taken = true;
		}
	}

	public bool chargeMagic()
	{
		if(playerCurrentMP - magic_cost >= 0)
		{
			playerCurrentMP -= magic_cost;
			return true;
		}
		return false;
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

	public void changeMaxMp(int maxMP)
	{
		playerMaxMP += maxMP;
		playerCurrentMP = playerMaxMP;
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
		changeMaxHealth(50);
		changeMaxMp(25);
		experience_to_level += 100;
		psystem.Play();

		//TO DO --- Display LEVEL UP on top of player w/ their level number
	}

	public void UnlockSlot(int unlock_slot)
	{
		switch(unlock_slot)
		{
			case 1:
				unlocked_1 = true;
				break;
			case 2:
				unlocked_2 = true;
				break;
			case 3:
				unlocked_3 = true;
				break;
		}
	}
}
