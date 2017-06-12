using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMaster : MonoBehaviour
{
	public float speed = 0.02f;
	private SpriteRenderer sprite;
	private bool isVisible = false;
	public int id;

	#region Unity
	void Start()
	{
		sprite = GetComponentInChildren <SpriteRenderer> ();
		CheckSide ();
	}

	void Update()
	{
		if(Time.timeScale > 0)
		{
			Movement ();
		}

		WhereAmI ();
		IAmReadytoDestroy ();
	}
	#endregion	

	#region Movement
	void CheckSide()
	{
		if (transform.position.x > 0) 
		{
			speed *= -1;
		} 
	}

	void Movement () 
	{
		transform.position = new Vector2 (transform.position.x + speed, transform.position.y);
	}
	#endregion

	#region Death
	protected void WhereAmI ()
	{
		if (sprite.isVisible) 
		{
			isVisible = true;
		}
	}

	void IAmReadytoDestroy()
	{
		if(isVisible == true && sprite.isVisible == false)
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			other.SendMessage ("MailBox", id);
			Destroy (this.gameObject);		
		}
	}

	#endregion
}
