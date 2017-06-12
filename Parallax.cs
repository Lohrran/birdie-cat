using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public float speed = 0.02f;

	private BoxCollider2D cloudsCollider;
	private float cloudsHorizontalLength;
	public GameObject[] clouds;

	#region Unity
	void Awake () 
	{
		cloudsCollider = GetComponentInChildren<BoxCollider2D> ();
		cloudsHorizontalLength = cloudsCollider.size.x;
	}

	void Update()
	{
		if (Time.timeScale > 0) 
		{
			Movement ();
			RepositionBackground ();
		}
	}
	#endregion

	#region Parallax
	void Movement()
	{
		transform.position = new Vector2 (transform.position.x - speed, transform.position.y);
	}

	private void RepositionBackground()
	{
		Vector2 cloudsOffSet = new Vector2 (cloudsHorizontalLength * 2f, 0);

		for(int i = 0; i < clouds.Length; i++)
		{
			if (clouds [i].transform.position.x < -cloudsHorizontalLength) 
			{
				clouds[i].transform.position = (Vector2)clouds[i].transform.position + cloudsOffSet;
			}
		}
	}
	#endregion
}

