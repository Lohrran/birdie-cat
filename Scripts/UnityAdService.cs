using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

[System.Serializable]
public class SpawnableItem
{
	public string type;
	public float weight;
}
	
public class UnityAdService : MonoBehaviour 
{
	private static UnityAdService instance;

	public static UnityAdService Instance
	{
		get 
		{
			return instance;
		}
	}

	public SpawnableItem[] spawnList; 

	private float totalSpawnWeight;
	private int chosenIndex;

	private int skin;
	private float timerLeft = 2.0f;
	private bool finished = false;

	[HideInInspector] public string adID;
	public GameObject adBanner;
	public GameObject curtainBanner;
	public GameObject button;

	void OnValidate()
	{
		totalSpawnWeight = 0f;

		foreach(var spawnable in spawnList)
		{
			totalSpawnWeight += spawnable.weight;
		}
	}

	void Awake()
	{
		instance = this;

		OnValidate ();

		adBanner.SetActive (false);
		curtainBanner.SetActive (false);
	}

	void Update()
	{
		if (finished == true)
		{
			timerLeft -= Time.deltaTime;
		}

		if (timerLeft <= 0)
		{
			adBanner.SetActive (false);
			curtainBanner.SetActive (false);
		}
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResult });
		}
	}

	private void HandleAdResult (ShowResult result)
	{
		DecideThePrize ();

		switch (result) 
		{
		case ShowResult.Finished:
			Prize ();
			adBanner.SetActive (true);
			curtainBanner.SetActive (true);
			button.SetActive (false);
			finished = true;
			Debug.Log ("Player Gains Coins");
			break;
		case ShowResult.Skipped:
			Debug.Log ("Player did not fully watch the Ad");
			break;
		case ShowResult.Failed:
			Debug.Log ("Player failed to launch the ad? Internet?");
			break;
		}
	}

	void DecideThePrize()
	{
		float pick = Random.value * totalSpawnWeight;
		chosenIndex = 0;
		float cumulativeWeight = spawnList [0].weight;

		while (pick > cumulativeWeight && chosenIndex < spawnList.Length - 1)
		{
			chosenIndex++;
			cumulativeWeight += spawnList [chosenIndex].weight;
		}
	}

	private void Prize()
	{
		switch (spawnList [chosenIndex].type) 
		{
		case "Birdie":
			skin = Random.Range (1, 10);
			adID = "birdie " + skin;
			StorageData.Instance.availabilityB += 1 << skin;
			break;
		case "Cat":
		 	skin = Random.Range (1, 10);
			adID = "cat " + skin;
			StorageData.Instance.availabilityC += 1 << skin;
			break;
		case "10Stars":
			adID = "10stars";
			StorageData.Instance.currency += 10;
			break;
		case "50Stars":
			adID = "50stars";
			StorageData.Instance.currency += 50;
			break;
		case "100Stars":
			adID = "100stars";
			StorageData.Instance.currency += 100;
			break;
		}
	}
}
