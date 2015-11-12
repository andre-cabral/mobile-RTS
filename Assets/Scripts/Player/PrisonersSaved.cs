using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrisonersSaved : MonoBehaviour {

	List<GameObject> prisonersSaved = new List<GameObject>();
	CharactersOnMissionList charactersOnMissionList;
	CharactersManager charactersManager;

	void Awake(){
		charactersOnMissionList = GameObject.FindGameObjectWithTag(Tags.charactersOnMissionList).GetComponent<CharactersOnMissionList>();
		charactersManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<CharactersManager>();
	}

	public bool SavePrisoner(GameObject prisoner, Vector3 prisonerPosition, Quaternion prisonerRotation){
		if(charactersOnMissionList.canAddCharacter()){
			GameObject newCharacter = charactersManager.AddNewCharacter(prisoner, prisonerPosition, prisonerRotation);
			newCharacter.GetComponent<PlayerStats>().setIsPrisoner(true);
			charactersOnMissionList.addCharacter(newCharacter);
			return true;
		}else{
			return false;
		}
	}

	public List<GameObject> getPrisonersSaved(){
		return prisonersSaved;
	}
}
