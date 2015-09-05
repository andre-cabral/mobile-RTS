using UnityEngine;
using System.Collections;

public class AvailableCharactersList : MonoBehaviour {

	public static AvailableCharactersList control;
	public GameObject[] allCharactersPrefabs;
	public int[] availableCharactersNumber;


	void Awake(){
		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}else if(control != this){
			Destroy (gameObject);
		}
	}

	public bool HasAvailableCharacter(int index){
		if(availableCharactersNumber[index]>0){
			return true;
		}else{
			return false;
		}
	}

	public void addOneToAvailableCharacter(int index){
		availableCharactersNumber[index]++;
	}
	
	public bool AddCharacterToMission(int index){
		if(availableCharactersNumber[index]>0){
			availableCharactersNumber[index]--;
			return true;
		}else{
			return false;
		}
	}
	
	public void RemoveCharacterFromMission(int index){
		availableCharactersNumber[index]++;
	}
	
	public int getAvailableCharacterNumber(int index){
		return availableCharactersNumber[index];
	}

	public GameObject[] getAllCharactersPrefabs(){
		return allCharactersPrefabs;
	}
}
