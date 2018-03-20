using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Damage : MonoBehaviour {

	public int damage_dealt;
	public Sprite change_sprite;
	private Attack projectile;
	private GameObject DamageNumber;
	private GameObject enemy;

	void Start () 
	{
		projectile = FindObjectOfType<Attack>();
		DamageNumber = Resources.Load<GameObject>("Map/Damage");
	}
	

	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if(gameObject.tag == "Magic")
			{
				projectile.TargetHit();
			}
			else if(gameObject.tag == "Magic_attach")
			{
				projectile.Attach(change_sprite,other.gameObject);
			}
			other.gameObject.GetComponent<Enemy_Stats>().doDamage(damage_dealt);
			var clone = (GameObject) Instantiate(DamageNumber,transform.position,Quaternion.identity);
			clone.transform.position = new Vector2(transform.position.x,transform.position.y);
			clone.GetComponent<DisplayDamageNumber>().damageNumber = damage_dealt;
			//projectile.TargetHit();
			//Flash Red
		}
	}

	public void DestroyOnAnimation()
	{
		Destroy(projectile);
	}
}
