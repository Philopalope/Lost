using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CHANGE
public class Inventory : MonoBehaviour
{
	private Canvas CanvasObject;
	private RectTransform inventoryRect;

	private float inventoryWidth, inventoryHeight;

	public int slots;
	public int rows;

	public float slotPaddingLeft, slotPaddingTop;

	public float slotSize;
	public GameObject slotPrefab;

	private List<GameObject> allSlots;

	private int emptySlot;

	void Start()
	{
		CanvasObject = GetComponentInParent<Canvas>();
		CreateLayout();
		CanvasObject.enabled = false;
	}

	//Create Inventory Layout
	private void CreateLayout()
	{
		Debug.Log("CREATE");
		allSlots = new List<GameObject>();
		emptySlot = slots;

		inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
		inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

		inventoryRect = GetComponent<RectTransform>();

		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

		int columns = slots / rows;

		for(int i = 0; i < rows; i++)
		{
			for(int j = 0; j < columns; j++)
			{
				GameObject newSlot = (GameObject) Instantiate(slotPrefab);

				RectTransform slotRect = newSlot.GetComponent<RectTransform>();

				newSlot.name = "Slot";
				newSlot.transform.SetParent(this.transform.parent);

				slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (j + 1) + (slotSize * j), -slotPaddingTop * (i+1) - (slotSize * i));

				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

				newSlot.transform.SetParent(this.transform);
				allSlots.Add(newSlot);
			}
		}
	}

	//Check to add item
	public bool AddItem(Item itemAdd)
	{
		if(itemAdd.maxSize == 1)
		{
			PlaceEmpty(itemAdd);
			return true;
		}
		return false;
	}

	//Check for empty slot
	private bool PlaceEmpty(Item itemCheck)
	{
		if(emptySlot > 0)
		{
			foreach ( GameObject slot in allSlots)
			{
				Stacks tmp = slot.GetComponent<Stacks>();
				if(tmp.IsEmpty())
				{
					tmp.AddItem(itemCheck);
					emptySlot--;
					return true;
				}
			}
		}

		return false;
	}

	public void ToggleInventory()
	{
		CanvasObject.enabled = !CanvasObject.enabled;
	}
}

