using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemName{HPotion, MPotion, Inn};

public class Shop_Dialogue : MonoBehaviour {

	public GameObject shopWindow;
	private static Shop shop;

	public int[] gold_cost;
	public ItemName[] items;
	public Sprite[] shopSprite;

	private bool shopExists;

	void Start () 
	{
		shopExists = false;
		shop = shopWindow.GetComponent<Shop>();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			if(Input.GetKeyDown(KeyCode.E) && !shopExists)
			{
				shop.CreateShop(items, gold_cost, shopSprite, other.gameObject);
				shopExists = true;
				shopWindow.SetActive(true);
			}
			else if(Input.GetKeyDown(KeyCode.Q) && shopExists)
			{
				shop.DestroyShop();
				shopExists = false;
				shopWindow.SetActive(false);
			}
		}
	}
}
