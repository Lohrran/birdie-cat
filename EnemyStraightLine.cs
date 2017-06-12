using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraightLine : EnemyMasterScript 
{
	#region Movement
	protected override void Movement()
	{
		transform.position = new Vector2 (transform.position.x + (speed * Time.deltaTime), transform.position.y);
	}
	#endregion
}
