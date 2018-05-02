using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	//Camera variables
	public Transform target;
	public float m_speed = 0.1f;
	Camera mycam;
	
	void Start () 
	{
		mycam = Camera.main;
	}
	
	//Follows player on screen
	void FixedUpdate () 
	{
		mycam.orthographicSize = (Screen.height / 100f) / 2.5f;
		if(target)
		{
			transform.position = Vector3.Lerp(transform.position, target.position, m_speed);
		}
	}
}
