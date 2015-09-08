using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterOnMissionTemplate : MonoBehaviour {

	public Text text;
	string originalText;
	GameObject characterPrefab;
	PlayerStats unitStats;
	AvailableCharactersManager availableCharactersManager;
	
	void Awake(){
		originalText = text.text;
		availableCharactersManager = GameObject.FindGameObjectWithTag(Tags.availableCharactersController).GetComponent<AvailableCharactersManager>();
	}
	
	public void setCharacterPrefab(GameObject characterPrefab){
		this.characterPrefab = characterPrefab;
		this.unitStats = characterPrefab.GetComponent<PlayerStats>();
	}

	public GameObject getCharacterPrefab(){
		return characterPrefab;
	}
	
	public void changeText(){
		text.text = originalText;
		text.text = text.text.Replace("Empty", unitStats.characterName);
	}

	public void resetText(){
		text.text = originalText;
	}
	
	public void removeCharacterClick(){
		if(characterPrefab != null){
			characterPrefab = null;
			resetText();
			availableCharactersManager.RemoveCharacterFromMission(unitStats);
		}
	}		

}