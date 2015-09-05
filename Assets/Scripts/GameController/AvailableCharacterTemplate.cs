using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvailableCharacterTemplate : MonoBehaviour {

	public Text text;
	string originalText;
	int characterIndex;
	GameObject characterPrefab;
	UnitStats unitStats;
	AvailableCharactersManager availableCharactersManager;
	CharactersOnMissionManager charactersOnMissionManager;

	void Awake(){
		charactersOnMissionManager = GameObject.FindGameObjectWithTag(Tags.charactersOnMissionController).GetComponent<CharactersOnMissionManager>();
		originalText = text.text;
	}

	public void setCharacterIndex(int characterIndex){
		this.characterIndex = characterIndex;
	}

	public void setCharacterPrefab(GameObject characterPrefab){
		this.characterPrefab = characterPrefab;
		this.unitStats = characterPrefab.GetComponent<UnitStats>();
	}

	public void setAvailableCharactersManager(AvailableCharactersManager availableCharactersManager){
		this.availableCharactersManager = availableCharactersManager;
	}

	public void changeText(){
		text.text = originalText;
		text.text = text.text.Replace("{name}", unitStats.characterName);
		text.text = text.text.Replace("{availableCharactersNumber}", availableCharactersManager.getAvailableCharacterNumber(characterIndex).ToString());
	}

	public void addCharacterClick(){
		if(charactersOnMissionManager.HasEmptyCharacter() && availableCharactersManager.HasAvailableCharacter(characterIndex)){
			availableCharactersManager.AddCharacterToMission(characterIndex);
			charactersOnMissionManager.AddCharacter(characterIndex, characterPrefab);
		}
		//addToList(characterPrefab)
	}
}
