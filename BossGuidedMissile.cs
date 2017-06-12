using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuidedMissile : MonoBehaviour
{
	public float speed;
	public float rotatingSpeed;
	public GameObject target;

	private Rigidbody2D rb;

	private bool isVisible = false;
	private PlayerScript playerScript;
	private SpriteRenderer childSprite;
	private Animator anim;

	private int variable;

	void Awake ()
	{
		CheckSide ();

		childSprite = GetComponentInChildren <SpriteRenderer> ();
		anim = GetComponentInChildren<Animator> ();

		playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
	}
		
	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		WhereAmI ();
		IAmReadytoDestroy ();
		SelfDestroy ();
	}

	void FixedUpdate()
	{
		Movement();
	}

	protected virtual void CheckSide()
	{
		if (transform.position.x < 0) 
		{
			GetComponentInChildren<SpriteRenderer> ().flipX = true;
			variable = 1 * 1;
		}
		else 
		{
			variable = 1 * -1;
		}
	}

	void Movement()
	{
		if (target != null)
		{
			Vector2 pointToTarget = (Vector2)transform.position - (Vector2)target.transform.position;
			pointToTarget.Normalize ();
			float value = Vector3.Cross (pointToTarget, (transform.right * variable)).z;

			rb.angularVelocity =  rotatingSpeed * value;

			rb.velocity = (transform.right * variable) * speed;
		}
	}

	void WhereAmI ()
	{
		if (childSprite.isVisible) {
			isVisible = true;
		}
	}

	void IAmReadytoDestroy ()
	{
		if (isVisible == true && childSprite.isVisible == false)
		{
			Destroy (this.gameObject);
		}
	}

	void SelfDestroy ()
	{
		if (playerScript.playerIsCompleteDead == true) {
			Destroy (this.gameObject);
		}	
	}



	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			anim.SetTrigger ("Explode");

			Camera.main.GetComponent<CameraControl>().Shake(0.2f, 20, 4);

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("aBoss01_explosion")) 
			{
				Destroy (this.gameObject);
			}

			playerScript.Dead ();
		}

		if (other.gameObject.tag == "PShield") 
		{
			anim.SetTrigger ("Explode");

			Camera.main.GetComponent<CameraControl>().Shake(0.5f, 5, 2);

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("aBoss01_explosion")) 
			{
				Destroy (this.gameObject);
			}
		}
	}

}
