using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharactersOnMissionList : MonoBehaviour {

	public static CharactersOnMissionList control;
	public GameObject[] charactersList ;
	public int maxCharacters = 4;
	int charactersUsed = 0;
	bool hardCoreMode = false;

	void Awake(){
		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}else if(control != this){
			Destroy (gameObject);
		}
	}


	public void setCharactersList(GameObject[] newCharactersList){
		charactersList = new GameObject[Mathf.Min(maxCharacters, newCharactersList.Length)];
		charactersUsed = Mathf.Min(charactersList.Length, newCharactersList.Length);
		for(int i=0; i < charactersUsed; i ++){
			charactersList[i] = newCharactersList[i];
		}
	}

	public bool addCharacter(GameObject newCharacter){
		if(maxCharacters >= charactersList.Length+1){
			GameObject[] newCharactersList = new GameObject[charactersList.Length+1];
			for(int i=0; i < charactersList.Length; i ++){
				newCharactersList[i] = charactersList[i];
			}
			newCharactersList[newCharactersList.Length-1] = newCharacter;
			setCharactersList(newCharactersList);
			return true;
		}else{
			return false;
		}
	}

	public bool canAddCharacter(){
		return maxCharacters >= charactersList.Length+1;
	}

	public GameObject[] getCharactersList(){
		return charactersList;
	}

	public void setHardCoreMode(bool hardCoreMode){
		this.hardCoreMode = hardCoreMode;
	}

	public bool getHardCoreMode(){
		return hardCoreMode;
	}
}
