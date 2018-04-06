using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {

	//Animation of chest and player stats;
	Animator anim;
	private Player_StatManager player_items;

	//Contents of chest
	public ItemName[] Items;
	public QuestItem questItem;
	public int chest_level;
	public int gold;

	void Start () 
	{
		anim = GetComponent<Animator>();
		player_items = FindObjectOfType<Player_StatManager>();
	}

	//Player collision with chest
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

	//Gives contents of chest to player
	public void GiveItems(GameObject player)
	{
		//Give gold to player
		int gold_portion = gold*Random.Range(1,chest_level);
		player_items.gold += gold_portion;
		Debug.Log("Gold Given: " + gold_portion);

		GameObject itemGiven;
		//Give items to player
		foreach(ItemName itemType in Items)
		{
			switch(itemType)
			{
				case ItemName.HPotion:
					itemGiven = Instantiate(Resources.Load<GameObject>("Items/Health Potion (Large)"),player.transform.position,Quaternion.identity);
					itemGiven.name = "Health Potion (Large)";
					break;
				case ItemName.MPotion:
					itemGiven = Instantiate(Resources.Load<GameObject>("Items/Magic Potion (Large)"),player.transform.position,Quaternion.identity);
					itemGiven.name = "Magic Potion (Large)";
					break;	
			}
		}

		//Chests that contain quest items must give quest item seperately
		if(gameObject.tag == "QuestChest")
		{
			switch(questItem)
			{
				case QuestItem.Gloves:
					itemGiven = Instantiate(Resources.Load<GameObject>("Items/Gloves"),player.transform.position,Quaternion.identity);
					itemGiven.name = "Gloves";
					break;
				default:
					Debug.Log("Unable to Load Quest Item! Check OpenChest.cs code and contents");
					break;
			}
		}

	}
}
