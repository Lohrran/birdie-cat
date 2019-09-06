using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawn : MonoBehaviour
{
	public int qtdEnemiesAllowedOnScene;
	public float delayAfterAlert;
	public float spawnDelay;

	public GameObject [] enemySpawnPoint;
	private GameObject[] enemiesOnScene;
	private int enemySpawnPointIndex;

	private bool canISpawn;
	private IEnumerator coroutine;

	private List <int> memory = new List<int>();

	#region Unity
	void Start () 
	{
		canISpawn = true;
	}

	void Update()
	{
		enemiesOnScene = GameObject.FindGameObjectsWithTag ("Enemy");
		coroutine = TheGatesOfHell (delayAfterAlert);

		if(canISpawn == true)
		{
			StartCoroutine (coroutine);
		}
	}
	#endregion
		
	#region Spawn
	private IEnumerator TheGatesOfHell(float waitNumber)
	{
		//enemySpawnPointIndex = Random.Range (0, enemySpawnPoint.Length);
		if (enemiesOnScene.Length < qtdEnemiesAllowedOnScene)
		{
			enemySpawnPointIndex = Random.Range (0, enemySpawnPoint.Length);

			Remove ();

			if (!memory.Contains (enemySpawnPointIndex)) 
			{
				if (enemySpawnPointIndex % 2 == 0 || enemySpawnPointIndex == 0)
				{
					if (!memory.Contains (enemySpawnPointIndex + 1))
					{
						SpawnBack ();
						yield return new WaitForSeconds (delayAfterAlert);
						SpawnNext ();	
					}

				}
				else
				{
					if (!memory.Contains (enemySpawnPointIndex - 1))
					{
						SpawnBack ();
						yield return new WaitForSeconds (delayAfterAlert);
						SpawnNext ();
					}

				}
			}
		}
	}

	void SpawnBack()
	{
		memory.Add (enemySpawnPointIndex);
		canISpawn = false;
		enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().DecideWhoSpawn ();
		enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().SpawnAlert ();

	}

	void SpawnNext()
	{
		enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().DestroyAlert ();
		enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().SpawnEnemy ();
		canISpawn = true;
	}

	void Remove()
	{
		if (memory.Contains (enemySpawnPointIndex))
		{
			if (enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().inUse == false)
			{
				memory.Remove (enemySpawnPointIndex);
			} 
		}
	}
}
#endregion