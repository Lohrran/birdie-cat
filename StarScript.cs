using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour 
{
	public float speedToScore;

	private RectTransform target;
	private Animator targetAnim;
	private bool canMove;
	private Animator anim;

//	private Trail trail;

	private GameObject player;
	public float speedToPlayer;

	private float timer;
	private bool iAmAlive;
	private ParticleSystem starExplosion;

	#region Unity

	void OnEnable()
	{
		timer = 0;
		canMove = false;
		iAmAlive = true;

		target = GameObject.FindGameObjectWithTag ("Score").GetComponent<RectTransform> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		anim = GetComponentInChildren<Animator> ();
		targetAnim = target.GetComponent<Animator> ();
//		trail = GetComponentInChildren <Trail> ();
		//FindObjectOfType<AudioManager> ().Play ("Star Spawn");
	
		starExplosion = GetComponentInChildren<ParticleSystem> ();
		//trail.SetEnabled (false);
	}

	void OnDisable()
	{
		//trail.SetEnabled (false);
	}
		
	void Update ()
	{		
		Hitted ();
		
		if (iAmAlive == true)
		{
			timer -= Time.deltaTime;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("aStar_start")) 
		{
			anim.SetTrigger ("Idle");
		}

		MoveToScore ();

		if (target != null) 
		{
			if (Vector2.Distance (transform.position, target.position) < 0.1f) 
			{
				canMove = false;

				//FindObjectOfType<AudioManager> ().Play ("Star Hit Score");

				targetAnim.SetTrigger ("GetPoint");

				Dead ();
			}
		}
			
		MoveToPlayer ();
	}
	#endregion
		
	#region PowerUpMagnet
	void MoveToPlayer ()
	{
		if (canMove == false) 
		{
			if (player != null) 
			{
				if (player.GetComponent<PlayerPowerUps> ().magnet.activeSelf == true) 
				{
					transform.Translate ((player.transform.position - transform.position).normalized * speedToPlayer * Time.deltaTime, Space.World);
					//trail.SetEnabled (true);
				} 
				else
				{
					//trail.SetEnabled (false);
				}
			}
		}
	}
	#endregion

	#region Death
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "PShield") 
		{
			StartCoroutine (Hitted());
		}
	}

	IEnumerator Hitted()
	{
		starExplosion.Play ();

		anim.SetTrigger ("Hitted");
		iAmAlive = false;
		GameController.GlobalVariables.lastTimeStar.Add (timer);
		timer = 0;

		yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo(0).length - 0.32f);

		FindObjectOfType<AudioManager> ().Play ("Star Hitted");

		canMove = true;
		gameObject.tag = "DeadStar";

	}

	void MoveToScore()
	{
		if (canMove == true)
		{
			transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (target.position.x, target.position.y), speedToScore * Time.deltaTime);
			//trail.SetEnabled (true);
		}
	}

	void Dead ()
	{
		GameController.GlobalVariables.points += 1;
		targetAnim.SetTrigger ("Idle");

		//Object Pooling
		gameObject.SetActive (false);
		gameObject.tag = "Star";
	}
	#endregion
}
