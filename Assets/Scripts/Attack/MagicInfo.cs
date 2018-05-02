using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicInfo : MonoBehaviour {

	//Info of magic menu buttons and what gets upgraded
	public int cost;
	public int upgrade_multiplier;
	public float cooldown;
	public float range;
	public float speed;

	//Keeps track of slot in menu and which attack type it is
	public int magic_slot;
	public int menu_slot;

	//handles when something is purchased or can be purchased
	public bool unlockable;
	public bool purchased;
	public string description;

	void Start()
	{
		purchased = false;
	}

}
