using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public GameObject dialogueBox;
	public Text dialogueText;

	public bool dialogueOpen;
	public bool dialogueReady;
	public bool shopDialogue;
	public bool exitFrame;
	public bool noGold;
	public bool releasedE;

	public string[] lines;
	public int currentLine;

	private float menuCooldownTime = 0.5f;
	private float menuCooldownCount;

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
		if(Input.GetKeyUp(KeyCode.E))
		{
			releasedE = true;
		}

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

		if(currentLine >= lines.Length)
		{
			dialogueBox.SetActive(false);
			dialogueOpen = false;
			currentLine = 0;
		}

		if(!noGold)
		{
			dialogueText.text = lines[currentLine];
		}
	}

	public void DisplayDialogue()
	{
		dialogueBox.SetActive(true);
		dialogueOpen = true;
		dialogueReady = false;
		menuCooldownCount = menuCooldownTime;

		player.inDialogue = true;
	}
}
