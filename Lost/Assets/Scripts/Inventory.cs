using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CHANGE
public class Inventory : MonoBehaviour
{
	private Canvas CanvasObject;
	private RectTransform inventoryRect;

	private static GameObject tooltip;
	public GameObject tooltipObj;

	public Text visual_textObj;
	public Text sizeTextObj;
	private static Text sizeText;
	private static Text visual_text;

	private float inventoryWidth, inventoryHeight;
	private float inventoryShiftX, inventoryShiftY;

	public int slots;
	public int rows;

	public bool canvas_enabled;

	public float slotPaddingLeft, slotPaddingTop;

	private float slotSize = 40f;
	public GameObject slotPrefab;

	private Stacks frm, to;
	private List<GameObject> allSlots;

	public GameObject iconPrefab;
	private static GameObject hoverObject;

	private static int emptySlots;

	public Canvas canvasRef;
	private float hoverYOffset;

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

		inventoryWidth = (slots / rows) * (slotSize) + slotPaddingLeft*2;
		inventoryHeight = (rows) * (slotSize) + slotPaddingTop*2;

		inventoryRect = GetComponent<RectTransform>();

		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

		inventoryShiftX = (Screen.width/2) - (inventoryRect.rect.width/2);
		inventoryShiftY = (Screen.height/2) + (inventoryRect.rect.height/2);

		inventoryRect.position = new Vector3(inventoryShiftX,inventoryShiftY,inventoryRect.position.z);

		for(int x = 0; x < (slots/rows); x++)
		{
			GameObject playerSlot = (GameObject) Instantiate(slotPrefab);

			RectTransform playerSlotRect = playerSlot.GetComponent<RectTransform>();

			playerSlot.name = "Tab Slot " + x;
			playerSlot.transform.SetParent(this.transform.parent);
			playerSlot.transform.SetSiblingIndex(x);

			playerSlotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft + (slotSize * x), 50);
			playerSlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			playerSlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
		}

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

	public void ToggleTooltipOff(GameObject slot)
	{
		tooltip.SetActive(false);
	}

	//Check to add item
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

	//Check for empty slot
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
				RectTransform clickedTransform = clickedItem.GetComponent<RectTransform>();

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

		if(to != null && frm != null)
		{
			Stack<Item> swapTo = new Stack<Item>(to.Items);
			to.AddItems(frm.Items);

			if(swapTo.Count == 0)
			{
				frm.ClearSlot();
			}
			else
			{
				frm.AddItems(swapTo);
			}
			frm.GetComponent<Image>().color = Color.white;
			to = null;
			frm = null;
			hoverObject = null;
		}
	}

	public void ToggleInventory()
	{
		CanvasObject.enabled = !CanvasObject.enabled;
		canvas_enabled = !canvas_enabled;
	}

}

