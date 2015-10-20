using UnityEngine;
using System.Collections;

public class UnpauseOnStart : MonoBehaviour {

	void Awake(){
		if(Time.timeScale != 1f){
			Time.timeScale = 1f;
		}
	}
}
