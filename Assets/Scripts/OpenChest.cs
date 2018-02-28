using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {

	Animator anim;
	private Player_StatManager player_items;
	public enum Contents{HPotion, Key, QuestItem, MPotion};
	public Contents[] Items;
	public int chest_level;
	public int gold;

	void Start () 
	{
		anim = GetComponent<Animator>();
		player_items = FindObjectOfType<Player_StatManager>();
	}
	
	
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			if(Input.GetKeyDown(KeyCode.E) && !anim.GetBool("Open"))
			{
				anim.SetBool("Open",true);
				GiveItems();
			}
		}
	}

	public void GiveItems()
	{
		foreach(Contents items in Items)
		{
			player_items.gold += gold*Random.Range(5,10*chest_level);
			switch(items)
			{
				case Contents.HPotion:
					break;
				case Contents.MPotion:
					break;	
				case Contents.Key:
					break;
				case Contents.QuestItem:
					break;
			}
		}
	}
}
