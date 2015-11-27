using UnityEngine;
using System.Collections;

public class PrisonerSavedByCollision : Prisoner {

	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag(Tags.player)){
			SavePrisoner(collider.gameObject);
		}
	}
}
