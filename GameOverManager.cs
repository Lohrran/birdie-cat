using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public GameObject canvasGameOver;
	private GameController gameController;

	#region Unity
	void Start () 
	{
		canvasGameOver.SetActive (false);
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}
	#endregion

	#region Buttons
	public void RestartBtn()
	{
		GameController.GlobalVariables.playerDeaths += 1;
		SceneManager.LoadScene ("Game Scene");
		gameController.CalculateMeanValue ();
	}

	public void HomeBtn()
	{
		SceneManager.LoadScene ("Main Menu");
	}

	public void ShopBtn()
	{
		
	}

	public void ScoreBtn()
	{
		
	}

	public void ShareBtn()
	{
		
	}
	#endregion
}
