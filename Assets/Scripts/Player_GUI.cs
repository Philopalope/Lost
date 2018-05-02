using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_GUI : MonoBehaviour {

	//Health bar info displayed on GUI
	public Slider healthbar;
	public Slider manabar;
	public Text healthText;
	public Text skillpointText;
	public Text manaText;
	public Text goldText;
	public Text pauseText;
	public Player_StatManager playerRef;
	private int displayedHealth;

	private bool game_pause;

	//Update player stats into GUI
	void Update () {
		healthbar.maxValue = playerRef.playerMaxHealth;
		manabar.maxValue = playerRef.playerMaxMP;
		healthbar.value = playerRef.playerCurrentHealth;
		manabar.value = playerRef.playerCurrentMP;
		healthText.text = "HP: " + playerRef.playerCurrentHealth + " / " + playerRef.playerMaxHealth;
		manaText.text = "MP: " + playerRef.playerCurrentMP + " / " + playerRef.playerMaxMP;

		skillpointText.text = "Skill Points: " + playerRef.magic_points;
		goldText.text = "Gold: " + playerRef.gold;


		if(Input.GetKeyDown(KeyCode.P))
		{
			if(!game_pause)
			{
				Time.timeScale = 0;
				game_pause = true;
				pauseText.enabled = true;
			}
			else
			{
				Time.timeScale = 1;
				game_pause = false;
				pauseText.enabled = false;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
