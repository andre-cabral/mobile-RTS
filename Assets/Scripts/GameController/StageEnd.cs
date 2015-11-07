using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageEnd : MonoBehaviour {

	ControlsManager controlsManager;
	GameObject[] objectsToShowOnGameOver;
	GameObject[] objectsToShowOnGameWin;

	CharactersOnMissionList charactersOnMissionList;
	AvailableCharactersList availableCharactersList;
	CharactersManager charactersManager;

	void Awake(){
		controlsManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ControlsManager>();
		objectsToShowOnGameOver = GameObject.FindGameObjectsWithTag(Tags.gameOverObject);
		setObjectsToShowOnGameOver(false);

		objectsToShowOnGameWin = GameObject.FindGameObjectsWithTag(Tags.gameWinObject);
		setObjectsToShowOnGameWin(false);

		charactersOnMissionList = GameObject.FindGameObjectWithTag(Tags.charactersOnMissionList).GetComponent<CharactersOnMissionList>();
		availableCharactersList = GameObject.FindGameObjectWithTag(Tags.availableCharactersList).GetComponent<AvailableCharactersList>();
		charactersManager = GetComponent<CharactersManager>();
	}

	public void charactersOnMissionReturnToAvailableList(bool savePrisoners){
		GameObject[] array = charactersManager.GetAllCharacters();
		for(int i = 0; i<array.Length; i++){
			PlayerStats playerStats = array[i].GetComponent<PlayerStats>();
			if( !charactersOnMissionList.getHardCoreMode() ){
				if(!playerStats.getIsPrisoner() || savePrisoners){
					availableCharactersList.AddOneToCharacterAvailableQuantity(playerStats);
				}
			}else{
				if( !playerStats.getIsDead() ){
					if(!playerStats.getIsPrisoner() || savePrisoners){
						availableCharactersList.AddOneToCharacterAvailableQuantity(playerStats);
					}
				}
			}
			/*
			if(savePrisoners && !playerStats.getIsDead()){
				PrisonersSaved prisonersSavedScript = array[i].GetComponent<PrisonersSaved>();
				List<GameObject> prisonersSaved = prisonersSavedScript.getPrisonersSaved();
				foreach(GameObject prisoner in prisonersSaved){
					availableCharactersList.AddOneToCharacterAvailableQuantity(prisoner.GetComponent<PlayerStats>());
				}
			}
			*/
			array[i] = null;
		}
	}

	public void Win(){
		controlsManager.StageEndPause();
		setObjectsToShowOnGameWin(true);
	}

	public void Lose(){
		controlsManager.StageEndPause();
		setObjectsToShowOnGameOver(true);
	}

	public void ExitToMenu(){
		charactersOnMissionReturnToAvailableList(false);
	}

	public void ExitToMenuSavingPrisoners(){
		charactersOnMissionReturnToAvailableList(true);
	}
	
	void setObjectsToShowOnGameOver(bool show){
		foreach(GameObject objectToSet in objectsToShowOnGameOver){
			objectToSet.SetActive(show);
		}
	}

	void setObjectsToShowOnGameWin(bool show){
		foreach(GameObject objectToSet in objectsToShowOnGameWin){
			objectToSet.SetActive(show);
		}
	}
}