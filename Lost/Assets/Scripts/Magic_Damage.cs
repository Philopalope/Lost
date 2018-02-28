using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Damage : MonoBehaviour {

	public int damage_dealt;
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
			other.gameObject.GetComponent<Enemy_Stats>().doDamage(damage_dealt);
			var clone = (GameObject) Instantiate(DamageNumber,transform.position,Quaternion.identity);
			clone.transform.position = new Vector2(transform.position.x,transform.position.y);
			clone.GetComponent<DisplayDamageNumber>().damageNumber = damage_dealt;
			
			//Flash Red
		}
		else if(other.gameObject.tag == "Magic")
		{
			projectile.TargetHit();
		}
	}

	public void DestroyOnAnimation()
	{
		Destroy(projectile);
	}
}
