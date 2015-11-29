using UnityEngine;
using System.Collections;

public class EnemyStats : UnitStats {

	public GameObject[] objectsToDeactivateOnDeath;
	public bool destroyOnDeath = false;
	public Prisoner prisonerToSave;
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

		if(prisonerToSave != null){
			if(getUnitWhoKilledThis() != null ){
				if(getUnitWhoKilledThis().GetComponent<PrisonersSaved>() != null){
					prisonerToSave.SavePrisoner(getUnitWhoKilledThis());
				}else{
					FirstAliveCharacterSavePrisoner();
				}
			}else{
				FirstAliveCharacterSavePrisoner();
			}
		}
	}

	void FirstAliveCharacterSavePrisoner(){
		GameObject[] allCharacters = gameController.GetComponent<CharactersManager>().GetAllCharacters();
		bool savedThePrisoner = false;
		foreach(GameObject character in allCharacters){
			if(!savedThePrisoner && !character.GetComponent<UnitStats>().getIsDead()){
				prisonerToSave.SavePrisoner(character);
				savedThePrisoner = true;
			}
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
