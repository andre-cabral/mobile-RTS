using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats {

	public int characterAvailableQuantity;
	public string characterCode;

	public override void DeathEffects(){
		base.DeathEffects();
		gameObject.tag = Tags.playerDead;
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
}
