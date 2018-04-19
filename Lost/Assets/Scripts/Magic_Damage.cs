using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Damage : MonoBehaviour {

	//Damage and sprite associated with magic attack
	public int damage_dealt_base;
	public int damage_dealt_calculated;
	public int multiplier;
	public Sprite change_sprite;

	//Handles projectile object and damage number
	private Attack projectile;
	private GameObject DamageNumber;

	void Start () 
	{
		projectile = FindObjectOfType<Attack>();
		DamageNumber = Resources.Load<GameObject>("Map/Damage");
	}

	//CHANGE FOR DAMAGE DEALT
	void Update()
	{
		//TO DO -- ADD FOR 3 TYPES OF ATTACK
		multiplier = projectile.GetComponent<Attack>().multiplier_1;
		
		damage_dealt_calculated = damage_dealt_base * multiplier;
	}

	//Checks for projectile -> enemy collision
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if(gameObject.tag == "Magic")
			{
				projectile.TargetHit();
			}
			//Displays damage as number on screen
			other.gameObject.GetComponent<Enemy_Stats>().doDamage(damage_dealt_calculated);
			var clone = (GameObject) Instantiate(DamageNumber,transform.position,Quaternion.identity);
			clone.transform.position = new Vector2(transform.position.x,transform.position.y);
			clone.GetComponent<DisplayDamageNumber>().damageNumber = damage_dealt_calculated;
			//projectile.TargetHit();

			//TO DO --- FLASH ENEMY DIFFERENT COLORS TO INDICATE DAMAGE 
		}
	}

	//Destroy projectile after animation is finished
	public void DestroyOnAnimation()
	{
		Destroy(projectile);
	}

	public void DestroyAfterAnimation()
	{
		Destroy(gameObject);
	}
}
