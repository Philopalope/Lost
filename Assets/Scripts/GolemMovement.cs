using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour {

	//Golem components
	private Rigidbody2D rbody;
	private Animator anim;

	//Reference to player for player to boss distance calculation
	private GameObject playerRef;
	private float agroRadius;
	private double agroDistanceX;
	private double agroDistanceY;

	//Golem speed
	public float speed;
	private float offsetNumX = 0.60f;
	private float offsetNumY = 0.5f;
	private Vector2 offset;
	private Vector2 xPosition;

	//Enemy variables referencing components
	public Collider2D AreaConfinement;
	private Vector2 minWalkPoint;
	private Vector2 maxWalkPoint;
	
	//Enemy variables handling projectiles fired
	private int projectiles_fired;
	private bool AttackInProgress;
	public enum BossStage{Aggrovated,Enraged,Beserk};
	public BossStage BossPhase;

	//Enemy variables handling movement
	private bool isWalking;
	private int direction_x;
	private int direction_y;
	private int direction_raw;

	//Golem Timers
	private float walkTime = 1.0f;
	private float walkCounter;
	private float waitTime;
	private float waitCounter;
	private float attackCooldownTime;
	private float attackCooldownCounter;
	private float projectileSpacing;
	private float projectileCounter;

	//Golem methods of attack
	public GameObject projectile;
	public GameObject miniGolemPrefab;
	private int miniGolems;
	private int miniGolemsSpawned;

	void Start () 
	{
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		playerRef = GameObject.Find("Player");

		//minWalkPoint = AreaConfinement.bounds.min;
		//maxWalkPoint = AreaConfinement.bounds.max;
		RandomDirection();

		walkCounter = walkTime;
		waitCounter = walkTime;
		attackCooldownCounter = attackCooldownTime;
		xPosition = (Vector2) transform.position + new Vector2(0f,-0.35f);
		miniGolemsSpawned = 0;
	}
	
	void FixedUpdate () 
	{
		attackCooldownCounter -= Time.deltaTime;

		if(anim.GetBool("IsAttacking"))
		{
			isWalking = false;
			anim.SetBool("IsMoving",false);
			waitCounter = waitTime;
		}

		if(GetComponent<Enemy_Stats>().enemyCurrentHealth < GetComponent<Enemy_Stats>().enemyMaxHealth/5)
		{
			BossPhase = BossStage.Beserk;
		}
		else if(GetComponent<Enemy_Stats>().enemyCurrentHealth < GetComponent<Enemy_Stats>().enemyMaxHealth/2)
		{
			BossPhase = BossStage.Enraged;
		}
		else
		{
			BossPhase = BossStage.Aggrovated;
		}

		switch(BossPhase)
		{
			case BossStage.Aggrovated:
				speed = 1.2f;
				agroRadius = 3.25f;
				attackCooldownTime = 3.0f;
				projectileSpacing = 0.15f;
				waitTime = 1.0f;
				miniGolems = 0;
				break;
			case BossStage.Enraged:
				speed = 1.35f;
				agroRadius = 3.35f;
				attackCooldownTime = 2.75f;
				projectileSpacing = 0.05f;
				waitTime = 0.65f;
				miniGolems = 1;
				break;
			case BossStage.Beserk:
				speed = 1.7f;
				agroRadius = 3.5f;
				attackCooldownTime = 2.5f;
				projectileSpacing = 0.01f;
				waitTime = 0.0f;
				miniGolems = 4;
				break;
		}

		//If Boss Phase contains mini golems and has not spawned total number, spawn them
		if(miniGolemsSpawned != miniGolems)
		{
			GameObject miniGolem = Instantiate(miniGolemPrefab, transform.position,Quaternion.identity);

			//Set parent to Golem Boss
			miniGolem.transform.SetParent(this.transform);
			miniGolem.name = "Mini Golem " + miniGolemsSpawned;
			miniGolemsSpawned++;
		}

		//If spike attack started, instantiate spikes with spacing and in order
		if(AttackInProgress)
		{
			projectileCounter -= Time.deltaTime;

			switch(BossPhase)
			{
				//Tier 1
				case BossStage.Aggrovated:
					if(projectiles_fired == 0 && projectileCounter < projectileSpacing*8f)
					{
						Instantiate(projectile, xPosition + offset*1.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 1 && projectileCounter < projectileSpacing*7f)
					{
						Instantiate(projectile, xPosition + offset*1.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 2 && projectileCounter < projectileSpacing*6f)
					{
						Instantiate(projectile, xPosition + offset*2.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 3 && projectileCounter < projectileSpacing*5f)
					{
						Instantiate(projectile, xPosition + offset*2.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 4 && projectileCounter < projectileSpacing*4f)
					{
						Instantiate(projectile, xPosition + offset*3.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 5 && projectileCounter < projectileSpacing*3f)
					{
						Instantiate(projectile, xPosition + offset*3.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 6 && projectileCounter < projectileSpacing*2f)
					{
						Instantiate(projectile, xPosition + offset*4.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 7 && projectileCounter < projectileSpacing*1f)
					{
						Instantiate(projectile, xPosition + offset*4.5f,Quaternion.identity);
						projectiles_fired++;
						AttackInProgress = false;
					}
					break;
				case BossStage.Enraged:
					if(projectiles_fired == 0 && projectileCounter < projectileSpacing*8f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*1.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*1.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*1.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*1.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 1 && projectileCounter < projectileSpacing*7f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*1.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*1.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*1.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*1.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 2 && projectileCounter < projectileSpacing*6f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*2.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*2.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*2.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*2.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 3 && projectileCounter < projectileSpacing*5f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*2.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*2.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*2.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*2.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 4 && projectileCounter < projectileSpacing*4f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*3.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*3.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*3.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*3.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 5 && projectileCounter < projectileSpacing*3f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*3.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*3.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*3.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*3.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 6 && projectileCounter < projectileSpacing*2f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*4.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*4.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*4.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*4.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 7 && projectileCounter < projectileSpacing*1f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*4.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*4.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*4.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*4.5f,Quaternion.identity);
						projectiles_fired++;
						AttackInProgress = false;
					}
					break;
				case BossStage.Beserk:
					if(projectiles_fired == 0 && projectileCounter < projectileSpacing*8f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*1.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*1.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*1.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*1.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 1 && projectileCounter < projectileSpacing*7f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*1.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*1.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*1.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*1.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 2 && projectileCounter < projectileSpacing*6f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*2.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*2.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*2.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*2.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 3 && projectileCounter < projectileSpacing*5f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*2.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*2.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*2.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*2.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 4 && projectileCounter < projectileSpacing*4f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*3.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*3.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*3.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*3.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 5 && projectileCounter < projectileSpacing*3f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*3.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*3.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*3.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*3.5f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 6 && projectileCounter < projectileSpacing*2f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*4.0f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*4.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*4.0f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*4.0f,Quaternion.identity);
						projectiles_fired++;
					}
					else if(projectiles_fired == 7 && projectileCounter < projectileSpacing*1f)
					{
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,offsetNumX)*4.5f,Quaternion.identity);
						Instantiate(projectile, (Vector2) transform.position + new Vector2(0,-offsetNumX)*4.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(offsetNumX,0)*4.5f,Quaternion.identity);
						Instantiate(projectile, xPosition + new Vector2(-offsetNumX,0)*4.5f,Quaternion.identity);
						projectiles_fired++;
						AttackInProgress = false;
					}
					break;
			}
			
			
		}

		if(!anim.GetBool("IsDead"))
		{
			if(!AttackInProgress)
			{
				AttackRadius();
			}

			if(isWalking && !anim.GetBool("IsAttacking"))
			{
				walkCounter -= Time.deltaTime;
				switch(direction_raw)
				{
					case 0: //Up
						rbody.velocity = new Vector2(0,speed);
						offset = new Vector2(0,offsetNumY);
						direction_x = 0;
						direction_y = 1;
						break;
					case 1: //Down
						rbody.velocity = new Vector2(0,-speed);
						offset = new Vector2(0,-offsetNumY);
						direction_x = 0;
						direction_y = -1;
						break;
					case 2: //Left
						rbody.velocity = new Vector2(-speed,0);
						offset = new Vector2(-offsetNumX,0);
						direction_x = -1;
						direction_y = 0;
						break;
					case 3: //Right
						rbody.velocity = new Vector2(speed,0);
						offset = new Vector2(offsetNumX,0);
						direction_x = 1;
						direction_y = 0;
						break;
				}
				if(walkCounter < 0)
				{
					isWalking = false;
					anim.SetBool("IsMoving",false);
					waitCounter = waitTime;
				}
			}
			else
			{
				waitCounter -= Time.deltaTime;
				rbody.velocity = Vector2.zero;
				//If wait counter hits 0, choose new direction and move
				if(waitCounter < 0)
				{
					RandomDirection();
				}
			}
			anim.SetFloat("Golem_x",direction_x);
			anim.SetFloat("Golem_y",direction_y);
		}
		else
		{
			rbody.velocity = Vector2.zero;
		}
	}

	public void RandomDirection()
	{
		direction_raw = Random.Range(0,4);
		isWalking = true;
		anim.SetBool("IsMoving",true);
		walkCounter = walkTime;
	}

	public void AttackRadius()
	{
		agroDistanceX = (double) Mathf.Abs(playerRef.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x);
		agroDistanceY = (double) Mathf.Abs(playerRef.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y);

		if(agroDistanceX < agroRadius && agroDistanceY < agroRadius && (agroDistanceX < 0.5f || agroDistanceY < 0.5f) && attackCooldownCounter < 0)
		{
			switch(agroDistanceX > agroDistanceY)
			{
				case true:
					//Right
					if(playerRef.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x > 0)
					{
						offset = new Vector2(offsetNumX,0);
						xPosition = (Vector2) transform.position + new Vector2(0f,-0.35f);
						direction_x = 1;
						direction_y = 0;
					}
					//Left
					else
					{
						offset = new Vector2(-offsetNumX,0);
						xPosition = (Vector2) transform.position + new Vector2(0f,-0.35f);
						direction_x = -1;
						direction_y = 0;
					}
					break;
				case false:
					//Up
					if(playerRef.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y > 0)
					{
						offset = new Vector2(0,offsetNumY);
						xPosition = (Vector2) transform.position;
						direction_x = 0;
						direction_y = 1;
					}
					//Down
					else
					{
						offset = new Vector2(0,-offsetNumY);
						xPosition = (Vector2) transform.position;
						direction_x = 0;
						direction_y = -1;
					}
					break;
			}

			anim.SetFloat("Golem_x",direction_x);
			anim.SetFloat("Golem_y",direction_y);
			anim.SetBool("IsAttacking",true);
		}
	}

	public void Attack()
	{
		projectileCounter = projectileSpacing*8.0f;
		attackCooldownCounter = attackCooldownTime;

		AttackInProgress = true;

		projectiles_fired = 0;
	}

	public void AttackAnimationEnded()
	{
		anim.SetBool("IsAttacking",false);
	}
}
