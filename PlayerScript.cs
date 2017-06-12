using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	[HideInInspector]
	public bool playerIsCompleteDead;

	public float upForce = 200f;
	public CameraBoundariesExtension cameraB;
	public float throwForce;

	[HideInInspector]
	public bool isDead;
	private Rigidbody2D myRb;
	private CapsuleCollider2D playerCollider;


	#region Unity
	void Start () 
	{
		myRb = GetComponent <Rigidbody2D> ();
		playerCollider = GetComponent<CapsuleCollider2D> ();
		isDead = false;
		playerIsCompleteDead = false;
	}

	void Update () 
	{
		Movement ();
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
				if (transform.position.y <= cameraB.maxY - 0.1f) 
				{
					if (Input.GetMouseButtonDown (0)) 
					{
						myRb.velocity = Vector2.zero;
						myRb.AddForce (new Vector2 (0, upForce));
					} 
				}
			}
		}
	}
	#endregion

	#region Death
	void GetOutOfScene()
	{
		if(transform.position.y <= cameraB.minY - 1)
		{
			isDead = true;
			Destroy (this.gameObject);
			playerIsCompleteDead = true;
		}
	}

	public void Dead ()
	{
		//Debug.Log ("I Die");
		myRb.constraints = RigidbodyConstraints2D.None;
		myRb.AddForce (new Vector2 (300, throwForce));
		playerCollider.enabled = false;
		isDead = true;
	}
	#endregion
}
