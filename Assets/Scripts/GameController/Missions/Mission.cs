using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour {

	int totalEnemies = 0;
	int enemiesAlive = 0;
	int enemiesDead = 0;
	StageEnd stageEnd;
	
	void Awake(){
		stageEnd = GetComponent<StageEnd>();
	}

	public void setTotalEnemies(int totalEnemies){
		this.totalEnemies = totalEnemies;
	}

	public void setEnemiesAlive(int enemiesAlive){
		this.enemiesAlive = enemiesAlive;
	}

	public int getEnemiesAlive(){
		return enemiesAlive;
	}

	public virtual void setEnemiesDead(int enemiesDead){
		this.enemiesDead = enemiesDead;
	}

	public virtual void MissionComplete(){
		stageEnd.Win();
	}
}
