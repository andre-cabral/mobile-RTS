using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats {

	public int characterAvailableStartingQuantity;
	public int characterAvailableQuantity;
	public string characterCode;
	GameObject gameController;
	PlayersCounter playersCounter;

	public override void Awake(){
		base.Awake ();
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		playersCounter = gameController.GetComponent<PlayersCounter>();
		playersCounter.characterSpawned();
	}

	public override void DeathEffects(){
		base.DeathEffects();
		gameObject.tag = Tags.playerDead;

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
}
