using UnityEngine;
using System.Collections;

public class ExitMissionCollider : MonoBehaviour {

	ExitMission exitMission;

	void Awake () {
		exitMission = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ExitMission>();
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.CompareTag(Tags.player)){
			exitMission.EnteredExit();
		}
	}
}
