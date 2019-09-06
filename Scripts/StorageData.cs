using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class StorageData : MonoBehaviour
{

	private static StorageData instance;

	public static StorageData Instance 
	{
		get 
		{
			return instance;
		}
	}
		
	[HideInInspector]
	public string catSkin;
	[HideInInspector]
	public string birdieSkin;



	//[HideInInspector]
	public int highestScore = 0;
	//[HideInInspector]
	public int currency = 500;

	//[HideInInspector]
	public int catID = 1;
	//[HideInInspector]
	public int birdieID = 1;

	//[HideInInspector]
	public int availabilityC = 1;
	public int availabilityB = 1;

	void Awake()
	{	
		//PlayerPrefs.DeleteAll ();
		if (instance == null) 
		{
			instance = this;
		}
		else
		{
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (transform.gameObject);

		if (PlayerPrefs.HasKey ("CatID") || PlayerPrefs.HasKey ("BirdieID")) 
		{
			//We had a previous session
			catID = PlayerPrefs.GetInt ("CatID");
			birdieID = PlayerPrefs.GetInt ("BirdieID");
			highestScore = PlayerPrefs.GetInt ("High");
			currency = PlayerPrefs.GetInt ("Currency");
			availabilityC = PlayerPrefs.GetInt ("AvailabilityCat");
			availabilityB = PlayerPrefs.GetInt ("AvailabilityBirdie");

			//PlayerPrefs.Save ();
		}
		else
		{
			Save ();
		}
	}
		
	public void Save ()
	{
		PlayerPrefs.SetInt ("CatID", catID);
		PlayerPrefs.SetInt ("BirdieID", birdieID);
		PlayerPrefs.SetInt ("High", highestScore);
		PlayerPrefs.SetInt ("Currency", currency);
		PlayerPrefs.SetInt ("AvailabilityCat", availabilityC);
		PlayerPrefs.SetInt ("AvailabilityBirdie", availabilityB);

		PlayerPrefs.Flush ();
		//PlayerPrefs.Save ();
	}
		
	void Start()
	{
		CatStateSkin ();
		BirdieStateSkin ();
	}

	void Update()
	{
		Debug.Log ("Cat: " + catID);
		Debug.Log ("Birdie: " + birdieID);

		CatStateSkin ();
		BirdieStateSkin ();
	}

	void CatStateSkin()
	{
		switch (catID)
		{
			case 1:
				catSkin = "cat01";
				break;
			case 2:
				catSkin = "cat02";
				break;
			case 3:
				catSkin = "cat03";
				break;
			case 4:
				catSkin = "cat04";
				break;
			case 5:
				catSkin = "cat05";
				break;
			case 6:
				catSkin = "cat06";
				break;
			case 7:
				catSkin = "cat07";
				break;
			case 8:
				catSkin = "cat08";
				break;
			case 9:
				catSkin = "cat09";
				break;
			case 10:
				catSkin = "cat10";
				break;
		}
	}

	void BirdieStateSkin()
	{
		switch(birdieID)
		{
			case 1:
				birdieSkin = "birdie01";
				break;
			case 2:
				birdieSkin = "birdie02";
				break;
			case 3:
				birdieSkin = "birdie03";
				break;
			case 4:
				birdieSkin = "birdie04";
				break;
			case 5:
				birdieSkin = "birdie05";
				break;
			case 6:
				birdieSkin = "birdie06";
				break;
			case 7:
				birdieSkin = "birdie07";
				break;
			case 8:
				birdieSkin = "birdie08";
				break;
			case 9:
				birdieSkin = "birdie09";
				break;
			case 10:
				birdieSkin = "birdie10";
				break;
		}
	}
}

	