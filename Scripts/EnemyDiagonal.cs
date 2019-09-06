using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiagonal : EnemyMasterScript
{
	public float speedUp = 0.08f;
	private float initialSpeedUp;

	#region Switch ON/OFF
	protected override void Connect ()
	{		
		initialSpeedUp = speedUp;
		base.Connect ();
	}

	protected override void Disconnect()
	{
		speedUp = initialSpeedUp;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		base.Disconnect ();
	}
	#endregion

	#region Movement
	protected override void Movement()
	{
		if (childSprite.isVisible) 
		{
			transform.position = new Vector2 (transform.position.x + (speed * Time.deltaTime), transform.position.y + (speedUp * Time.deltaTime));
		} 
		else 
		{
			transform.position = new Vector2 (transform.position.x + (speed * Time.deltaTime), transform.position.y);
		}
	}
	#endregion

	#region WhichSideStart
	protected override void CheckSide()
	{
		//base.CheckSide ();

		if (transform.position.x < 0) 
		{
			GetComponentInChildren<SpriteRenderer> ().flipX = true;
		} 
		else 
		{			
			speed *= -1;
		}

		if (transform.position.y > 0)
		{
			speedUp *= -1;
			if (transform.position.x < 0) 
			{
				transform.Rotate (0, 0, -90);
			} 
			else 
			{
				transform.Rotate(0, 0, 90);
			}
		}
	}
	#endregion
}
