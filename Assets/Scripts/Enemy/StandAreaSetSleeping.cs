using UnityEngine;
using System.Collections;

public class StandAreaSetSleeping : MonoBehaviour {

	public StandAndAttackArea standAreaMovement;

	public void GoingToSleepEnd(){
		standAreaMovement.endSleeping();
	}
	public void AwakingEnd(){
		standAreaMovement.endSleeping();
	}
}
