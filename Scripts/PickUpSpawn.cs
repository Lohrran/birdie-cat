using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawn : MonoBehaviour {

	[System.Serializable]
	public class Spawnable
	{
		public GameObject pickUp;
		public float weight;
	}

	public Spawnable[] spawnList; 
	public CameraBoundariesExtension cameraB;
	public GameObject[] spawnPoints;

	private float totalSpawnWeight;
	private int chosenIndex;

	#region Unity
	void OnValidate()
	{
		totalSpawnWeight = 0f;
		foreach(var spawnable in spawnList)
		{
			totalSpawnWeight += spawnable.weight;
		}
	}

	void Awake ()
	{
		OnValidate();
	}

	void Start()
	{
		InvokeRepeating ("Spawn", 30f, 60f);
	}
	#endregion

	#region Spawn
	void Spawn()
	{
		int pickUpPositionY = (int)Random.Range(cameraB.minY,cameraB.maxY);
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		float pick = Random.value * totalSpawnWeight;
		chosenIndex = 0;
		float cumulativeWeight = spawnList [0].weight;

		while (pick > cumulativeWeight && chosenIndex < spawnList.Length - 1)
		{
			chosenIndex++;
			cumulativeWeight += spawnList [chosenIndex].weight;
		}

		Instantiate (spawnList [chosenIndex].pickUp, new Vector2(spawnPoints[spawnPointIndex].transform.position.x, pickUpPositionY), Quaternion.identity);
	}
	#endregion
}
