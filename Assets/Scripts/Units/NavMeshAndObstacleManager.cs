using UnityEngine;
using System.Collections;

public class NavMeshAndObstacleManager : MonoBehaviour {
	NavMeshAgent navMeshAgent;
	NavMeshObstacle navMeshObstacle;
	bool checkObstacle = true;
	Vector3 destination;

	void Awake(){
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshObstacle = GetComponent<NavMeshObstacle>();
	}

	void Update(){
		if(navMeshAgent.enabled && checkObstacle){
			if(navMeshAgent.velocity.magnitude == 0){
				navMeshAgentToObstacle();
			}
		}

		if(!navMeshObstacle.enabled && !navMeshAgent.enabled){
			navMeshAgent.enabled = true;
		}
		if(navMeshAgent.enabled){
			if(navMeshAgent.velocity.magnitude > 0){
				checkObstacle = true;
			}

			if(navMeshAgent.destination != destination){
				navMeshAgent.destination = destination;
			}
		}
	}

	public void setDestination(Vector3 destination){
		checkObstacle = false;
		navMeshObstacleToAgent();
		this.destination = destination;
	}

	public void navMeshAgentStop(){
		/*
		navMeshAgent.velocity = Vector3.zero;
		navMeshAgent.destination = transform.position;
		*/
		navMeshAgentToObstacle();
	}
	
	public void navMeshAgentToObstacle(){
		if(navMeshAgent.enabled && !navMeshObstacle.enabled){
			navMeshAgent.enabled = false;
			navMeshObstacle.enabled = true;
		}
	}
	
	public void navMeshObstacleToAgent(){
		navMeshObstacle.enabled = false;
	}

	public float getStoppingDistance(){
		if(navMeshAgent.enabled){
			return navMeshAgent.stoppingDistance;
		}else{
			return 0f;
		}
	}

	public float getNavMeshAgentVelocity(){
		if(navMeshAgent.enabled){
			return navMeshAgent.velocity.magnitude;
		}else{
			return 0f;
		}
	}
}
