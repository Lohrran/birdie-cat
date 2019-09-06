using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControlShop : MonoBehaviour
{
	public Text currencyText;

	void Start()
	{
		currencyText.text = StorageData.Instance.currency.ToString();
	}

	void Update()
	{
		currencyText.text = StorageData.Instance.currency.ToString();
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene ("Main Menu");
		FindObjectOfType<AudioManager> ().Play ("Button");
	}
}
