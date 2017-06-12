using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeStartManager : MonoBehaviour {

	public GameObject canvasBeforeStart;

	public GameObject player;
	public GameObject masterSpawn;
	public GameObject powerUpSpawn;
	public GameObject starSpawn;
	public GameObject canvasInGame;

	#region Unity
	void Awake ()
	{
		player.GetComponent<Rigidbody2D> ().gravityScale = 0;
		player.GetComponent<PlayerScript> ().enabled = false;
		masterSpawn.SetActive (false);
		starSpawn.SetActive (false);
		powerUpSpawn.SetActive (false);
		canvasInGame.SetActive (false);
	}
	#endregion

	#region
	public void StartBtn()
	{
		GameController.GlobalVariables.starTimeMatch = true;
		
		player.GetComponent<Rigidbody2D> ().gravityScale = 1;
		player.GetComponent<PlayerScript> ().enabled = true;
		masterSpawn.SetActive (true);
		starSpawn.SetActive (true);
		powerUpSpawn.SetActive (true);
		canvasInGame.SetActive (true);
		canvasBeforeStart.SetActive (false);
	}
	#endregion
}
