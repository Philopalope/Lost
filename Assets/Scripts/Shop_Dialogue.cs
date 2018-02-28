using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop_Dialogue : MonoBehaviour {

	private DialogueManager DM;
	private Player_StatManager Stats;
	public string[] lines;
	public KeyCode[] options;

	public int[] gold_cost;

	public enum FunctionName{Inn,Exit};
	public enum ItemName{Potion};
	public FunctionName[] FunctionList;

	delegate void functionCalls();
	functionCalls CallFunction;

	void Start () 
	{
		DM = FindObjectOfType<DialogueManager>();
		Stats = FindObjectOfType<Player_StatManager>();
	}
	
	void Update () 
	{

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			if(Input.GetKeyDown(KeyCode.E) && DM.GetComponent<DialogueManager>().dialogueOpen == false
				&& DM.GetComponent<DialogueManager>().dialogueReady == true)
			{
				DM.lines = lines;
				DM.currentLine = 0;
				DM.DisplayDialogue();
				DM.shopDialogue = true;
				DM.exitFrame = false;
				DM.noGold = false;
			}
			else if(DM.GetComponent<DialogueManager>().dialogueOpen == true && !DM.exitFrame)
			{
				for(int i = 0; i<options.Length;i++)
				{
					if(Input.GetKeyDown(options[i]))
					{
						EnumToFunctionCall(FunctionList[i],i);
					}
				}
			}
			else if(DM.exitFrame)
			{
				if(Input.GetKeyDown(KeyCode.E))
				{
					DM.currentLine = lines.Length;
				}
			}
		}
	}

	void EnumToFunctionCall(FunctionName functionType, int index)
	{
		switch(functionType)
		{
			case FunctionName.Inn:
				Inn(index);
				break;
			case FunctionName.Exit:
				ExitDialogue();
				break;
		}
	}

	public void Inn(int index)
	{
		if(ChangeGold(gold_cost[index]))
		{
			Stats.HealPlayer(Stats.playerMaxHealth);
		}	
	}

	public void AddItem(ItemName item)
	{
		//ADD MORE
	}

	public bool ChangeGold(int gold)
	{
		DM.exitFrame = true;
		if(Stats.gold >= gold)
		{
			Stats.gold -= gold;
			DM.currentLine = 1;
			return true;
		}
		else
		{
			DM.dialogueText.text = "You don't have enough gold!";
			DM.noGold = true;
			return false;
		}
	}

	public void ExitDialogue()
	{
		DM.currentLine = lines.Length - 1;
		DM.exitFrame = true;
	}
}
