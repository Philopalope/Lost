using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_RandomMovement : MonoBehaviour 
{
	//NPC movement speed
	public float NPC_speed = 1.1f;

	//NPC Components
	private Rigidbody2D NPCRbody;
	private Animator NPCAnim;
	private PlayerMovement player;
	public Collider2D AreaConfinement;

	//NPC variables referencing components
	private Vector2 minWalkPoint;
	private Vector2 maxWalkPoint;
	private bool NPC_Confined = false;

	//NPC directional and movement variables
	private bool isWalking;
	private bool storedWalking;
	private int direction_x;
	private int direction_y;
	private int direction_raw;

	//Handles how long NPC walks vs. stands still
	public float walkTime;
	private float walkCounter;
	public float waitTime;
	private float waitCounter;

	//Checks if NPC is in dialogue with player
	public bool inDialogue;

	//Dialogue Manager refernence
	private DialogueManager DM;

	void Start () 
	{
		NPCAnim = GetComponent<Animator>();
		NPCRbody = GetComponent<Rigidbody2D>();
		DM = FindObjectOfType<DialogueManager>();
		player = FindObjectOfType<PlayerMovement>();

		//Set confined area for NPC if one exists
		if(AreaConfinement != null)
		{
			minWalkPoint = AreaConfinement.bounds.min;
			maxWalkPoint = AreaConfinement.bounds.max;
			NPC_Confined = true;
		}

		waitCounter = waitTime;
		walkCounter = walkTime;

		RandomDirection();
	}
	
	void FixedUpdate () 
	{
		storedWalking = isWalking;

		//If not in dialogue, reinitialize their original state of walking or standing still
		if(!DM.dialogueOpen)
		{
			inDialogue = false;
			if(storedWalking && isWalking)
			{
				NPCAnim.SetBool("NPCMoving",true);
				storedWalking = false;
			}
		}
		//If in dialogue, halt and store walking state
		if(inDialogue)
		{
			storedWalking = NPCAnim.GetBool("NPCMoving");
			NPCRbody.velocity = Vector2.zero;

			//TO DO --- FIX DIALOGUE FACING MECHANIC FOR NPC'S

			NPCAnim.SetFloat("NPC_x",player.GetComponent<Animator>().GetFloat("Input_x")*-1);
			NPCAnim.SetFloat("NPC_y",player.GetComponent<Animator>().GetFloat("Input_y")*-1);
			NPCAnim.SetBool("NPCMoving",false);
			return;
		}

		//If walking, move in specified direction and animate
		if(isWalking)
		{
			walkCounter -= Time.deltaTime;

			//Each case statement contains the bounds of the confinement. Halt when bounds are reached
			switch(direction_raw)
			{
				case 0:	//Up
					NPCRbody.velocity = new Vector2(0,NPC_speed);
					if(NPC_Confined && transform.position.y > maxWalkPoint.y)
					{
						isWalking = false;
						NPCAnim.SetBool("NPCMoving",false);
						waitCounter = waitTime;
					}
					direction_x = 0;
					direction_y = 1;
					break;
				case 1:	//Down
					NPCRbody.velocity = new Vector2(0,-NPC_speed);
					if(NPC_Confined && transform.position.y < minWalkPoint.y)
					{
						isWalking = false;
						NPCAnim.SetBool("NPCMoving",false);
						waitCounter = waitTime;
					}
					direction_x = 0;
					direction_y = -1;
					break;
				case 2:	//Left
					NPCRbody.velocity = new Vector2(-NPC_speed,0);
					if(NPC_Confined && transform.position.x < minWalkPoint.x)
					{
						isWalking = false;
						NPCAnim.SetBool("NPCMoving",false);
						waitCounter = waitTime;
					}
					direction_x = -1;
					direction_y = 0;
					break;
				case 3:	//Right
					NPCRbody.velocity = new Vector2(NPC_speed,0);
					if(NPC_Confined && transform.position.x > maxWalkPoint.x)
					{
						isWalking = false;
						NPCAnim.SetBool("NPCMoving",false);
						waitCounter = waitTime;
					}
					direction_x = 1;
					direction_y = 0;
					break;
			}
			//When counter hits 0, halt
			if(walkCounter < 0)
			{
				isWalking = false;
				NPCAnim.SetBool("NPCMoving",false);
				waitCounter = waitTime;
			}
		}
		else
		{
			waitCounter -= Time.deltaTime;
			NPCRbody.velocity = Vector2.zero;
			//When wait counter hits 0, choose a new direction to walk in
			if(waitCounter < 0)
			{
				RandomDirection();
			}
		}
		NPCAnim.SetFloat("NPC_x",direction_x);
		NPCAnim.SetFloat("NPC_y",direction_y);
	}

	//Choose new direction to walk in
	public void RandomDirection()
	{
		direction_raw = Random.Range(0,4);
		isWalking = true;
		NPCAnim.SetBool("NPCMoving",true);
		walkCounter = walkTime;
	}
}