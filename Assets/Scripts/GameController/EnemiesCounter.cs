using UnityEngine;
using System.Collections;

public class EnemiesCounter : MonoBehaviour {

	int totalEnemies = 0;
	int enemiesAlive = 0;
	int enemiesDead = 0;
	Mission mission;

	void Awake(){
		mission = GetComponent<Mission>();
	}

	public void enemySpawned(){
		totalEnemies++;
		enemiesAlive++;
		setNumbersOnMission();
	}

	public void enemyKilled(){
		enemiesAlive--;
		enemiesDead++;
		setNumbersOnMission();
	}

	public bool areAllEnemiesDead(){
		return enemiesAlive == 0;
	}

	public void setNumbersOnMission(){
		mission.setTotalEnemies(totalEnemies);
		mission.setEnemiesAlive(enemiesAlive);
		mission.setEnemiesDead(enemiesDead);
	}
}
