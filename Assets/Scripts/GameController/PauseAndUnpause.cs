using UnityEngine;
using System.Collections;

public class PauseAndUnpause : MonoBehaviour {

	ControlsManager controlsManager;

	void Awake () {
		controlsManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ControlsManager>();
	}
	
	public void pause(){
		controlsManager.setPause(true);
	}

	public void unpause(){
		controlsManager.setPause(false);
	}
}
