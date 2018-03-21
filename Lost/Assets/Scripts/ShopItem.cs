using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

	//Switches Shop slot's item sprite -> Specific to each slot
	public void UpdateShopSprite(Sprite itemSprite)
	{
		GetComponent<Image>().sprite = itemSprite;
	}

}
