﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvailableCharacterTemplate : MonoBehaviour {

	public Text text;
	string originalText;
	GameObject characterPrefab;
	PlayerStats unitStats;
	AvailableCharactersManager availableCharactersManager;
	CharactersOnMissionManager charactersOnMissionManager;

	void Awake(){
		charactersOnMissionManager = GameObject.FindGameObjectWithTag(Tags.charactersOnMissionController).GetComponent<CharactersOnMissionManager>();
		originalText = text.text;
	}

	public void setCharacterPrefab(GameObject characterPrefab){
		this.characterPrefab = characterPrefab;
		this.unitStats = characterPrefab.GetComponent<PlayerStats>();
	}

	public void setAvailableCharactersManager(AvailableCharactersManager availableCharactersManager){
		this.availableCharactersManager = availableCharactersManager;
	}

	public void changeText(){
		text.text = originalText;
		text.text = text.text.Replace("{name}", unitStats.characterName);
		text.text = text.text.Replace("{availableCharactersNumber}", unitStats.getCharacterAvailableQuantity().ToString());
	}

	public void addCharacterClick(){
		if(charactersOnMissionManager.HasEmptyCharacter() && unitStats.HasAvailableCharacter()){
			availableCharactersManager.AddCharacterToMission(unitStats);
			charactersOnMissionManager.AddCharacter(characterPrefab);
		}
		//addToList(characterPrefab)
	}
}
