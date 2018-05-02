using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MagicSelection{fire,air,water,earth};

public class MagicMenu : MonoBehaviour 
{
	//Component References
	private Canvas CanvasObject;
	private RectTransform menu_rect;
	private static Player_StatManager PSM;
	private static Attack attack;

	//Magic Slots and Tool tip
	public GameObject magic_slot;
	private static GameObject tooltip;

	//Magic Menu Variables
	private bool MMCanvas_enabled;
	private static List<GameObject> buttons = new List<GameObject>();

	void Start()
	{
		CanvasObject = GetComponentInParent<Canvas>();
		menu_rect = GetComponentInParent<RectTransform>();
		PSM = FindObjectOfType<Player_StatManager>();
		attack = FindObjectOfType<Attack>();

		CanvasObject.enabled = false;
		MMCanvas_enabled = false;

		CreateMagicMenu();
	}

	void Update()
	{
		// switch(PSM.magicSelected)
		// {
		// 	case MagicSelection.fire:

		// 		break;
		// }
	}

	//Create Magic Menu
	//TO DO - ADD MORE ATTACK METHODS ... CURRENTLY ONLY SUPPORTS TYPE: AIR
	private void CreateMagicMenu()
	{
		menu_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width/2f);
		menu_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height/2f);
		
		for(int i = 0; i < 3; i++)
		{
			for(int j = 0; j < 5; j++)
			{
				GameObject tempObject = Instantiate(magic_slot);
				tempObject.name = "Slot: " + (i+1) + " - " + (j+1);
				tempObject.transform.SetParent(this.transform);
				tempObject.GetComponent<RectTransform>().localPosition = new Vector2(-200 + 100*j,100 - 100*i);
			
				tempObject.GetComponent<Image>().color = Color.gray;
				tempObject.GetComponent<MagicInfo>().menu_slot = j;

				switch(i)
				{
					case 0:
						switch(j)
						{
							//Unlock attack
							case 0:
								tempObject.GetComponent<MagicInfo>().cost = 1;
								tempObject.GetComponent<MagicInfo>().unlockable = true;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/air-burst-sky-2");
								break;
							//Upgrade Damage
							case 1:
								tempObject.GetComponent<MagicInfo>().cost = 2;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 1;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/enchant-sky-1");
								break;
							//Upgrade Speed
							case 2:
								tempObject.GetComponent<MagicInfo>().cost = 3;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.5f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/haste-sky-1");
								break;
							//Upgrade Rate of Fire
							case 3:
								tempObject.GetComponent<MagicInfo>().cost = 4;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.05f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/runes-blue-3");
								break;
							//Upgrade Projectiles
							case 4:
								tempObject.GetComponent<MagicInfo>().cost = 5;
								tempObject.GetComponent<MagicInfo>().unlockable = true;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/wind-blue-1");
								break;
						}
						break;
					case 1:
						switch(j)
						{
							//Unlock attack
							case 0:
								tempObject.GetComponent<MagicInfo>().cost = 2;
								tempObject.GetComponent<MagicInfo>().unlockable = true;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/air-burst-sky-2");
								break;
							//Upgrade Damage
							case 1:
								tempObject.GetComponent<MagicInfo>().cost = 3;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 1;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/enchant-sky-1");
								break;
							//Upgrade Speed
							case 2:
								tempObject.GetComponent<MagicInfo>().cost = 4;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.5f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/haste-sky-1");
								break;
							//Upgrade Rate of Fire
							case 3:
								tempObject.GetComponent<MagicInfo>().cost = 5;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.05f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/runes-blue-3");
								break;
							//Upgrade Range
							case 4:
								tempObject.GetComponent<MagicInfo>().cost = 5;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.2f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/wind-blue-1");
								break;
						}
						break;
					case 2:
						switch(j)
						{
							//Unlock attack
							case 0:
								tempObject.GetComponent<MagicInfo>().cost = 1;
								tempObject.GetComponent<MagicInfo>().unlockable = true;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/air-burst-sky-2");
								break;
							//Upgrade Damage
							case 1:
								tempObject.GetComponent<MagicInfo>().cost = 2;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 1;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/enchant-sky-1");
								break;
							//Upgrade Speed
							case 2:
								tempObject.GetComponent<MagicInfo>().cost = 3;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.5f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/haste-sky-1");
								break;
							//Upgrade Rate of Fire
							case 3:
								tempObject.GetComponent<MagicInfo>().cost = 4;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.05f;
								tempObject.GetComponent<MagicInfo>().range = 0.0f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/runes-blue-3");
								break;
							//Upgrade Range
							case 4:
								tempObject.GetComponent<MagicInfo>().cost = 5;
								tempObject.GetComponent<MagicInfo>().unlockable = false;
								tempObject.GetComponent<MagicInfo>().upgrade_multiplier = 0;
								tempObject.GetComponent<MagicInfo>().speed = 0.0f;
								tempObject.GetComponent<MagicInfo>().cooldown = 0.0f;
								tempObject.GetComponent<MagicInfo>().range = 0.2f;
								tempObject.GetComponent<MagicInfo>().magic_slot = i+1;
								tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Magic/Magic Images/Air/wind-blue-1");
								break;
						}
						break;
				}
				buttons.Add(tempObject);
			}
		}
	}

	//Toggle Menu on and off
	public void ToggleMagicMenu()
	{
		CanvasObject.enabled = !CanvasObject.enabled;
		MMCanvas_enabled = !MMCanvas_enabled;
	}

	public void PurchaseMagicTier(GameObject clickedMagic)
	{
		if(clickedMagic.GetComponent<MagicInfo>().cost <= PSM.magic_points)
		{
			if(clickedMagic.GetComponent<MagicInfo>().menu_slot == 0)
			{
				if(!clickedMagic.GetComponent<MagicInfo>().purchased)
				{
					PSM.magic_points -= clickedMagic.GetComponent<MagicInfo>().cost;
					PSM.UnlockSlot(clickedMagic.GetComponent<MagicInfo>().magic_slot);
				}
			}
			else if( clickedMagic.GetComponent<MagicInfo>().menu_slot == 4 && buttons[3].GetComponent<MagicInfo>().purchased)
			{
				PSM.magic_points -= clickedMagic.GetComponent<MagicInfo>().cost;
				attack.UpgradeFirst(PSM.magicSelected); 

				clickedMagic.GetComponent<MagicInfo>().cost += 1;
			}
			else if(buttons[clickedMagic.GetComponent<MagicInfo>().menu_slot - 1].GetComponent<MagicInfo>().purchased)
			{
				PSM.magic_points -= clickedMagic.GetComponent<MagicInfo>().cost;

				clickedMagic.GetComponent<MagicInfo>().cost += 1;
				attack.GetComponent<Attack>().multiplier_1 += clickedMagic.GetComponent<MagicInfo>().upgrade_multiplier;
				attack.GetComponent<Attack>().cooldownTime -= clickedMagic.GetComponent<MagicInfo>().cooldown;
				attack.GetComponent<Attack>().range += clickedMagic.GetComponent<MagicInfo>().range;
				attack.GetComponent<Attack>().projectile_speed += clickedMagic.GetComponent<MagicInfo>().speed;
			}
			else
			{
				Debug.Log("Must Purchase Previous Tier");
				return;
			}

			if(!clickedMagic.GetComponent<MagicInfo>().purchased)
			{
				clickedMagic.GetComponent<MagicInfo>().purchased = true;
				clickedMagic.GetComponent<Image>().color = Color.white;
				attack.GetComponent<Attack>().ChangeAttack(clickedMagic.GetComponent<MagicInfo>().magic_slot,MagicSelection.air);
			}

		}
	}
}
