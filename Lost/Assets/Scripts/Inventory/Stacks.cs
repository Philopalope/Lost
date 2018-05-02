using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//CHANGE
public class Stacks : MonoBehaviour, IPointerClickHandler
{

	//Stack of items and their text
	private Stack<Item> items;
	public GameObject itemStored;
	public Text stackText;

	//Slot Sprites
	public Sprite slotEmpty;
	public Sprite slotHighlight;

	//Helper Functions
	public Item CurrentItem{ get {return items.Peek();}}
	public bool IsAvailable{ get {return CurrentItem.maxSize > items.Count; }}
	public bool IsEmpty { get{ return items.Count == 0;}}

	//Get private variable stack of items
	public Stack<Item> Items
	{
		get {return items;}
		set {items = value;}
	}

	//initialize variables, components, and objects
	void Start()
	{
		items = new Stack<Item>();
		RectTransform slotRect = GetComponent<RectTransform>();
		RectTransform textRect = stackText.GetComponent<RectTransform>();

		int textScaleFactor = (int) (slotRect.sizeDelta.x * 0.60);
		stackText.resizeTextMaxSize = textScaleFactor;
		stackText.resizeTextMinSize = textScaleFactor;

		textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
		textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
	}

	//Add item to Stack and change resulting sprite to item sprite
	public void AddItem(Item itemAdd)
	{
		items.Push(itemAdd);

		string item_path = itemAdd.name;
		itemStored = Resources.Load<GameObject>("Items/" + item_path);

		if(items.Count > 1)
		{
			stackText.text = items.Count.ToString();
		}

		ChangeSprite(itemAdd.spriteNeutral, itemAdd.spriteHighlighted);
	}

	//Change item stack and GameObject of item between slots 
	public void SwapItems(Stack<Item> itemsAdd, GameObject objectToSwap)
	{
		this.items = new Stack<Item>(itemsAdd);
		
		itemStored = objectToSwap;

		stackText.text = itemsAdd.Count > 1 ? itemsAdd.Count.ToString() : string.Empty;
		ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
	}

	//Change sprite of inventory slot
	private void ChangeSprite(Sprite neutral, Sprite highlighted)
	{
		GetComponent<Image>().sprite = neutral;

		SpriteState ss = new SpriteState();

		ss.highlightedSprite = highlighted;

		ss.pressedSprite = neutral;

		GetComponent<Button>().spriteState = ss;
	}

	//Check if item is useable and then use it (call function associated with item)
	private void UseItem()
	{
		if(!IsEmpty)
		{
			if(items.Peek().useableItem)
			{
				items.Pop().UseItem();
				stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
				if(IsEmpty)
				{
					ChangeSprite(slotEmpty, slotHighlight);
					Inventory.EmptySlots++;
				}
			}
			else
			{
				Debug.Log("You cannot use this item!");
			}
		}
	}

	//Remove sprite and text from slot
	public void ClearSlot()
	{
		items.Clear();
		itemStored = null;
		ChangeSprite(slotEmpty,slotHighlight);
		stackText.text = string.Empty;
	}

	//Handles mouseclick event
	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.button == PointerEventData.InputButton.Right)
		{
			UseItem();
		}
	}
}
