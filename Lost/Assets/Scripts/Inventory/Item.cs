﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Items and Quest Items defined globally
public enum ItemName{HPotion, MPotion, Backpack, Inn, Food, QuestItem};
public enum QuestItem{None, Gloves};

//CHANGE
public class Item : MonoBehaviour 
{
	//Item info and player stat manager
	private Player_StatManager PSM;
	private Inventory IM;
	public enum ItemRarity{Common, Rare, Legendary, Quest};

	//Stored item type and rarity
	public ItemName type;
	public ItemRarity rarity;

	//Item sprites
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;

	//Item usability, size of stack, and description in inventory
	public bool useableItem;
	public int maxSize;
	public int numberOfItems;
	public int restorePointsAmount;
	public string description;

	void Start()
	{
		PSM = FindObjectOfType<Player_StatManager>();
		IM = FindObjectOfType<Inventory>();
	}

	//Do something based on item used
	public void UseItem()
	{
		switch(type)
		{
			case ItemName.HPotion:
				PSM.HealPlayer(restorePointsAmount);
				break;
			case ItemName.MPotion:
				PSM.HealMagic(restorePointsAmount);
				break;
			case ItemName.Backpack:
				IM.IncreaseRows(restorePointsAmount);
				break;
			case ItemName.Food:
				PSM.HealPlayer(restorePointsAmount);
				break;
		}
	}

	//Convert enum to string of item
	public string ItemToString(ItemName item)
	{
		switch(item)
		{
			case ItemName.HPotion:
				return "Health Potion";
			case ItemName.MPotion:
				return "Magic Potion";
			case ItemName.QuestItem:
				return "Quest Item"; //TO DO --- CHANGE TO QUEST ITEM NAME
		}
		return null;
	}

	//Get all info of item and convert to string for tool tip in inventory
	public string GetTooltip()
	{
		string color = string.Empty;
		string newLine = string.Empty;

		if(description != string.Empty)
		{
			newLine = "\n";

		}

		//Change color based on rarity
		switch(rarity)
		{
			case ItemRarity.Common:
				color = "white";
				break;
			case ItemRarity.Rare:
				color = "teal";
				break;
			case ItemRarity.Legendary:
				color = "pink";
				break;
			case ItemRarity.Quest:
				color = "orange";
				break;
		}

		//TO DO --- Add stats for other stats such as attack power and defense

		return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>" + newLine + "{1}</color></i></size>", ItemToString(type),description);
	}
}
