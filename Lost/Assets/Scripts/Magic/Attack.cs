using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Magic {fireball, firebomb, firecharge, airburst, tripleburst, storm, shield, icespikes, none};

public class Attack : MonoBehaviour
{
	//Animator of projectile, DialogueManager reference, Inventory reference
	Animator anim;
	public DialogueManager DM;
	public Player_StatManager PSM;
	private Inventory INV;

	//Projectile's velocity
	public float projectile_speed = 2;

	//Player direction faced
	private int player_face;
	private int player_actual;

	//Offset projectile instead of spawning inside player
	private Vector2 offset;
	private float offsetNum = 0.2f;

	//Range and Cooldown of attack
	private List<float> rangeCounter = new List<float>();
	private List<float> coolDownCounter = new List<float>();
	public float range=0.9f;
	public float cooldownTime=0.9f;

	public int multiplier_1 = 1;
	public int multiplier_2 = 1;
	public int multiplier_3 = 1;

	//Projectile boolean information
	public bool ReadyToFire = true;
	private bool rotate_once = true;
	private bool is_rotated;
	private bool hitAnimation;
	public bool isFiring;

	//Info for what to load as projectile
	public Magic magic_attack;
	private Magic magic_attack1;
	private Magic magic_attack2;
	private Magic magic_attack3;
	private GameObject attack;
	private GameObject projectile;

	//Projectile's fired at same time
	private int projectile_amount;

	//Projectile Lists for what is on screen and what to destroy
	private List<GameObject> projectile_storage = new List<GameObject>();
	private List<GameObject> projectile_destruction = new List<GameObject>();

	void Start () 
	{
		anim = GetComponent<Animator>();
		magic_attack = Magic.fireball;
		DM = FindObjectOfType<DialogueManager>();
		INV = FindObjectOfType<Inventory>();

		magic_attack1 = Magic.airburst;
		magic_attack2 = Magic.shield;
		magic_attack3 = Magic.storm;
	}
	

