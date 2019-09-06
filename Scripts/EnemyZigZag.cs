using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZigZag : EnemyMasterScript {

	public float crazyMoveSpeed = 0.02f;
	public float maxAmount = 1f; 

	private float Ypos;

	#region Unity
	void Start ()
	{
		Ypos = transform.position.y;
	}
	#endregion

	#region Movement
	protected override void Movement ()
	{
		transform.position = new Vector2 (transform.position.x + speed, transform.position.y + crazyMoveSpeed);
	
		if (transform.position.y >= Ypos + maxAmount) 
		{ 
			crazyMoveSpeed = crazyMoveSpeed * -1;
		}

		else if (transform.position.y <= Ypos)
		{
			crazyMoveSpeed = crazyMoveSpeed * -1;
		}
	}
	#endregion
}