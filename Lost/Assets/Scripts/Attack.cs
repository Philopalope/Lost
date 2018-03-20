using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour 
{
	Animator anim;
	public DialogueManager DM;
	private Inventory INV;
	public float projectile_speed = 2;

	private int player_face;
	private int player_actual;
	private Vector2 offset;
	private float offsetNum = 0.2f;
	public float maxDistance = 2;

	private List<float> rangeCounter = new List<float>();
	public float range=0.9f;
	private List<float> coolDownCounter = new List<float>();
	public float cooldownTime=0.9f;

	public bool ReadyToFire = true;
	private bool rotate_once = true;
	private bool is_rotated;
	private bool hitAnimation;
	public bool isFiring;

	public enum Magic {fireball, firebomb, firecharge, airburst, tripleburst, storm};
	public Magic magic_attack;
	private GameObject attack;
	private GameObject projectile;

	private int projectile_amount;
	private List<GameObject> projectile_storage = new List<GameObject>();
	private List<GameObject> projectile_destruction = new List<GameObject>();

	void Start () 
	{
		anim = GetComponent<Animator>();
		magic_attack = Magic.tripleburst;
		DM = FindObjectOfType<DialogueManager>();
		INV = FindObjectOfType<Inventory>();
	}
	
	void Update () 
	{
		//CHANGE
		SwapAttack(magic_attack);
		////////

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

		if(Input.GetKeyDown(KeyCode.Space))
		{
			isFiring = true;
		}
		else if(Input.GetKeyUp(KeyCode.Space))
		{
			isFiring = false;
		}

		if(isFiring && ReadyToFire && !DM.dialogueOpen && !INV.canvas_enabled)
		{
			anim.SetBool("IsAttacking",true);
			ReadyToFire = false;
			rotate_once = true;

			for(int i = 0; i<projectile_amount; i++)
			{
				attack = (GameObject) Instantiate(projectile,(Vector2)transform.position + offset, Quaternion.identity);
				attack.name = "Projectile: "+i;
				projectile_storage.Add(attack);
				projectile_destruction.Add(attack);
				rangeCounter.Add(range);
			}

			coolDownCounter.Add(cooldownTime);
			player_actual = player_face;
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

		rangeCounter = rangeCounter.Select(x => x - Time.deltaTime).ToList(); 
		coolDownCounter = coolDownCounter.Select(x => x - Time.deltaTime).ToList();

		for(int i = 0; i < rangeCounter.Count; i++)
		{
			if(rangeCounter[i] <= 0 && projectile_destruction.Count > 0)
			{
				Destroy(projectile_destruction[0]);
				projectile_destruction.RemoveAt(0);
				rangeCounter.RemoveAt(0);
			}
		}

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

	public void SwapAttack(Magic selected_attack)
	{
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
			default:
				Debug.Log("Failed to load: " + selected_attack.ToString());
				projectile = Resources.Load<GameObject>("Magic/fireball");
				break;
		}
	}

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

	public void Attach(Sprite sprite, GameObject enemy)
	{
		Debug.Log("Attach");
		foreach(GameObject attack in projectile_storage)
		{
			attack.GetComponent<SpriteRenderer>().sprite = sprite;
			attack.transform.localPosition = enemy.transform.position;
			attack.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		projectile_storage.Clear();
	}
}
