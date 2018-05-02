using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CHANGE
public class Inventory : MonoBehaviour
{
	//Inventory Canvas and Background
	private Canvas CanvasObject;
	private RectTransform inventoryRect;

	//Tooltip of item
	private static GameObject tooltip;
	public GameObject tooltipObj;

	//Text of item
	public Text visual_textObj;
	public Text sizeTextObj;
	private static Text sizeText;
	private static Text visual_text;

	//Inventory background resizing
	private float inventoryWidth, inventoryHeight;
	private float inventoryShiftX, inventoryShiftY;

	//Number of slots and rows of inventory
	public int slots;
	public int rows;

	//Defined for outside script to check if inventory is active
	public bool canvas_enabled;

	//Offset of inventory slots
	public float slotPaddingLeft, slotPaddingTop;

	//Size of slots and slot prefab
	private float slotSize = 40f;
	public GameObject slotPrefab;

	//Handle Item switching and discard
	private Stacks frm, to;
	private List<GameObject> allSlots;

	//Prefab of slot image
	public GameObject iconPrefab;

	//Number of empty slots in inventory
	private static int emptySlots;

	//Canvas reference
	public Canvas canvasRef;

	//private static GameObject hoverObject;
	//private float hoverYOffset;

	//Getter method
	public static int EmptySlots
	{
		get { return emptySlots;}
		set { emptySlots = value;}
	}

	void Start()
	{
		CanvasObject = GetComponentInParent<Canvas>();
		tooltip = tooltipObj;
		sizeText = sizeTextObj;
		visual_text = visual_textObj;
		CreateLayout();
		CanvasObject.enabled = false;
		canvas_enabled = false;
	}

	//Meant for drag and drop in inventory.  Postponed for now
	void Update()
	{
		// if(hoverObject != null)
		// {
		// 	Vector2 position;
		// 	RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRef.transform as RectTransform, Input.mousePosition, canvasRef.worldCamera, out position);
		// 	position.Set(position.x,position.y - hoverYOffset);
		// 	hoverObject.transform.position = canvasRef.transform.TransformPoint(position);
		// }
	}

