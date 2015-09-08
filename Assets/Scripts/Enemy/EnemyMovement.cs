using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Vector3 lastPlayerSeenResetPosition = new Vector3(9999f,9999f,9999f);
	public Vector3 lastPlayerSeenPosition = new Vector3(9999f,9999f,9999f);
	bool isSeeingPlayer = false;

	GameObject lastCharacterSeen;


	public void FlipWithSpeed(Vector3 end, float speed){
		Quaternion finalRotation = Quaternion.LookRotation(end - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * speed);
	}
	
	public void SeePlayer(){
		lastPlayerSeenPosition = lastCharacterSeen.transform.position;
	}


	public Vector3 getLastPlayerSeenResetPosition(){
		return lastPlayerSeenResetPosition;
	}

	public GameObject getLastCharacterSeen(){
		return lastCharacterSeen;
	}

	public void setLastCharacterSeen(GameObject lastCharacterSeen){
		this.lastCharacterSeen = lastCharacterSeen;
	}

	public bool getIsSeeingPlayer(){
		return isSeeingPlayer;
	}
	
	public void setIsSeeingPlayer(bool isSeeingPlayer){
		this.isSeeingPlayer = isSeeingPlayer;
	}


	public Vector3 getLastPlayerSeenPosition(){
		return lastPlayerSeenPosition;
	}
	
	public void setLastPlayerSeenPosition(Vector3 lastPlayerSeenPosition){
		this.lastPlayerSeenPosition = lastPlayerSeenPosition;
	}

	public void resetLastPlayerSeenPosition(){
		lastPlayerSeenPosition = lastPlayerSeenResetPosition;
	}


	public bool isLastCharacterSeenDead(){
		if(lastCharacterSeen == null){
			return true;
		}
		if(lastCharacterSeen.GetComponent<UnitStats>().getIsDead()){
			return true;
		}
		return false;
	}
}
