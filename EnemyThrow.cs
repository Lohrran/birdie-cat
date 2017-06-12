using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : EnemyMasterScript {

	public float upForce = 200f;
	private Rigidbody2D myRB;   

	#region Unity
	void Start ()
	{
		myRB = GetComponent<Rigidbody2D>();
		Movement ();
	}

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
	#endregion
}
