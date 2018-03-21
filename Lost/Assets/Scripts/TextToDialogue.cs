using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToDialogue : MonoBehaviour {

	//Dialogue Manager reference
	private DialogueManager DM;

	//Lines of NPC to output
	public string[] lines;

	void Start () 
	{
		DM = FindObjectOfType<DialogueManager>();
	}

	//When player collides with NPC
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{	
			//If player hits E and dialogue is ready
			if(Input.GetKeyDown(KeyCode.E) && DM.GetComponent<DialogueManager>().dialogueOpen == false
				&& DM.GetComponent<DialogueManager>().dialogueReady == true && DM.releasedE)
			{
				DM.lines = lines;
				DM.currentLine = 0;
				DM.DisplayDialogue();
				DM.releasedE = false;

				//Halt NPC
				if(GetComponentInParent<NPC_RandomMovement>() != null)
				{
					GetComponentInParent<NPC_RandomMovement>().inDialogue = true;
				}
			}
		}
	}
}
