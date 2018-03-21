using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamageNumber : MonoBehaviour {

	//Damage number variables
	public float moveSpeed;
	public int damageNumber;
	public Text damageText;

	//Time of number displayed on screen
	public float timeOfNumber;

	//Display Damage number object when damage is given
	void Update () 
	{
		damageText.text = "" + damageNumber;
		transform.position = new Vector3(transform.position.x, transform.position.y+(moveSpeed*Time.deltaTime),transform.position.z);
		timeOfNumber -= Time.deltaTime;
		if(timeOfNumber < 0)
		{
			Destroy(gameObject);
		}
	}
}
