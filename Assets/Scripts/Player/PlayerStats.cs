using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats {

	public int characterAvailableStartingQuantity;
	public int characterAvailableQuantity;
	public string characterCode;
	CharacterMovement characterMovement;
	GameObject gameController;
	PlayersCounter playersCounter;
	bool isPrisoner = false;

	public override void Awake(){
		base.Awake ();
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		playersCounter = gameController.GetComponent<PlayersCounter>();
		characterMovement = GetComponent<CharacterMovement>();
		playersCounter.characterSpawned();
	}

	public override void DeathEffects(){
		base.DeathEffects();
		gameObject.tag = Tags.playerDead;

		characterMovement.CharacterDeathEffects();

		playersCounter.characterKilled();
	}

	public bool HasAvailableCharacter(){
		if(characterAvailableQuantity>0){
			return true;
		}else{
			return false;
		}
	}

	public int getCharacterAvailableQuantity(){
		return characterAvailableQuantity;
	}

	public void setCharacterAvailableQuantity(int characterAvailableQuantity){
		this.characterAvailableQuantity = characterAvailableQuantity;
	}

	public void addCharacterAvailableQuantity(int numberToAdd){
		characterAvailableQuantity += numberToAdd;
	}

	public bool removeOneFromCharacterAvailableQuantity(){
		if(characterAvailableQuantity > 0){
			characterAvailableQuantity--;
			return true;
		}
		return false;
	}

	public void resetCharacterAvailableQuantity(){
		characterAvailableQuantity = characterAvailableStartingQuantity;
	}

	public void setIsPrisoner(bool isPrisoner){
		this.isPrisoner = isPrisoner;
	}

	public bool getIsPrisoner(){
		return isPrisoner;
	}
}
