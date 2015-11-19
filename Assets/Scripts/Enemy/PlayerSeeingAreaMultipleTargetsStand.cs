using UnityEngine;
using System.Collections;

public class PlayerSeeingAreaMultipleTargetsStand : MonoBehaviour {
	
	public Transform enemyTransform;
	public StandAndAttackArea enemyMovement;
	public UnitStats enemyStats;
	//public bool canSeeStealthPlayer = false;
	public bool needLineOfSightToGetPlayer = false;

	void OnTriggerEnter(Collider collider){
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.player){
			bool canSeePlayer = true;
			if(needLineOfSightToGetPlayer){
				canSeePlayer = HasLineOfSight(enemyTransform.position, collidedObject.transform.position);
			}
			
			if(canSeePlayer){
				enemyMovement.setLastPlayerSeenPosition(collidedObject.transform.position);
				enemyMovement.setLastCharacterSeen(collidedObject);
				if(!enemyMovement.getIsSeeingPlayer() ){
					enemyMovement.addTarget(collidedObject);
				}
			}
		}
	}
	
	void OnTriggerExit(Collider collider){
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.player){
			enemyMovement.removeTarget(collidedObject);
		}
	}
	
	//if there is another tag to hide the player, add it here in the foreach
	bool HasLineOfSight(Vector3 origin, Vector3 target){
		bool hasLOS = true;
		
		Vector3 direction = target - origin;
		float distance = (target - origin).magnitude;
		
		RaycastHit[] allHits = Physics.RaycastAll(origin, direction, distance);
		
		foreach(RaycastHit hit in allHits){
			if(hit.collider.gameObject.tag == Tags.wall){
				hasLOS = false;
			}
		}
		
		return hasLOS;
	}
	
}
