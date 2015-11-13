using UnityEngine;
using System.Collections;

public class SpawnerStats : UnitStats {

	public GameObject[] objectsToDeactivateOnDeath;
	public bool destroyOnDeath = false;
	EnemyMovement enemyMovement;
	GameObject gameController;
	EnemiesCounter enemiesCounter;

	public override void Awake(){
		base.Awake();
		enemyMovement = GetComponent<EnemyMovement>();

		gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		enemiesCounter = gameController.GetComponent<EnemiesCounter>();
	}

	void Start(){
		enemiesCounter.enemySpawned();
	}

	public override void DeathEffects(){
		base.DeathEffects();
		gameObject.tag = Tags.enemyDead;
		foreach(GameObject objToDeactivate in objectsToDeactivateOnDeath){
			objToDeactivate.SetActive(false);
		}
		enemiesCounter.enemyKilled();

		if(destroyOnDeath){
			Destroy(gameObject);
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
