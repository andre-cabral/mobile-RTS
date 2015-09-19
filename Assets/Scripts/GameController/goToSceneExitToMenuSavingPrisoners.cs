using UnityEngine;
using System.Collections;

public class goToSceneExitToMenuSavingPrisoners : MonoBehaviour {
	public string sceneName;
	StageEnd stageEnd;

	void Awake(){
		stageEnd = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<StageEnd>();
	}

	public void selectScene (){
		stageEnd.ExitToMenuSavingPrisoners();
		Application.LoadLevel(sceneName);
	}
}
