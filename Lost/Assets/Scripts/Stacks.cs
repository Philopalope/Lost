using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CHANGE
public class Stacks : MonoBehaviour {

	private Stack<Item> items;

	public Text stackText;

	public Sprite slotEmpty;
	public Sprite slotHighlight;

	public bool IsEmpty() 
	{
		Debug.Log(items.Count == 0);
		return items.Count == 0;
	} 

	void Start()
	{
		items = new Stack<Item>();
		RectTransform slotRect = GetComponent<RectTransform>();
		RectTransform textRect = GetComponent<RectTransform>();

		int textScaleFactor = (int) (slotRect.sizeDelta.x * 0.60);
		stackText.resizeTextMaxSize = textScaleFactor;
		stackText.resizeTextMinSize = textScaleFactor;

		textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
		textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
		Debug.Log("test");
		//this.transform.parent.gameObject.SetActive(false);
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

	private void ChangeSprite(Sprite neutral, Sprite highlighted)
	{
		GetComponent<Image>().sprite = neutral;

		SpriteState ss = new SpriteState();

		ss.highlightedSprite = highlighted;

		ss.pressedSprite = neutral;

		GetComponent<Button>().spriteState = ss;
	}
}
