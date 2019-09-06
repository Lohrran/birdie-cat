using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	private Text scoreText;

	void Start ()
	{
		scoreText = GetComponentInChildren <Text> ();
	}

	void Update () 
	{
		scoreText.text = "" + GameController.GlobalVariables.points;
	}
}
