using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{HPotion, MPotion};

//CHANGE
public class Item : MonoBehaviour 
{

	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;

	public int maxSize;

	public void UseItem()
	{
		switch(type)
		{
			case ItemType.HPotion:
				Debug.Log("Health");
				break;
			case ItemType.MPotion:
				Debug.Log("Mana");
				break;
		}
	}
}
