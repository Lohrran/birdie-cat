using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	[HideInInspector]
	public bool playerIsCompleteDead;

	public float upForce = 192f;
	public CameraBoundariesExtension cameraB;
	public float throwForce;

	[HideInInspector]
	public bool isDead;
	private Rigidbody2D myRb;
	private CapsuleCollider2D playerCollider;
	private AudioManager audioManager;

	private bool clicked;

	#region Unity
	void Start () 
	{
		audioManager = 	FindObjectOfType<AudioManager> ();

		myRb = GetComponent <Rigidbody2D> ();
		playerCollider = GetComponent<CapsuleCollider2D> ();
		isDead = false;
		playerIsCompleteDead = false;
		clicked = false;
	}

	void FixedUpdate()
	{
		Movement ();
	}

	void Update () 
	{
		CheckInput ();
		GetOutOfScene();
	}
	#endregion

	#region Movement
	void Movement()
	{
		if (isDead == false) 
		{
			if (Time.timeScale > 0) 
			{
				if (GetComponent<PortalPowerUp>().enabled == false)
				{
					if (transform.position.y <= cameraB.maxY - 0.1f)
					{
						if (clicked == true) 
						{
							myRb.velocity = Vector2.zero;
							myRb.AddForce (new Vector2 (0, upForce));
							clicked = false;
						} 
					} 

					else 
					{
						//FindObjectOfType<AudioManager> ().Play ("Cat Hit Top");
					}
				}

				else
				{
					if (clicked == true) 
					{
						myRb.velocity = Vector2.zero;
						myRb.AddForce (new Vector2 (0, upForce));
						clicked = false;
					} 
				}

			}
		}
	}

	void CheckInput()
	{
		if (Input.GetMouseButtonDown (0))
		{
			clicked = true;
			audioManager.Play ("Star Spawn");
		}
	}
	#endregion

	#region Death
	void GetOutOfScene()
	{
		if (GetComponent<PortalPowerUp> ().enabled == false)
		{
			if (transform.position.y <= cameraB.minY - 1)
			{
				isDead = true;
				Destroy (this.gameObject);
				playerIsCompleteDead = true;
			}
		}
	}

	public void Dead ()
	{
		if (GetComponent<PortalPowerUp> ().enabled == true)
		{
			GetComponent<PlayerPowerUps> ().enabled = false;
			GetComponent<PortalPowerUp> ().enabled = false;
		}

		//myRb.constraints = RigidbodyConstraints2D.None;
		//myRb.AddForce (new Vector2 (300, throwForce));
		playerCollider.enabled = false;
		isDead = true;
	}
	#endregion
}
