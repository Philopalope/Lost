using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CHANGE
public class Item : MonoBehaviour 
{

	private Player_StatManager PSM;
	public enum ItemType{HPotion, MPotion, QuestItem};
	public enum LevelOfItem{Small, Medium, Large, NoSize};

	public ItemType type;
	public LevelOfItem level;

	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;

	public bool useableItem;
	public int maxSize;
	public string description;

	void Start()
	{
		PSM = FindObjectOfType<Player_StatManager>();
	}

	public void UseItem()
	{
		switch(type)
		{
			case ItemType.HPotion:
				PSM.HealPlayer(50);
				break;
			case ItemType.MPotion:
				Debug.Log("Mana");
				break;
		}
	}

	public string ItemToString(ItemType item)
	{
		switch(item)
		{
			case ItemType.HPotion:
				return "Health Potion";
			case ItemType.MPotion:
				return "Magic Potion";
			case ItemType.QuestItem:
				return "Quest Item"; //CHANGE TO ACCOMODATE NAME
		}
		return "ERROR";
	}

	public string GetTooltip()
	{
		string color = string.Empty;
		string newLine = string.Empty;

		if(description != string.Empty)
		{
			newLine = "\n";

		}

		//Change later to accomodate item rarity
		switch(level)
		{
			case LevelOfItem.Small:
				color = "green";
				break;
			case LevelOfItem.Medium:
				color = "orange";
				break;
			case LevelOfItem.Large:
				color = "magenta";
				break;
			case LevelOfItem.NoSize:
				color = "white";
				break;
		}

		//Add stats for other stats such as attack power and defense

		return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>" + newLine + "{1}</color></i></size>", ItemToString(type),description);
	}
}
