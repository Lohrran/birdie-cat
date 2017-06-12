using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour {

	public GameObject masterSpawn;
	public GameObject starSpawn;
	public GameObject powerUpSpawn;
	public GameObject canvasInGame;
	public GameObject canvasGameOver;

	//private bool gameStarted;

	private PlayerScript playerScript;
	private static List<float> lastScores = new List<float>(); //Equal to zero just when the player exit game

	private float timer;
	private float timerSet;

	#region GlobalVariables
	public static class GlobalVariables
	{
		//Fuzzy Variables
		public static int points;
		public static float gameTime;
		public static int playerDeaths; //Equal to zero just when the player exit game
		public static float lastScoresAverage; //Equal to zero just when the player exit game
		public static float matchTime;

		public static bool starTimeMatch = false;

		public static List<float> lastTimeStar = new List<float> ();
		public static float lastTimeStarAverage;
	}
	#endregion
		
	#region Unity
	void Awake ()
	{
		masterSpawn.SetActive (false);
		starSpawn.SetActive (false);
		powerUpSpawn.SetActive (false);
		canvasInGame.SetActive (false);
	}

	void Start()
	{
		timerSet = 30;
		timer = timerSet;
		GlobalVariables.points = 0;
		playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
	}

	void Update()
	{
		GameTimerMacthTime ();
		CallGameOver ();
		TimeStarAverage ();
		TimerStar ();
	}
	#endregion

	#region StartTimer
	void GameTimerMacthTime()
	{
		GlobalVariables.gameTime += Time.deltaTime;

		if (GlobalVariables.starTimeMatch == true)
		{
			GlobalVariables.matchTime += Time.deltaTime;
		}
	}
	#endregion

	#region GameOver
	void CallGameOver()
	{
		if(playerScript.playerIsCompleteDead == true)
		{
			GlobalVariables.matchTime = 0;
			GameController.GlobalVariables.starTimeMatch = false;

			masterSpawn.SetActive (false);
			starSpawn.SetActive (false);
			powerUpSpawn.SetActive (false);

			canvasInGame.SetActive(false);
			canvasGameOver.SetActive (true);
			GlobalVariables.lastTimeStar.Clear ();
			GlobalVariables.lastTimeStarAverage = 0;
		}
	}

	public void CalculateMeanValue()//Call in the Game Over RestartBtn Event
	{
		if (playerScript.playerIsCompleteDead == true)
		{
			lastScores.Add (GlobalVariables.points);
			GlobalVariables.lastScoresAverage = lastScores.Average();
			//Debug.Log ("Average: " + GlobalVariables.lastScoresAverage);
		}
	}

	void TimeStarAverage()
	{
		if (GlobalVariables.lastTimeStar.Count != 0)
		{
			GlobalVariables.lastTimeStarAverage = GlobalVariables.lastTimeStar.Average ();
			GlobalVariables.lastTimeStarAverage = GlobalVariables.lastTimeStarAverage * -1;
			//Debug.Log ("Star Time: " + GlobalVariables.lastTimeStarAverage);
		}

	}

	void TimerStar()
	{
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			GlobalVariables.lastTimeStar.Clear ();
			timer = timerSet;;
		}
	}
	#endregion
}
