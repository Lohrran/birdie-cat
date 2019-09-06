using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour {

	public GameObject pauseMenu;
	//public AudioMixer masterVolume;

	private bool mClicked;
	private bool sClicked;

	#region Unity
	void Start()
	{
		pauseMenu.SetActive (false);
		mClicked = false;
		sClicked = false;
	}	
	#endregion

	#region Buttons
	public void PauseBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		Paused (true);
		SetVolume (-20);
		pauseMenu.SetActive (true);
	}

	public void ContinueBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		Paused (false);
		SetVolume (0);
		pauseMenu.SetActive (false);
	}

	public void RestartBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		Paused (false);
		SetVolume (0);
		SceneManager.LoadScene ("Game Scene");
	}

	public void HomeBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		Paused (false);
		SetVolume (0);
		SceneManager.LoadScene ("Main Menu");
	}

	public void MusicBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		mClicked = !mClicked;

		if (mClicked)
		{
			FindObjectOfType<AudioManager> ().Stop ("Music");
		}

		else
		{
			FindObjectOfType<AudioManager> ().Play ("Music");
			//masterVolume.SetFloat ("musicVolume", 0);
		}
	}

	public void SoundFxBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		sClicked = !sClicked;

		if (sClicked)
		{
			FindObjectOfType<AudioManager> ().StopAll();
			//masterVolume.SetFloat ("soundFxVolume", -80);
		}

		else
		{
			FindObjectOfType<AudioManager> ().PlayAll();
			//masterVolume.SetFloat ("soundFxVolume", 0);
		}
	}

	void SetVolume(float volume)
	{
		//masterVolume.SetFloat ("masterVolume", volume);
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
