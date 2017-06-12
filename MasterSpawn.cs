using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawn : MonoBehaviour {

	public int qtdEnemiesAllowedOnScene;
	public float delayAfterAlert;
	public float spawnDelay;

	public GameObject [] enemySpawnPoint;
	private GameObject[] enemiesOnScene;
	private int enemySpawnPointIndex;

	private bool canISpawn;
	private IEnumerator coroutine;

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
		enemySpawnPointIndex = Random.Range (0, enemySpawnPoint.Length);

		if(enemiesOnScene.Length < qtdEnemiesAllowedOnScene)
		{
			if (enemySpawnPoint[enemySpawnPointIndex].GetComponent<EnemySpawnScript>().inUse == false)
			{
				if (enemySpawnPointIndex / 2 == 0 || enemySpawnPointIndex == 0) 
				{
					if (enemySpawnPoint [enemySpawnPointIndex + 1].GetComponent<EnemySpawnScript> ().inUse == false) 
					{
						canISpawn = false;

						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().DecideWhoSpawn ();
						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().SpawnAlert ();

						yield return new WaitForSeconds (delayAfterAlert);

						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().DestroyAlert ();
						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().SpawnEnemy ();

						yield return new WaitForSeconds (spawnDelay);

						canISpawn = true;
					}
				} 

				else 
				{
					if (enemySpawnPoint [enemySpawnPointIndex - 1].GetComponent<EnemySpawnScript> ().inUse == false) 
					{
						canISpawn = false;

						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().DecideWhoSpawn ();
						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().SpawnAlert ();

						yield return new WaitForSeconds (delayAfterAlert);

						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().DestroyAlert ();
						enemySpawnPoint [enemySpawnPointIndex].GetComponent<EnemySpawnScript> ().SpawnEnemy ();

						yield return new WaitForSeconds (spawnDelay);

						canISpawn = true;
					}
					
				}
			}
		}
	}
	#endregion
}