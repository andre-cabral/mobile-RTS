using UnityEngine;
using System.Collections;

public class SlowDownUnits : MonoBehaviour {

	public float speedReducer = 2;
	public float speedDivider = 0;

	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag(Tags.player)){
			collider.GetComponent<UnitStats>().ReduceSpeed(speedReducer);
			collider.GetComponent<UnitStats>().ReduceSpeedWithDivider(speedDivider);
		}

		if(collider.CompareTag(Tags.enemy)){
			collider.GetComponent<UnitStats>().ReduceSpeed(speedReducer);
			collider.GetComponent<UnitStats>().ReduceSpeedWithDivider(speedDivider);
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.CompareTag(Tags.player)){
			collider.GetComponent<UnitStats>().AddSpeed(speedReducer);
			collider.GetComponent<UnitStats>().AddSpeedWithDivider(speedDivider);
		}

		if(collider.CompareTag(Tags.enemy)){
			collider.GetComponent<UnitStats>().AddSpeed(speedReducer);
			collider.GetComponent<UnitStats>().AddSpeedWithDivider(speedDivider);
		}
	}
}
