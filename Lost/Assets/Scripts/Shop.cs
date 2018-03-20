using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Shop : MonoBehaviour {

	public GameObject shopPrefab;
	private static Player_StatManager playerStats;
	private static GameObject playerReference;
	private static ShopItem shopImage;
	private RectTransform shopRect;
	private Text shopInfo;

	private int items_in_shop;
	private float shopHeight;
	private float titlePadding = 20f;

	private List<GameObject> shopStock = new List<GameObject>();
	public static List<ItemName> ItemList = new List<ItemName>();
	public static List<int> ItemCost = new List<int>();

	void Start()
	{
		playerStats = FindObjectOfType<Player_StatManager>();
	}

	public string GetItemName(ItemName item)
	{
		switch(item)
		{
			case ItemName.HPotion:
				return "Health Potion";
			case ItemName.MPotion:
				return "Magic Potion";
			case ItemName.Inn:
				return "Rest";
			default:
				return "Error Getting Item";
		}
	}

	public void CreateShop(ItemName[] items, int[] cost, Sprite[] shopSprites, GameObject player)
	{
		playerReference = player;
		shopImage = shopPrefab.GetComponentInChildren<ShopItem>();
		Debug.Log(shopSprites[0]);
		if(items.Length != cost.Length || items.Length != shopSprites.Length)
		{
			throw new Exception("Items in Shop do not match up to Cost Array... please check attached shop scripts");
		}

		shopHeight = 52*items.Length + titlePadding;
		shopRect = GetComponent<RectTransform>();

		shopRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,shopHeight);
		items_in_shop = items.Length;

		for(int i = 0; i < items.Length; i++)
		{
			shopImage.UpdateShopSprite(shopSprites[i]);
			GameObject shopItem = (GameObject) Instantiate(shopPrefab);
			shopInfo = shopItem.GetComponentInChildren<Text>();
			

			shopStock.Add(shopItem);
			ItemList.Add(items[i]);
			ItemCost.Add(cost[i]);

			RectTransform shopItemRect = shopItem.GetComponent<RectTransform>();

			shopItem.name = "Stock " + i;
			shopInfo.text = string.Format(GetItemName(items[i]) + "\n" + cost[i].ToString() + " Gold");

			shopItem.transform.SetParent(this.transform);
			shopItem.transform.SetSiblingIndex(i);

			shopItemRect.localPosition = new Vector2(GetComponent<RectTransform>().rect.width/2, -(titlePadding + shopItemRect.rect.height*i));
		}
	}

	public void DestroyShop()
	{
		for(int i = 0; i < items_in_shop; i++)
		{
			Destroy(shopStock[i]);
		}
		items_in_shop = 0;
		shopStock.Clear();
		ItemList.Clear();
		ItemCost.Clear();
	}

	public void PurchaseItem()
	{
		int index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();

		if(!playerStats.ChargeGold(ItemCost[index]))
		{
			return;
		}
		//Make Sound on Item Purchase
		switch(ItemList[index])
		{
			case ItemName.HPotion:
				Instantiate(Resources.Load<GameObject>("Items/Health Potion (Large)"),playerReference.transform.position,Quaternion.identity);
				break;
			case ItemName.MPotion:
				break;
			case ItemName.Inn:
				playerStats.HealPlayer(99999);
				break;
		}
		
	}
}
