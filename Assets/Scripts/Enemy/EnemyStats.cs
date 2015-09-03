using UnityEngine;
using System.Collections;

public class EnemyStats : UnitStats {

	public GameObject[] objectsToDeactivateOnDeath;

	public override void DeathEffects(){
		base.DeathEffects();
		gameObject.tag = Tags.enemyrDead;
		foreach(GameObject objToDeactivate in objectsToDeactivateOnDeath){
			objToDeactivate.SetActive(false);
		}
	}
}
