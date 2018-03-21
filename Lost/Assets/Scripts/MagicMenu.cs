using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMenu : MonoBehaviour 
{
	private Canvas CanvasObject;
	private RectTransform menu_rect;
	private bool MMCanvas_enabled;

	void Start()
	{
		CanvasObject = GetComponentInParent<Canvas>();
		menu_rect = GetComponentInParent<RectTransform>();
		CanvasObject.enabled = false;
		MMCanvas_enabled = false;

		CreateMagicMenu();
	}

	//Create Magic Menu
	private void CreateMagicMenu()
	{
		menu_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width/1.5f);
		menu_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height/1.5f);
	}

	//Toggle Menu on and off
	public void ToggleMagicMenu()
	{
		CanvasObject.enabled = !CanvasObject.enabled;
		MMCanvas_enabled = !MMCanvas_enabled;
	}

}
