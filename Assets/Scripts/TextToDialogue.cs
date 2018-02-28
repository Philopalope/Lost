using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToDialogue : MonoBehaviour {

	private DialogueManager DM;

	public string[] lines;

	void Start () 
	{
		DM = FindObjectOfType<DialogueManager>();
	}
	
	void Update () 
	{

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			if(Input.GetKeyDown(KeyCode.E) && DM.GetComponent<DialogueManager>().dialogueOpen == false
				&& DM.GetComponent<DialogueManager>().dialogueReady == true && DM.releasedE)
			{
				DM.lines = lines;
				DM.currentLine = 0;
				DM.DisplayDialogue();
				DM.releasedE = false;


				if(GetComponentInParent<NPC_RandomMovement>() != null)
				{
					GetComponentInParent<NPC_RandomMovement>().inDialogue = true;
				}
			}
		}
	}
}
