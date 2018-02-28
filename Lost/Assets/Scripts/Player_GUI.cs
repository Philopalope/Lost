using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_GUI : MonoBehaviour {

	public Slider healthbar;
	public Text health_Text;
	public Player_StatManager playerHP;
	private int displayedHealth;

	void Start () {
		
	}
	
	void Update () {
		healthbar.maxValue = playerHP.playerMaxHealth;
		healthbar.value = playerHP.playerCurrentHealth;
		health_Text.text = "HP: " + playerHP.playerCurrentHealth + " / " + playerHP.playerMaxHealth;
	}
}
