using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	//Dialogue box and text in box
	public GameObject dialogueBox;
	public Text dialogueText;

	//Handles dialogue interaction
	public bool dialogueOpen;
	public bool dialogueReady;
	public bool shopDialogue;
	public bool exitFrame;
	public bool noGold;
	public bool releasedE;

	//Lines specific to NPC
	public string[] lines;
	public int currentLine;

	//Prevent user from spamming menu
	private float menuCooldownTime = 0.5f;
	private float menuCooldownCount;

	//Handle player movement
	private PlayerMovement player;

	void Start () 
	{
		player = FindObjectOfType<PlayerMovement>();
		dialogueBox.SetActive(false);
		dialogueOpen = false;
		dialogueReady = true;
		shopDialogue = false;
		releasedE = true;
	}
	
	void Update () 
	{
		//Handle user input and dialogue interaction
		if(Input.GetKeyUp(KeyCode.E))
		{
			releasedE = true;
		}

		//If user exists dialogue, start counter
		if(!dialogueOpen)
		{
			menuCooldownCount -= Time.deltaTime;
			if(menuCooldownCount < 0)
			{
				dialogueReady = true;
			}
		}
		else if(dialogueOpen && Input.GetKeyDown(KeyCode.E) && !shopDialogue && releasedE)
		{
			currentLine++;
			releasedE = false;
		}

		//If max lines reached, exit dialogue
		if(currentLine >= lines.Length)
		{
			dialogueBox.SetActive(false);
			dialogueOpen = false;
			currentLine = 0;
		}

		//If player is out of gold
		if(!noGold)
		{
			dialogueText.text = lines[currentLine];
		}
	}

	//Initialize dialogue box and set active
	public void DisplayDialogue()
	{
		dialogueBox.SetActive(true);
		dialogueOpen = true;
		dialogueReady = false;
		menuCooldownCount = menuCooldownTime;

		player.inDialogue = true;
	}
}
