using UnityEngine;
using System.Collections;

public class KillAllEnemiesMission : Mission {

	public bool areAllEnemiesDead(){
		return getEnemiesAlive() == 0;
	}

	public override void setEnemiesDead(int enemiesDead){
		base.setEnemiesDead(enemiesDead);
		if(areAllEnemiesDead()){
			MissionComplete();
		}
	}
}
