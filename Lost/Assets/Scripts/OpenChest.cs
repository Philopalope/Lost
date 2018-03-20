using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {

	Animator anim;
	private Player_StatManager player_items;

	public enum Contents{HPotion, MPotion, Random};
	public Contents[] Items;
	public int chest_level;
	public int gold;

	public enum QuestItem{None, Gloves};
	public QuestItem questItem;


	void Start () 
	{
		anim = GetComponent<Animator>();
		player_items = FindObjectOfType<Player_StatManager>();
	}
	
	
	void Update () 
	{
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			if(Input.GetKeyDown(KeyCode.E) && !anim.GetBool("Open"))
			{
				anim.SetBool("Open",true);
				GiveItems(other.gameObject);
			}
		}
	}

	public void GiveItems(GameObject player)
	{
		int gold_portion = gold*Random.Range(1,chest_level);
		player_items.gold += gold_portion;
		Debug.Log("Gold Given: " + gold_portion);
		foreach(Contents itemType in Items)
		{
			switch(itemType)
			{
				case Contents.HPotion:
					Debug.Log("You found a potion!");
					Instantiate(Resources.Load<GameObject>("Items/Health Potion (Large)"),player.transform.position,Quaternion.identity);
					break;
				case Contents.MPotion:
					break;	
			}
		}

		if(gameObject.tag == "QuestChest")
		{
			switch(questItem)
			{
				case QuestItem.Gloves:
					Instantiate(Resources.Load<GameObject>("Items/Gloves"),player.transform.position,Quaternion.identity);
					break;
				default:
					Debug.Log("Unable to Load Quest Item! Check OpenChest.cs code and contents");
					break;
			}
		}

	}
}
