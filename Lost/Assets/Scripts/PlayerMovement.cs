using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 1.3f; //Change Later
	private float waitReload = 5.0f;
	Rigidbody2D rbody;
	Animator anim;
	public DialogueManager DM;

	public bool inDialogue;

	void Start () 
	{
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		DM = FindObjectOfType<DialogueManager>();
		inDialogue = false;
	}
	

	void FixedUpdate () 
	{
		if(!DM.dialogueOpen)
		{
			inDialogue = false;
		}

		if(inDialogue)
		{
			rbody.velocity = Vector2.zero;
			anim.SetBool("IsWalking",false);
			return;
		}

		if(!anim.GetBool("IsDead"))
		{
			Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

			if (movement_vector != Vector2.zero)
			{
				anim.SetBool("IsWalking",true);
				anim.SetFloat("Input_x", movement_vector.x);
				anim.SetFloat("Input_y",movement_vector.y);
			}
			else 
			{ 
				anim.SetBool("IsWalking",false);
			}

			rbody.MovePosition(rbody.position + movement_vector * (Time.deltaTime*speed));
		}
		else
		{
			waitReload -= Time.deltaTime;
			rbody.velocity = Vector2.zero;
			if(waitReload < 0)
			{
				Application.LoadLevel(Application.loadedLevel);
				anim.SetBool("IsDead",false);
			}
		}

	}

}

