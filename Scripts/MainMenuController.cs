using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public Text highScoreText;

	void Start()
	{
		highScoreText.text = StorageData.Instance.highestScore.ToString();
	}

	public void PlayBtn()
	{
		FindObjectOfType<AudioManager> ().Play ("Button");
		SceneManager.LoadScene ("Game Scene");
	}

	public void ScoreBtn()
	{
		
	}

	public void ShopBtn()
	{		
		FindObjectOfType<AudioManager> ().Play ("Button");
		SceneManager.LoadScene ("Shop");
	}

}
