using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : EnemyMasterScript {

	public float upForce = 200f;
	private Rigidbody2D myRB;

	#region Switch ON
	protected override void Connect()
	{
		speed = 210;
		base.Connect ();
		myRB = GetComponent<Rigidbody2D>();

		Movement ();
	}
	#endregion

	#region Unity
	void Update()
	{
		base.WhereAmI ();
		base.IAmReadytoDestroy ();
		base.SelfDestroy ();
	}
	#endregion

	#region Movement
	protected override void Movement ()
	{
		myRB.AddForce (new Vector2 (speed, upForce));
	}

	protected override void CheckSide ()
	{
		if (transform.position.x < 0) 
		{
			GetComponentInChildren<SpriteRenderer> ().flipX = true;
			if (particle != null)
			{
				particle.localPosition = new Vector3(-0.2f, 0.4f, 0);
				particle.rotation = Quaternion.Euler (180, 90, -90);
			}
		} 
		else 
		{			
			speed *= -1;
			if (particle != null)
			{	
				particle.localPosition = new Vector3 (0.2f, 0.4f, 0);
				particle.rotation = Quaternion.Euler (180, -90, -90);
			}
		}
	}
	#endregion
}
