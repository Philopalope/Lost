using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

	public void UpdateShopSprite(Sprite itemSprite)
	{
		GetComponent<Image>().sprite = itemSprite;
	}

}
