using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawnable
{
	public GameObject enemyPrefab;
	public GameObject alertPrefab;
	[HideInInspector]
	public float weight;
}

public class EnemySpawnScript : MonoBehaviour 
{
	public Spawnable[] spawnList; 
	public float xPos;

	private float totalSpawnWeight;
	private int chosenIndex;
	private GameObject alertToDestroy;
	[HideInInspector]
	public GameObject enemyGhost;
	[HideInInspector]
	public bool inUse;

	private GameObject alert;
	private GameObject enemy;

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
		AvailableSpot ();
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
		//alertToDestroy = (GameObject) Instantiate (spawnList [chosenIndex].alertPrefab, new Vector2(xPos, transform.position.y), Quaternion.identity);

		alert = ObjectPooler.SharedInstance.GetPooledGameObject (spawnList[chosenIndex].alertPrefab.tag);
		if (alert != null)
		{
			alert.transform.position = new Vector2 (xPos, transform.position.y);
			alert.SetActive (true);
			inUse = true;
		}
	}

	public void DestroyAlert()
	{
		alert.SetActive (false);
	}

	public void SpawnEnemy()
	{
		enemy = ObjectPooler.SharedInstance.GetPooledGameObject (spawnList[chosenIndex].enemyPrefab.tag);
		if (enemy != null)
		{
			enemy.transform.position = new Vector2 (transform.position.x, transform.position.y);
			enemy.SetActive (true);
		}
	}

	public void AvailableSpot()
	{
		if (enemy != null)
		{
			if (enemy.GetComponent<EnemyMasterScript> ().GetCurrentState () == true) 
			{
				inUse = true;
			}
			else
			{
				inUse = false;
			}
		}
	}
	#endregion
}
