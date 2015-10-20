using UnityEngine;
using System.Collections;

public class StartMissionButton : MonoBehaviour {

	CharactersOnMissionManager charactersOnMissionManager;
	public string sceneToGoName;


	void Awake(){
		charactersOnMissionManager = GameObject.FindGameObjectWithTag(Tags.charactersOnMissionController).GetComponent<CharactersOnMissionManager>();
	}

	public void ClickStartMission(){
		if(charactersOnMissionManager.clickStartMission()){
			Application.LoadLevel(sceneToGoName);
		}
	}

}
