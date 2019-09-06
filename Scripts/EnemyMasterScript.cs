using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMasterScript : MonoBehaviour
{
	public float speed = 0.02f;
	public float weight;
	public string bread;

	private bool isVisible = false;
	protected SpriteRenderer childSprite;
	private PlayerScript playerScript;

	protected Animator anim;

	private float initialSpeed;
	protected bool alive;
	private GameObject[] temp;

	protected Transform particle;

	#region Swtich ON/OFF
	void OnEnable ()
	{
		Connect ();
	}

	protected virtual void Connect()
	{	
		GetParticle ();	
		initialSpeed = speed;
		CheckSide ();

		childSprite = GetComponentInChildren <SpriteRenderer> ();
		anim = GetComponentInChildren<Animator> ();

		if (GameObject.FindGameObjectWithTag ("Player").gameObject != null) 
		{
			playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
		}

		alive = true;
	}
		
	void OnDisable ()
	{
		Disconnect ();
	}

	protected virtual void Disconnect()
	{
		speed = initialSpeed;
		GetComponentInChildren<SpriteRenderer> ().flipX = false;
		GetComponent<Collider2D> ().enabled = true;
		alive = false;
		particle.gameObject.SetActive (true);
	}
	#endregion

	#region Unity
	void Update ()
	{
		Movement ();
		WhereAmI ();
		IAmReadytoDestroy ();
		SelfDestroy ();
	}
	#endregion

	void GetParticle()
	{
		foreach(Transform child in transform)
		{
			if (child.name == "Particle Smoke")
			{
				particle = child;
			}
		}
	
	}
		
	#region Start
	protected virtual void CheckSide()
	{
		if (transform.position.x < 0) 
		{
			GetComponentInChildren<SpriteRenderer> ().flipX = true;
			if (particle != null)
			{
				particle.localPosition = new Vector3(-0.5f, 0, 0);
				particle.rotation = Quaternion.Euler (180, 90, -90);
			}
		} 
		else 
		{			
			speed *= -1;
			if (particle != null)
			{	
				particle.localPosition = new Vector3 (0.5f, 0, 0);
				particle.rotation = Quaternion.Euler (180, -90, -90);
			}
		}
	}
	#endregion


	#region Movement
	protected virtual void Movement () 
	{
		
	}
	#endregion

	#region Death
	protected void WhereAmI ()
	{
		if (childSprite.isVisible) 
		{
			isVisible = true;
		}
	}

	protected void IAmReadytoDestroy ()
	{
		if (isVisible == true && childSprite.isVisible == false) 
		{
			gameObject.SetActive (false);
			isVisible = false;
		}
	}

	protected void SelfDestroy ()
	{
		if (playerScript.playerIsCompleteDead == true) 
		{
			gameObject.SetActive (false);
		}	
	}

	IEnumerator DeathByPlayer()
	{
		FindObjectOfType<AudioManager> ().Play ("Enemy Explosion");
		FindObjectOfType<AudioManager> ().Play ("Cat Die");
	
		particle.gameObject.SetActive (false);
		GetComponent<Collider2D> ().enabled = false;
		anim.SetTrigger ("Explode");
		speed = 0.0f;
		Camera.main.GetComponent<CameraControl>().Shake(0.3f, 5, 2.5f);

		yield return new WaitForSeconds (0.0006f);

		StopTime (false);
		gameObject.SetActive (false);
	}

	IEnumerator DeathByShield()
	{
		FindObjectOfType<AudioManager> ().Play ("Enemy Explosion");

		particle.gameObject.SetActive (false);
		GetComponent<Collider2D> ().enabled = false;
		anim.SetTrigger ("Explode");
		speed = 0.0f;
		Camera.main.GetComponent<CameraControl>().Shake(0.1f, 3, 1);

		yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo(0).length);

		gameObject.SetActive (false);
	}
		
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			StopTime (true);

			playerScript.Dead ();

			StartCoroutine (DeathByPlayer());
		}

		else if (other.gameObject.tag == "PShield") 
		{					
			StartCoroutine (DeathByShield());
		}
	}
		
	void StopTime(bool stop)
	{
		if (stop == true)
		{
			Time.timeScale = 0.001f;
			anim.speed = 1000;
			playerScript.enabled = false;
		}

		else if (stop == false)
		{
			Time.timeScale = 1;
			anim.speed = 1;
			playerScript.enabled = true;
		}
	}

	public bool GetCurrentState()
	{
		return alive;
	}

	#endregion
}