	void Update () 
	{
		//Get player's direction and allow projectile to spawn in that direction
		Vector2 facing_vector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

		//Up
		if(facing_vector == new Vector2(0,1) || facing_vector == new Vector2(1,1) || facing_vector == new Vector2(-1,1))
		{
			player_face = 0;
			offset = new Vector2(0,offsetNum);
		}
		//Down
		else if(facing_vector == new Vector2(0,-1) || facing_vector == new Vector2(1,-1) || facing_vector == new Vector2(-1,-1))
		{
			player_face = 1;
			offset = new Vector2(0,-offsetNum);

		}
		//Left
		else if(facing_vector == new Vector2(-1,0))
		{
			player_face = 2;
			offset = new Vector2(-offsetNum,0);
		}
		//Right
		else if(facing_vector == new Vector2(1,0))
		{
			player_face = 3;
			offset = new Vector2(offsetNum,0);
		}

		if(Input.GetKeyDown(KeyCode.Space) && magic_attack != Magic.shield && PSM.chargeMagic())
		{
			isFiring = true;
		}
		else if(Input.GetKeyUp(KeyCode.Space) && magic_attack != Magic.shield)
		{
			isFiring = false;
		}
		else if(Input.GetKeyDown(KeyCode.Space) && magic_attack == Magic.shield && PSM.chargeMagic())
		{
			ActivateShield();
		}
		

		//Fire only when cooldown is reached and dialogue and inventory are not open
		if(isFiring && ReadyToFire && !DM.dialogueOpen && !INV.canvas_enabled)
		{
			anim.SetBool("IsAttacking",true);
			ReadyToFire = false;
			rotate_once = true;

			//Spawn projectiles based on amount
			for(int i = 0; i<projectile_amount; i++)
			{
				attack = (GameObject) Instantiate(projectile,(Vector2)transform.position + offset, Quaternion.identity);
				attack.name = "Projectile: " + i;
				projectile_storage.Add(attack);
				projectile_destruction.Add(attack);
				rangeCounter.Add(range);
			}

			//Initiate cooldown time limit
			coolDownCounter.Add(cooldownTime);
			player_actual = player_face;
		}
		else
		{
			anim.SetBool("IsAttacking",false);
		}

		//If projectiles exist 
		if(projectile_storage != null && projectile_storage.Count > 0 && magic_attack != Magic.shield)
		{
			//First Switch statement on player's direction faced
			//Second Switch statement based on how many projectiles are being instantiated at one time
			switch(player_actual)
			{
				//Up
				case 0:
					switch(projectile_amount)
					{
						case 1:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectile_speed);
							if(rotate_once && is_rotated)
							{
								attack.transform.Rotate(Vector3.forward * -90);
								rotate_once = false;
							}
							break;
						case 2:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f,projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.3f,projectile_speed);
							if(rotate_once && is_rotated)
							{
								attack.transform.Rotate(Vector3.forward * -90);
								rotate_once = false;
							}
							break;
						case 3:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0.4f,projectile_speed);
							projectile_storage[2].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.4f,projectile_speed);
							if(rotate_once && is_rotated)
							{
								attack.transform.Rotate(Vector3.forward * -90);
								rotate_once = false;
							}
							break;
					}
					break;
				//Down
				case 1:
					switch(projectile_amount)
					{
						case 1:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectile_speed);
							if(rotate_once && is_rotated)
							{
								attack.transform.Rotate(Vector3.forward * 90);
								rotate_once = false;
							}
							break;
						case 2:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f,-projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.3f,-projectile_speed);
							if(rotate_once && is_rotated)
							{
								attack.transform.Rotate(Vector3.forward * 90);
								rotate_once = false;
							}
							break;
						case 3:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0.4f,-projectile_speed);
							projectile_storage[2].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.4f,-projectile_speed);
							if(rotate_once && is_rotated)
							{
								attack.transform.Rotate(Vector3.forward * 90);
								rotate_once = false;
							}
							break;
					}
					break;
				//Left
				case 2:
					switch(projectile_amount)
					{
						case 1:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(-projectile_speed,0);
							break;
						case 2:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(-projectile_speed,0.3f);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(-projectile_speed,-0.3f);
							break;
						case 3:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(-projectile_speed,0);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(-projectile_speed,0.4f);
							projectile_storage[2].GetComponent<Rigidbody2D>().velocity = new Vector2(-projectile_speed,-0.4f);
							break;
					}
					
					attack.GetComponent<SpriteRenderer>().flipX = false;
					break;
				//Right
				case 3:

					switch(projectile_amount)
					{
						case 1:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(projectile_speed,0);
							break;
						case 2:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(projectile_speed,0.3f);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(projectile_speed,-0.3f);
							break;
						case 3:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(projectile_speed,0);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(projectile_speed,0.4f);
							projectile_storage[2].GetComponent<Rigidbody2D>().velocity = new Vector2(projectile_speed,-0.4f);
							break;
					}
					attack.GetComponent<SpriteRenderer>().flipX = true;
					break;
			}
		}
		else if(magic_attack == Magic.shield)
		{
			//Debug.Log("SHIELD");
		}
		
		//countdown range and cooldown after being fired
		rangeCounter = rangeCounter.Select(x => x - Time.deltaTime).ToList(); 
		coolDownCounter = coolDownCounter.Select(x => x - Time.deltaTime).ToList();

		//Destroy objects when range has been reached
		for(int i = 0; i < rangeCounter.Count; i++)
		{
			if(rangeCounter[i] <= 0 && projectile_destruction.Count > 0)
			{
				Destroy(projectile_destruction[0]);
				projectile_destruction.RemoveAt(0);
				rangeCounter.RemoveAt(0);
			}
		}

		//Clear storage when cooldown has been reached
		for(int i = 0; i < coolDownCounter.Count; i++)
		{
			if(coolDownCounter[i] <= 0)
			{
				projectile_storage.Clear();
				coolDownCounter.RemoveAt(0);
				ReadyToFire = true;
			}
		}
	}

	//Swap player's attack and associated information
	public void SwapAttack(Magic selected_attack)
	{
		magic_attack = selected_attack;
		
		projectile_storage.Clear();
		switch(selected_attack)
		{
			case Magic.fireball:
				projectile = Resources.Load<GameObject>("Magic/fireball");
				projectile_amount = 1;
				is_rotated = true;
				hitAnimation = false;
				break;
			case Magic.firebomb:
				projectile = Resources.Load<GameObject>("Magic/firebomb");
				projectile_amount = 1;
				is_rotated = true;
				hitAnimation = true;
				break;
			case Magic.airburst:
				projectile = Resources.Load<GameObject>("Magic/airburst");
				projectile_amount = 1;
				is_rotated = false;
				hitAnimation = false;
				break;
			case Magic.tripleburst:
				projectile = Resources.Load<GameObject>("Magic/tripleburst");
				projectile_amount = 3;
				is_rotated = false;
				hitAnimation = false;
				break;
			case Magic.storm:
				projectile = Resources.Load<GameObject>("Magic/tornado");
				projectile_amount = 1;
				is_rotated = false;
				hitAnimation = true;
				break;
			case Magic.firecharge:
				projectile = Resources.Load<GameObject>("Magic/firecharge");
				projectile_amount = 1;
				is_rotated = true;
				hitAnimation = false;
				break;
			//Not yet finished for player use
			case Magic.icespikes:
				projectile = Resources.Load<GameObject>("Magic/IceSpikes/Spike4");
				projectile_amount = 1;
				is_rotated = false;
				hitAnimation = false;
				break;
			case Magic.shield:
				projectile = Resources.Load<GameObject>("Magic/Ice Shield");

				break;
			default:
				Debug.Log("Failed to load: " + selected_attack.ToString());
				projectile = Resources.Load<GameObject>("Magic/fireball");
				break;
		}
	}

	//Destroy object when called from collision function (when collided with an enemy)
	public void TargetHit()
	{
		if(attack.GetComponent<Animator>() != null && hitAnimation)
		{
			attack.GetComponent<Animator>().SetBool("TriggerAnimation",true);
			foreach(GameObject attack in projectile_storage)
			{
				attack.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
		}
		else
		{
			foreach(GameObject attack in projectile_storage)
			{
				DestroyObject(attack);
			}
		}
		projectile_storage.Clear();
	}

	public void ChangeAttack(int tab_slot,MagicSelection magic)
	{
		switch(magic)
		{
			case MagicSelection.air:
				switch(tab_slot)
				{
					case 1:
						SwapAttack(magic_attack1);
						break;
					case 2:
						SwapAttack(magic_attack2);
						break;
					case 3:
						SwapAttack(magic_attack3);
						break;
				}
				break;
		}
	}

	public void UpgradeFirst(MagicSelection magic)
	{
		switch(magic)
		{
			case MagicSelection.air:
				magic_attack1 = Magic.tripleburst;
				break;
		}
	}

	public void ActivateShield()
	{
		if(!GetComponent<Player_StatManager>().shield)
		{
			anim.SetBool("ShieldUp",true);
			projectile_storage.Add(Instantiate(projectile,transform.position,Quaternion.identity));
			GetComponent<Player_StatManager>().shield = true;
			Debug.Log("SHIELD UP");
			GetComponent<PlayerMovement>().pause = true;
		}
		else
		{
			Debug.Log("Shield is already active");
		}

	}

	public void ShieldAnimation()
	{
		anim.SetBool("ShieldUp",false);
		foreach(GameObject shield in projectile_storage)
		{
			Destroy(shield);
		}
		projectile_storage.Clear();
		GetComponent<PlayerMovement>().pause = false;
	}
}
