using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour {

	public Item QuestItem;
	public int gold_reward;
	public int experience_reward;
	public string response;

	public void GiveRewards(GameObject player)
	{
		player.GetComponent<Player_StatManager>().gold += gold_reward;
		player.GetComponent<Player_StatManager>().experience += experience_reward;

		//Display Dialogue Response -- TO DO --
	}
}
