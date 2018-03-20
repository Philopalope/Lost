using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 1.3f; //Change Later
	private float waitReload = 5.0f;
	Rigidbody2D rbody;
	Animator anim;
	public DialogueManager DM;

	public bool inDialogue;
	public bool pause = false;

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

		if(!anim.GetBool("IsDead") && !pause)
		{
			Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

			if (movement_vector != Vector2.zero)
			{
				anim.SetBool("IsWalking",true);
				anim.SetFloat("Input_x", movement_vector.x);
				anim.SetFloat("Input_y",movement_vector.y);
				//Debug.Log(movement_vector + "");
			}
			else 
			{ 
				anim.SetBool("IsWalking",false);
			}

			rbody.MovePosition(rbody.position + movement_vector * (Time.deltaTime*speed));
		}
		else if(anim.GetBool("IsDead"))
		{
			waitReload -= Time.deltaTime;
			rbody.velocity = Vector2.zero;
			if(waitReload < 0)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				anim.SetBool("IsDead",false);
			}
		}

	}
}

