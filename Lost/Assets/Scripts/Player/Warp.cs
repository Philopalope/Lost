using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

	public Transform warpTarget;

	//When player collides, warp to target
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			other.gameObject.transform.position = warpTarget.position;
			Camera.main.transform.position = warpTarget.position;
		}
	}
}
