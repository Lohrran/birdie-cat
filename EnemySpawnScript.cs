using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

	[System.Serializable]
	public class Spawnable
	{
		public GameObject enemyPrefab;
		public GameObject alertPrefab;
		[HideInInspector]
		public float weight;
	}

	public Spawnable[] spawnList; 
	public float xPos;

	private float totalSpawnWeight;
	private int chosenIndex;
	private GameObject alertToDestroy;
	[HideInInspector]
	public GameObject enemyGhost;
	[HideInInspector]
	public bool inUse;

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
		for(int i = 0; i < spawnList.Length; i++)
		{
			spawnList[i].weight = spawnList[i].enemyPrefab.GetComponent<EnemyMasterScript> ().weight;
		}

		OnValidate();

		inUse = false;
	}

	void Update()
	{
		if (enemyGhost != null)
		{
			inUse = true;
		}

		else
		{
			inUse = false;
		}
	}
	#endregion

	#region Spawn
	public void DecideWhoSpawn()
	{
		float pick = Random.value * totalSpawnWeight;
		chosenIndex = 0;
		float cumulativeWeight = spawnList [0].weight;

		while (pick > cumulativeWeight && chosenIndex < spawnList.Length - 1)
		{
			chosenIndex++;
			cumulativeWeight += spawnList [chosenIndex].weight;
		}
	}

	public void SpawnAlert()
	{
		alertToDestroy = (GameObject) Instantiate (spawnList [chosenIndex].alertPrefab, new Vector2(xPos, transform.position.y), Quaternion.identity);
	}

	public void DestroyAlert()
	{
		Destroy (alertToDestroy);
	}

	public void SpawnEnemy()
	{
		enemyGhost = (GameObject) Instantiate (spawnList [chosenIndex].enemyPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
	}
	#endregion
}
