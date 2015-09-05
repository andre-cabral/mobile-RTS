using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterOnMissionTemplate : MonoBehaviour {

	public Text text;
	string originalText;
	int characterIndex;
	GameObject characterPrefab;
	UnitStats unitStats;
	AvailableCharactersManager availableCharactersManager;
	
	void Awake(){
		originalText = text.text;
		availableCharactersManager = GameObject.FindGameObjectWithTag(Tags.availableCharactersController).GetComponent<AvailableCharactersManager>();
	}
	
	public void setCharacterIndex(int characterIndex){
		this.characterIndex = characterIndex;
	}
	
	public void setCharacterPrefab(GameObject characterPrefab){
		this.characterPrefab = characterPrefab;
		this.unitStats = characterPrefab.GetComponent<UnitStats>();
	}

	public GameObject getCharacterPrefab(){
		return characterPrefab;
	}
	
	public void changeText(){
		text.text = originalText;
		text.text = text.text.Replace("Empty", unitStats.name);
	}

	public void resetText(){
		text.text = originalText;
	}
	
	public void removeCharacterClick(){
		if(characterPrefab != null){
			characterPrefab = null;
			resetText();
			availableCharactersManager.RemoveCharacterFromMission(characterIndex);
		}
	}		

}