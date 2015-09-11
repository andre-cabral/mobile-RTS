using UnityEngine;
using System.Collections;

public class EnemyStats : UnitStats {

	public GameObject[] objectsToDeactivateOnDeath;
	EnemyMovement enemyMovement;

	public override void Awake(){
		base.Awake();
		enemyMovement = GetComponent<EnemyMovement>();
	}

	public override void DeathEffects(){
		base.DeathEffects();
		gameObject.tag = Tags.enemyrDead;
		foreach(GameObject objToDeactivate in objectsToDeactivateOnDeath){
			objToDeactivate.SetActive(false);
		}
	}

	public override void takeDamage(GameObject attacker, int attackingValue){
		base.takeDamage(attacker, attackingValue);
		if( /*(canSeeStealthPlayer || collidedObject.name != ClassesObjectsNames.stealth) && */
		   enemyMovement.getLastPlayerSeenPosition() == enemyMovement.getLastPlayerSeenResetPosition()
		   || enemyMovement.isLastCharacterSeenDead()
		   ){			
				enemyMovement.setLastPlayerSeenPosition(attacker.transform.position);
				enemyMovement.setLastCharacterSeen(attacker);
				if(!enemyMovement.getIsSeeingPlayer() ){
					enemyMovement.setIsSeeingPlayer(true);
				}
			}			
	}
}
