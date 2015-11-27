using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] enemyTypesToSpawn;
	public Transform[] spawnPoints;
	public GameObject spawnerSpriteObject;
	public float timeToSpawn = 4f;
	public int numberOfSpawns = 1;
	public bool randomNumberOfEnemies = true;
	public bool hasSpawnAnimation = false;
	public float[] percentagesToChangeAnimation;
	public UnitStats unitStats;
	bool hasPercentagesAnimation = false;
	int percentagesReached = 0;
	int numberOfSpawnsOcurred = 0;
	float timeToSpawnPassed = 0f;
	bool startSpawnTimer = false;
	Animator animator;
	HashAnimatorSpawner hashAnimatorSpawner;

	void Awake(){
		if(percentagesToChangeAnimation.Length > 0){
			hasPercentagesAnimation = true;
		}
		if(hasPercentagesAnimation || hasSpawnAnimation){
			animator = spawnerSpriteObject.GetComponent<Animator>();
			hashAnimatorSpawner = spawnerSpriteObject.GetComponent<HashAnimatorSpawner>();
		}
	}

	void Update() {
		if(startSpawnTimer){
			if(timeToSpawnPassed <= timeToSpawn){
				timeToSpawnPassed += Time.deltaTime;

				if(hasPercentagesAnimation && animator != null && hashAnimatorSpawner != null 
				   && percentagesReached < percentagesToChangeAnimation.Length){

					if(timeToSpawnPassed >= timeToSpawn * percentagesToChangeAnimation[percentagesReached]/100){
						percentagesReached++;
						animator.SetInteger(hashAnimatorSpawner.percentagesReached, percentagesReached);
					}

				}
			}else{
				if(numberOfSpawnsOcurred < numberOfSpawns){
					SpawnEnemies();
				}
			}
		}
	}

	public void setStartSpawnTimer(bool startSpawnTimer){
		this.startSpawnTimer = startSpawnTimer;
	}

	public void SpawnEnemies(){
		timeToSpawnPassed = 0f;

		int numberOfEnemies = 0;
		if(randomNumberOfEnemies){
			numberOfEnemies = Random.Range(1, spawnPoints.Length);
		}else{
			numberOfEnemies = spawnPoints.Length-1;
		}

		for(int i=0; i <= numberOfEnemies; i++){
			Instantiate(enemyTypesToSpawn[Random.Range(0, enemyTypesToSpawn.Length)], spawnPoints[i].position, spawnPoints[i].rotation);
		}

		if(hasSpawnAnimation && animator != null && hashAnimatorSpawner != null){
			animator.SetBool(hashAnimatorSpawner.spawn, true);
		}

		numberOfSpawnsOcurred++;

		if(numberOfSpawnsOcurred >= numberOfSpawns){
			unitStats.setLife(gameObject, 0);
			enabled = false;
		}
	}
}
