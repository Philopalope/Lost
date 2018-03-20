using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchImage : MonoBehaviour {
	private RectTransform background_rect;
	void Start () {
		background_rect = GetComponent<RectTransform>();
		background_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
		background_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
	}
	
}
