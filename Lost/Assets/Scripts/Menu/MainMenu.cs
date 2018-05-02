using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

	public GameObject informationPage;
	public Button backButton;

	public void PlayGame()
	{
		SceneManager.LoadScene("Lost");
	}

	public void ExitGame()
	{
		Application.Quit();
	}	

	public void AboutPage()
	{	
		informationPage.SetActive(true);
	}	

	public void GoBack()
	{
		informationPage.SetActive(false);
	}
}
