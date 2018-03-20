using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_Dialogue : MonoBehaviour {

	private DialogueManager DM;

	public string[] lines;
	public KeyCode[] options;
	public bool one_time_information;
	public bool activation;

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
			if(Input.GetKeyUp(KeyCode.E) && DM.GetComponent<DialogueManager>().dialogueOpen == false
				&& DM.GetComponent<DialogueManager>().dialogueReady == true)
			{
				DM.lines = lines;
				DM.currentLine = 1;
				DM.DisplayDialogue();

				if(one_time_information)
				{
					DM.currentLine = 0;
					one_time_information = false;
					if(activation)
					{
						Debug.Log("Activate");

					}

				}

				if(GetComponentInParent<NPC_RandomMovement>() != null)
				{
					GetComponentInParent<NPC_RandomMovement>().inDialogue = true;
				}
			}
		}
	}
}
