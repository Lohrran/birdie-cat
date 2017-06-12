using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

	public GameObject pauseMenu;

	#region Unity
	void Start()
	{
		pauseMenu.SetActive (false);
	}	
	#endregion

	#region Buttons
	public void PauseBtn()
	{
		Paused (true);
		Time.timeScale = 0;
		pauseMenu.SetActive (true);
	}

	public void ContinueBtn()
	{
		Paused (false);
		pauseMenu.SetActive (false);
	}

	public void RestartBtn()
	{
		Paused (false);
		SceneManager.LoadScene ("Game Scene");
	}

	public void HomeBtn()
	{
		//SceneManager.LoadScene ("Main Menu");
	}

	public bool Paused(bool paused)
	{
		if (paused == true)
		{
			Time.timeScale = 0;
		}
		else if(paused == false)
		{
			Time.timeScale = 1;
		}

		return paused;
	}
	#endregion
}
