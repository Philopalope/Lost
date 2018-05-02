using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop_Dialogue : MonoBehaviour {

	//References to shop component and script
	public GameObject shopWindow;
	private static Shop shop;

	//Item, Cost, and Sprite specific to each NPC
	public int[] gold_cost;
	public ItemName[] items;
	public Sprite[] shopSprite;

	//Check if shop is already created
	private bool shopExists;

	void Start () 
	{
		shopExists = false;
		shop = shopWindow.GetComponent<Shop>();
	}

	//If player collides with NPC that contains this script
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			//Start initialization of shop when player hits E and shop does not exist
			if(Input.GetKeyDown(KeyCode.E) && !shopExists)
			{
				if(GetComponent<Quests>() != null)
				{
					if(other.gameObject.GetComponent<Player_StatManager>().inventory.ItemInInventory(GetComponent<Quests>().QuestItem))
					{
						GetComponent<Quests>().GiveRewards(other.gameObject);
					}
				}

				shop.CreateShop(items, gold_cost, shopSprite, other.gameObject);
				shopExists = true;
				shopWindow.SetActive(true);
			}
			//Start deconstruction of shop when player hits Q and shop exists
			else if(Input.GetKeyDown(KeyCode.Q) && shopExists)
			{
				shop.DestroyShop();
				shopExists = false;
				shopWindow.SetActive(false);
			}
		}
	}
}
