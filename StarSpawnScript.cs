using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawnScript : MonoBehaviour {

	public float min;
	public float max;

	public GameObject starPrefab;
	public Transform playerTransform;
	public CameraBoundariesExtension cameraB;

	private GameObject[] manyStars;

	#region Unity
	void Update ()
	{
		manyStars = GameObject.FindGameObjectsWithTag("Star");

		if (manyStars.Length == 0)
		{
			Spawn ();
		}
	}
	#endregion

	#region Spawn
	void Spawn()
	{
		// Y position between top & bottom border
		int starPositionY = (int)Random.Range(cameraB.minY,cameraB.maxY);

		// Check the distance between the two points
		float dist = Mathf.Abs (starPositionY - playerTransform.position.y);

		// Instantiate the Star at (x, y) with Object Pooling
		GameObject star = ObjectPooler.SharedInstance.GetPooledGameObject("Star");
		if (star != null)
		{
			if (dist >= min && dist <= max) 
			{
				star.transform.position = new Vector2 (0, starPositionY);
				star.SetActive (true);
			}
		}
	}
	#endregion		
}