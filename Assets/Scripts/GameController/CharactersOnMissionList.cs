using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharactersOnMissionList : MonoBehaviour {

	public static CharactersOnMissionList control;
	public GameObject[] charactersList ;
	public int maxCharacters = 4;
	int charactersUsed = 0;

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

	public GameObject[] getCharactersList(){
		return charactersList;
	}

}
