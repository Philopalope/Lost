using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Shop : MonoBehaviour {

	//Shop Template
	public GameObject shopPrefab;

	//Player Information
	private static Player_StatManager playerStats;
	private static GameObject playerReference;

	//Shop variables for shop window
	private ShopItem shopImage;
	private RectTransform shopRect;
	private Text shopInfo;

	//Shop stats
	private int items_in_shop;
	private float shopHeight;
	private float titlePadding = 20f;

	//Lists of shop object, item, and cost
	private List<GameObject> shopStock = new List<GameObject>();
	public static List<ItemName> ItemList = new List<ItemName>();
	public static List<int> ItemCost = new List<int>();

	void Start()
	{
		playerStats = FindObjectOfType<Player_StatManager>();
	}

	//Convert enum to string
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

	//Create shop window for NPC being interacted with
	public void CreateShop(ItemName[] items, int[] cost, Sprite[] shopSprites, GameObject player)
	{
		playerReference = player;
		playerReference.GetComponent<PlayerMovement>().pause = true;

		shopImage = shopPrefab.GetComponentInChildren<ShopItem>();

		//Ensure each item has a cost and a sprite attached to it
		if(items.Length != cost.Length || items.Length != shopSprites.Length)
		{
			throw new Exception("Items in Shop do not match up to Cost Array... please check attached shop scripts");
		}

		shopHeight = 52*items.Length + titlePadding;
		shopRect = GetComponent<RectTransform>();

		shopRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,shopHeight);
		items_in_shop = items.Length;

		//Instantiate shop items
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

	//Destroy objects associated with shop (only 1 shop ever instantiated at a single time)
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
		playerReference.GetComponent<PlayerMovement>().pause = false;
	}

	//Give item associated with button clicked to player and charge gold
	public void PurchaseItem()
	{
		//index associated with position of shop slot
		int index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();

		if(!playerStats.ChargeGold(ItemCost[index]))
		{
			return;
		}

		//Gives item to player
		switch(ItemList[index])
		{
			case ItemName.HPotion:
				Instantiate(Resources.Load<GameObject>("Items/Health Potion (Large)"),playerReference.transform.position,Quaternion.identity);
				break;
			case ItemName.MPotion:
				Instantiate(Resources.Load<GameObject>("Items/Magic Potion (Large)"),playerReference.transform.position,Quaternion.identity);
				break;
			case ItemName.Inn:
				playerStats.HealPlayer(99999);
				break;
		}
		
	}
}
