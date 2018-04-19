using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{
	//Player movement speed and reload time 
	public float speed = 1.3f; //TO DO --- ADD CHANGEABLE PLAYER MOVEMENT SPEED
	private float waitReload = 5.0f;

	//Player components 
	Rigidbody2D rbody;
	Animator anim;

	private Vector2 movement_vector;
	private float direction_x = 0f;
	private float direction_y = 0f;
	private KeyCode key;

	//Dialogue Manager Reference
	public DialogueManager DM;

	//Handles if a player can move
	public bool inDialogue;
	public bool pause = false;

	//TO DO
	// private bool firingUp;
	// private bool firingDown;
	// private bool firingLeft;
	// private bool firingRight;

	void Start () 
	{
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		DM = FindObjectOfType<DialogueManager>();
		inDialogue = false;
	}
	
	//Updates movement of player
	void FixedUpdate () 
	{
		//If not in dialogue
		if(!DM.dialogueOpen)
		{
			inDialogue = false;
		}

		//If in dialogue, halt player
		if(inDialogue)
		{
			rbody.velocity = Vector2.zero;
			anim.SetBool("IsWalking",false);
			return;
		}

		//If player can move, then move
		if(!anim.GetBool("IsDead") && !pause)
		{
			movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

			// if(Input.GetKeyDown(KeyCode.UpArrow))
			// {
			// 	direction_x = 0;
			// 	direction_y = 1;
			// 	firingUp = true;
			// }
			// if(Input.GetKeyDown(KeyCode.DownArrow))
			// {
			// 	direction_x = 0;
			// 	direction_y = -1;
			// 	firingDown = true;
			// }
			// if(Input.GetKeyDown(KeyCode.LeftArrow))
			// {
			// 	direction_x = -1;
			// 	direction_y = 0;
			// 	firingLeft = true;
			// }
			// if(Input.GetKeyDown(KeyCode.RightArrow))
			// {
			// 	direction_x = 1;
			// 	direction_y = 0;
			// 	firingRight = true;
			// }
			// if(Input.GetKeyUp(KeyCode.UpArrow))
			// {
			// 	firingUp = false;
			// }
			// if(Input.GetKeyUp(KeyCode.DownArrow))
			// {
			// 	firingDown = false;
			// }
			// if(Input.GetKeyUp(KeyCode.LeftArrow))
			// {
			// 	firingLeft = false;
			// }
			// if(Input.GetKeyUp(KeyCode.RightArrow))
			// {
			// 	firingRight = false;
			// }

			// if(!firingUp && !firingDown && !firingLeft && !firingRight) 
			// {
			// 	direction_x = 0;
			// 	direction_y = 0;
			// }
		
			

			//If not standing still, update direction and animation
			if (movement_vector != Vector2.zero)
			{
				anim.SetBool("IsWalking",true);

				//TO DO
				// if(firingUp || firingDown || firingLeft || firingRight) 
				// {
				// 	anim.SetFloat("Input_x", direction_x);
				// 	anim.SetFloat("Input_y", direction_y);
				// }
				// else
				// {
				// 	anim.SetFloat("Input_x", movement_vector.x);
				// 	anim.SetFloat("Input_y", movement_vector.y);
				// }
				
				anim.SetFloat("Input_x", movement_vector.x);
				anim.SetFloat("Input_y", movement_vector.y);

			}
			else 
			{ 
				anim.SetBool("IsWalking",false);
			}

			//Moves player
			rbody.MovePosition(rbody.position + movement_vector * (Time.deltaTime*speed));
		}
		//If player dies, play animation and wait for reload time to hit 0
		else if(anim.GetBool("IsDead"))
		{
			waitReload -= Time.deltaTime;
			rbody.velocity = Vector2.zero;
			if(waitReload < 0)
			{
				//TO DO --- CHANGE TO RELOAD FROM CHECKPOINT OR LAST SAVE
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				anim.SetBool("IsDead",false);
			}
		}

	}
}

