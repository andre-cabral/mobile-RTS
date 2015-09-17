using UnityEngine;
using System.Collections;

public class StageEnd : MonoBehaviour {

	ControlsManager controlsManager;
	GameObject[] objectsToShowOnGameOver;

	CharactersOnMissionList charactersOnMissionList;
	AvailableCharactersList availableCharactersList;
	CharactersManager charactersManager;

	void Awake(){
		controlsManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ControlsManager>();
		objectsToShowOnGameOver = GameObject.FindGameObjectsWithTag(Tags.gameOverObject);
		setObjectsToShowOnGameOver(false);

		charactersOnMissionList = GameObject.FindGameObjectWithTag(Tags.charactersOnMissionList).GetComponent<CharactersOnMissionList>();
		availableCharactersList = GameObject.FindGameObjectWithTag(Tags.availableCharactersList).GetComponent<AvailableCharactersList>();
		charactersManager = GetComponent<CharactersManager>();
	}

	public void charactersOnMissionReturnToAvailableList(){
		GameObject[] array = charactersManager.GetAllCharacters();
		for(int i = 0; i<array.Length; i++){
			PlayerStats playerStats = array[i].GetComponent<PlayerStats>();
			if( !charactersOnMissionList.getHardCoreMode() ){
				availableCharactersList.AddOneToCharacterAvailableQuantity(playerStats);
			}else{
				if( !playerStats.getIsDead() ){
					availableCharactersList.AddOneToCharacterAvailableQuantity(playerStats);
				}else{
					Debug.Log("DEAD");
				}
			}
			array[i] = null;
		}
	}

	public void Win(){
		charactersOnMissionReturnToAvailableList();
	}

	public void Lose(){
		GameOverEvent();
	}

	public void ExitToMenu(){
		charactersOnMissionReturnToAvailableList();
	}


	
	public void GameOverEvent () {
		controlsManager.GameOverPause();
		setObjectsToShowOnGameOver(true);
	}
	
	void setObjectsToShowOnGameOver(bool show){
		foreach(GameObject objectToSet in objectsToShowOnGameOver){
			objectToSet.SetActive(show);
		}
	}
}