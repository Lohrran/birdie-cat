using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMasterScript : MonoBehaviour
{
	public float speed = 0.02f;
	public float weight;

	private bool isVisible = false;
	protected SpriteRenderer childSprite;
	private PlayerScript playerScript;

	protected Animator anim;

	#region Unity
	void Awake ()
	{
		CheckSide ();

		childSprite = GetComponentInChildren <SpriteRenderer> ();
		anim = GetComponentInChildren<Animator> ();

		if (GameObject.FindGameObjectWithTag ("Player").gameObject != null) 
		{
			playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
		}
	}

	void Update ()
	{
		Movement ();

		WhereAmI ();
		IAmReadytoDestroy ();
		SelfDestroy ();
	}
	#endregion

	#region WhichSideStart
	protected virtual void CheckSide()
	{
		if (transform.position.x < 0) 
		{
			GetComponentInChildren<SpriteRenderer> ().flipX = true;
		} 
		else 
		{
			speed *= -1;
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
			Destroy (this.gameObject);
		}
	}

	protected void SelfDestroy ()
	{
		if (playerScript.playerIsCompleteDead == true) 
		{
			Destroy (this.gameObject);
		}	
	}

	IEnumerator DeathByPlayer()
	{
		GetComponent<Collider2D> ().enabled = false;
		anim.SetTrigger ("Explode");
		speed = 0.0f;
		//Camera.main.GetComponent<CameraControl>().Shake(0.1f, 3, 1);
		Camera.main.GetComponent<CameraControl>().Shake(0.3f, 5, 2.5f);


		yield return new WaitForSeconds (0.0006f);

		StopTime (false);

		Destroy (this.gameObject);

	}

	IEnumerator DeathByShield()
	{
		GetComponent<Collider2D> ().enabled = false;
		anim.SetTrigger ("Explode");
		speed = 0.0f;
		Camera.main.GetComponent<CameraControl>().Shake(0.1f, 3, 1);

		yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo(0).length);

		Destroy (this.gameObject);

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

	#endregion
}
