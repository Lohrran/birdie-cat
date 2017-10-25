using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

	public GameObject canvasGameOver;
	private GameController gameController;

	public Image score;
	public Image highScore;


	#region Unity
	void Start () 
	{
		canvasGameOver.SetActive (false);
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		highScore.enabled = false;
		score.enabled = true;
	}

	void Update()
	{
		if (GameController.GlobalVariables.points > StorageData.Instance.highestScore)
		{
			highScore.enabled = true;
			score.enabled = false;

			StorageData.Instance.highestScore = GameController.GlobalVariables.points;
			StorageData.Instance.Save ();
		}

	}
	#endregion

	#region Buttons
	public void RestartBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		GameController.GlobalVariables.playerDeaths += 1;
		SceneManager.LoadScene ("Game Scene");
		gameController.CalculateMeanValue ();
	}

	public void HomeBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		SceneManager.LoadScene ("Main Menu");
	}

	public void ShopBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		SceneManager.LoadScene ("Shop");
	}

	public void ScoreBtn()
	{
		
	}

	public void ShareBtn()
	{
		
	}
	#endregion
}
