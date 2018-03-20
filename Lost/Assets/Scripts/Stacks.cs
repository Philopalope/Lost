using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//CHANGE
public class Stacks : MonoBehaviour, IPointerClickHandler
{

	private Stack<Item> items;

	public Text stackText;

	public Sprite slotEmpty;
	public Sprite slotHighlight;

	public Item CurrentItem{ get {return items.Peek();}}
	public bool IsAvailable{ get {return CurrentItem.maxSize > items.Count; }}
	public bool IsEmpty { get{ return items.Count == 0;}}

	public Stack<Item> Items
	{
		get {return items;}
		set {items = value;}
	}

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

	public void AddItem(Item itemAdd)
	{
		items.Push(itemAdd);

		if(items.Count > 1)
		{
			stackText.text = items.Count.ToString();
		}

		ChangeSprite(itemAdd.spriteNeutral, itemAdd.spriteHighlighted);
	}

	public void AddItems(Stack<Item> itemsAdd)
	{
		this.items = new Stack<Item>(itemsAdd);
		stackText.text = itemsAdd.Count > 1 ? itemsAdd.Count.ToString() : string.Empty;
		ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
	}

	private void ChangeSprite(Sprite neutral, Sprite highlighted)
	{
		GetComponent<Image>().sprite = neutral;

		SpriteState ss = new SpriteState();

		ss.highlightedSprite = highlighted;

		ss.pressedSprite = neutral;

		GetComponent<Button>().spriteState = ss;
	}

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

	public void ClearSlot()
	{
		items.Clear();
		ChangeSprite(slotEmpty,slotHighlight);
		stackText.text = string.Empty;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.button == PointerEventData.InputButton.Right)
		{
			UseItem();
		}
	}
}
