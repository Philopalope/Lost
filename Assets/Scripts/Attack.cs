using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour 
{
	Animator anim;
	public DialogueManager DM;
	public float projectile_speed = 2;
	public Vector2 velocity = new Vector2(0.2f,0.2f);

	private Vector2 direction;
	private int player_face;
	private int player_actual;
	public float maxDistance = 2;

	private float rangeCounter;
	public float range=0.9f;
	private float coolDownCounter;
	public float cooldownTime=0.9f;

	public bool ReadyToFire = true;
	private bool rotate_once = true;

	public enum Magic {fireball, firebomb, airburst, tripleburst};
	public Magic magic_attack;
	private GameObject attack;
	private GameObject projectile;

	public int projectile_amount;
	private List<GameObject> projectile_storage = new List<GameObject>();
	private List<GameObject> projectile_destruction = new List<GameObject>();

	void Start () 
	{
		anim = GetComponent<Animator>();
		magic_attack = Magic.fireball;
		DM = FindObjectOfType<DialogueManager>();
	}
	
	void Update () 
	{
		SwapAttack(magic_attack);

		Vector2 facing_vector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
		//Up
		if(facing_vector == new Vector2(0,1) || facing_vector == new Vector2(1,1) || facing_vector == new Vector2(-1,1))
		{
			player_face = 0;
		}
		//Down
		else if(facing_vector == new Vector2(0,-1) || facing_vector == new Vector2(1,-1) || facing_vector == new Vector2(-1,-1))
		{
			player_face = 1;
		}
		//Left
		else if(facing_vector == new Vector2(-1,0))
		{
			player_face = 2;
		}
		//Right
		else if(facing_vector == new Vector2(1,0))
		{
			player_face = 3;
		}
		

		if(Input.GetKeyDown("space") && ReadyToFire && !DM.dialogueOpen)
		{
			anim.SetBool("IsAttacking",true);
			ReadyToFire = false;
			rotate_once = true;

			
			for(int i = 0; i<projectile_amount; i++)
			{
				attack = (GameObject) Instantiate(projectile,(Vector2)transform.position, Quaternion.identity);
				projectile_storage.Add(attack);
				projectile_destruction.Add(attack);
			}
			
			player_actual = player_face;
			rangeCounter = range;
			coolDownCounter = cooldownTime;
		}
		else
		{
			anim.SetBool("IsAttacking",false);
		}


		if(projectile_storage != null && projectile_storage.Count > 0)
		{
			switch(player_actual)
			{
				//Up
				case 0:
					switch(projectile_storage.Count)
					{
						case 1:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectile_speed);
							if(rotate_once)
							{
								attack.transform.Rotate(Vector3.forward * -90);
								rotate_once = false;
							}
							break;
						case 2:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f,projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.3f,projectile_speed);
							if(rotate_once)
							{
								attack.transform.Rotate(Vector3.forward * -90);
								rotate_once = false;
							}
							break;
						case 3:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0.4f,projectile_speed);
							projectile_storage[2].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.4f,projectile_speed);
							if(rotate_once)
							{
								attack.transform.Rotate(Vector3.forward * -90);
								rotate_once = false;
							}
							break;
					}
					break;
				//Down
				case 1:
					switch(projectile_storage.Count)
					{
						case 1:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectile_speed);
							if(rotate_once)
							{
								attack.transform.Rotate(Vector3.forward * 90);
								rotate_once = false;
							}
							break;
						case 2:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f,-projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.3f,-projectile_speed);
							if(rotate_once)
							{
								attack.transform.Rotate(Vector3.forward * 90);
								rotate_once = false;
							}
							break;
						case 3:
							projectile_storage[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectile_speed);
							projectile_storage[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0.4f,-projectile_speed);
							projectile_storage[2].GetComponent<Rigidbody2D>().velocity = new Vector2(-0.4f,-projectile_speed);
							if(rotate_once)
							{
								attack.transform.Rotate(Vector3.forward * 90);
								rotate_once = false;
							}
							break;
					}
					break;
				//Left
				case 2:
					switch(projectile_storage.Count)
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
					switch(projectile_storage.Count)
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
		
		rangeCounter -= Time.deltaTime;
		coolDownCounter -= Time.deltaTime;
		if(rangeCounter <= 0 && projectile_destruction.Count > 0)
		{
			foreach(GameObject attack in projectile_destruction)
			{
				DestroyObject(attack);
			}
			projectile_storage.Clear();
			projectile_destruction.Clear();
			ReadyToFire = true;
		}
		
		if(coolDownCounter <= 0 && projectile_storage.Count > 0)
		{
			projectile_storage.Clear();
			ReadyToFire = true;
		}
	}

	public void SwapAttack(Magic selected_attack)
	{
		switch(selected_attack)
		{
			case Magic.fireball:
				projectile = Resources.Load<GameObject>("Magic/fireball");
				projectile_amount = 1;
				break;
			case Magic.firebomb:
				projectile = Resources.Load<GameObject>("Magic/firebomb");
				projectile_amount = 1;
				break;
			case Magic.airburst:
				projectile = Resources.Load<GameObject>("Magic/airburst");
				projectile_amount = 1;
				break;
			case Magic.tripleburst:
				projectile = Resources.Load<GameObject>("Magic/tripleburst");
				projectile_amount = 3;
				break;
			default:
				Debug.Log("Failed to load: " + selected_attack.ToString());
				projectile = Resources.Load<GameObject>("Magic/fireball");
				break;
		}
	}

	public void TargetHit()
	{
		if(attack.GetComponent<Animator>() != null)
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
}
