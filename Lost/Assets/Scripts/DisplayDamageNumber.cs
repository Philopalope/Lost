using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamageNumber : MonoBehaviour {

	public float moveSpeed;
	public int damageNumber;
	public Text damageText;

	public float timeOfNumber;

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
