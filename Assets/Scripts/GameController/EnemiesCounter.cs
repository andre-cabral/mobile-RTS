using UnityEngine;
using System.Collections;

public class EnemiesCounter : MonoBehaviour {

	int totalEnemies = 0;
	int enemiesAlive = 0;
	int enemiesDead = 0;

	public void enemySpawned(){
		totalEnemies++;
		enemiesAlive++;
	}

	public void enemyKilled(){
		enemiesAlive--;
		enemiesDead++;
	}

	public bool areAllEnemiesDead(){
		return enemiesAlive == 0;
	}
}