	//Create Inventory Layout
	private void CreateLayout()
	{
		allSlots = new List<GameObject>();
		emptySlots = slots;

		//hoverYOffset = slotSize * 0.01f;

		//Inventory initialization
		inventoryWidth = (slots / rows) * (slotSize) + slotPaddingLeft*2;
		inventoryHeight = (rows) * (slotSize) + slotPaddingTop*2;

		inventoryRect = GetComponent<RectTransform>();

		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

		inventoryShiftX = (Screen.width/2) - (inventoryRect.rect.width/2);
		inventoryShiftY = (Screen.height/2) + (inventoryRect.rect.height/2);

		inventoryRect.position = new Vector3(inventoryShiftX,inventoryShiftY,inventoryRect.position.z);

		//Item "back pack" inventory
		for(int i = 0; i < rows; i++)
		{
			for(int j = 0; j < (slots/rows); j++)
			{
				GameObject newSlot = (GameObject) Instantiate(slotPrefab);

				RectTransform slotRect = newSlot.GetComponent<RectTransform>();

				newSlot.name = "Slot " + (j+i*(slots/rows));
				newSlot.transform.SetParent(this.transform.parent);

				slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft + (slotSize * j), -slotPaddingTop - (slotSize * i));
				newSlot.transform.SetParent(this.transform);

				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
				
				allSlots.Add(newSlot);
			}
		}
	}

	public void IncreaseRows(int row_increase)
	{
		slots += row_increase*(slots/rows);
		rows += row_increase;

		for(int i = 0; i < row_increase; i++)
		{
			for(int j = 0; j < (slots/rows); j++)
			{
				GameObject newSlot = (GameObject) Instantiate(slotPrefab);

				RectTransform slotRect = newSlot.GetComponent<RectTransform>();

				newSlot.name = "Slot " + j + (slots*rows);
				newSlot.transform.SetParent(this.transform.parent);

				slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft + (slotSize * j), -slotPaddingTop - (slotSize*(rows-(i+1))));
				newSlot.transform.SetParent(this.transform);

				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

				allSlots.Add(newSlot);
			}
		}

		inventoryWidth = (slots / rows) * (slotSize) + slotPaddingLeft*2;
		inventoryHeight = (rows) * (slotSize) + slotPaddingTop*2;

		inventoryRect = GetComponent<RectTransform>();

		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

		inventoryShiftX = (Screen.width/2) - (inventoryRect.rect.width/2);
		inventoryShiftY = (Screen.height/2) + (inventoryRect.rect.height/2);

		inventoryRect.position = new Vector3(inventoryShiftX,inventoryShiftY,inventoryRect.position.z);
	}

	//Turn tooltip on when hovering over item
	public void ToggleTooltipOn(GameObject slot)
	{
		if(!slot.GetComponent<Stacks>().IsEmpty)
		{
			visual_text.text = slot.GetComponent<Stacks>().CurrentItem.GetTooltip();
			sizeText.text = visual_text.text;

			tooltip.SetActive(true);
			float x_position = slot.transform.position.x;
			float y_position = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y;

			tooltip.transform.position = new Vector2(x_position,y_position);
		}
		
	}

	//Turn tooltip off when mouse leaves item rect
	public void ToggleTooltipOff(GameObject slot)
	{
		tooltip.SetActive(false);
	}

	//Check if able to add item to slot
	public bool AddItem(Item itemAdd)
	{
		if(itemAdd.maxSize == 1)
		{
			PlaceEmpty(itemAdd);
			return true;
		}
		else
		{
			foreach(GameObject slot in allSlots)
			{
				Stacks slotStack = slot.GetComponent<Stacks>();
				if(!slotStack.IsEmpty)
				{
					if(slotStack.CurrentItem.type == itemAdd.type && slotStack.IsAvailable)
					{
						slotStack.AddItem(itemAdd);
						return true;
					}
				}
			}
			if(emptySlots > 0)
			{
				PlaceEmpty(itemAdd);
			}
		}
		return false;
	}

	//Check for an empty slot
	private bool PlaceEmpty(Item itemCheck)
	{
		if(emptySlots > 0)
		{
			foreach ( GameObject slot in allSlots)
			{
				Stacks slotEmpty = slot.GetComponent<Stacks>();
				if(slotEmpty.IsEmpty)
				{
					slotEmpty.AddItem(itemCheck);
					emptySlots--;
					return true;
				}
			}
		}

		return false;
	}

	//Move item from A to B in inventory
	public void MoveItem(GameObject clickedItem)
	{
		if(frm == null)
		{
			if(!clickedItem.GetComponent<Stacks>().IsEmpty)
			{
				frm = clickedItem.GetComponent<Stacks>();
				frm.GetComponent<Image>().color = Color.gray;

				// hoverObject = (GameObject) Instantiate(iconPrefab);
				// hoverObject.GetComponent<Image>().sprite = clickedItem.GetComponentInChildren<Image>().sprite;
				// hoverObject.name = "Selected Item";

				// RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
				// RectTransform clickedTransform = clickedItem.GetComponent<RectTransform>();

				// hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
				// hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);
				
				// hoverObject.transform.SetParent(GameObject.Find("Inventory").transform,true);
				// hoverObject.transform.localScale = frm.gameObject.transform.localScale;
			}
		}
		else if(to == null)
		{
			to = clickedItem.GetComponent<Stacks>();
			Destroy(GameObject.Find("Selected Item"));
		}

		//If both are not null, swap items
		if(to != null && frm != null)
		{
			Stack<Item> swapTo = new Stack<Item>(to.Items);
			GameObject swapObject = to.itemStored;

			to.SwapItems(frm.Items, frm.itemStored);

			if(swapTo.Count == 0)
			{
				frm.ClearSlot();
			}
			else
			{
				frm.SwapItems(swapTo, swapObject);
			}
			frm.GetComponent<Image>().color = Color.white;
			to = null;
			frm = null;
			// hoverObject = null;
		}
	}

	//Toggle inventory window
	public void ToggleInventory()
	{
		CanvasObject.enabled = !CanvasObject.enabled;
		canvas_enabled = !canvas_enabled;
	}

	public void DiscardItem()
	{
		if(frm != null && frm.itemStored.GetComponent<Item>().type != ItemName.QuestItem)
		{
			float drop_coordinate_x = Random.Range(-0.5f,0.5f);
			float drop_coordinate_y = Random.Range(-0.5f,0.5f);
			if(drop_coordinate_x < 0.3f && drop_coordinate_x > -0.3f)
			{
				drop_coordinate_x = 0.3f;
			}
			if(drop_coordinate_y < 0.3f && drop_coordinate_y > -0.3f)
			{
				drop_coordinate_y = 0.3f;
			}

			GameObject droppedItem = Instantiate(frm.itemStored,FindObjectOfType<Player_StatManager>().transform.position + new Vector3(0.3f,0,0),Quaternion.identity);
			droppedItem.name = frm.itemStored.name;
			droppedItem.GetComponent<Item>().numberOfItems = frm.Items.Count;
			frm.ClearSlot();
			frm.GetComponent<Image>().color = Color.white;
			frm = null;
		}
		else
		{
			Debug.Log("Cannot Drop Item");
		}
	}

	public bool ItemInInventory(Item item_searched)
	{
		foreach(GameObject item in allSlots)
		{
			if(item.GetComponent<Stacks>().itemStored != null)
			{
				if(item.GetComponent<Stacks>().itemStored.name == item_searched.name)
				{
					item.GetComponent<Stacks>().ClearSlot();
					return true;
				}
			}
		}
		return false;

	}
}

