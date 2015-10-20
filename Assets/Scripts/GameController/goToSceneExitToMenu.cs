using UnityEngine;
using System.Collections;

public class goToSceneExitToMenu : MonoBehaviour {
	public string sceneName;
	StageEnd stageEnd;

	void Awake(){
		stageEnd = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<StageEnd>();
	}

	public void selectScene (){
		stageEnd.ExitToMenu();
		Application.LoadLevel(sceneName);
	}
}
