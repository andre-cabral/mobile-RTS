using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Attack))]
public class PatrolAndStalkMovement : EnemyMovement {
	
	public bool moveYAxis = false;
	
	public GameObject[] patrolWaypoints;
	public float distanceToChangeWaypoint = 0.5f;
	public float timeOnWaypoint = 0f;
	float timePassedOnWaypoint = 0f;
	private Vector3[] points;
	private int vectorIndex = 1;

	private Vector3 destination;
	
	private Animator enemyAnimator;
	private HashAnimatorUnit hashAnimator;
	
	NavMeshAgent agent;
	
	private EnemyStats enemyStats;

	Attack enemyAttack;
	
	private bool attacking = false;
	private bool patrolling = true;
	private bool stalking = false;
	
	public float lookAtPlayerSpeed = 3f;
	
	public virtual void Awake () {
		agent = gameObject.GetComponent<NavMeshAgent>();
		
		//Patrol instance START
		points = new Vector3[patrolWaypoints.Length];
		
		for(int i=0; i<patrolWaypoints.Length; i++ ){
			if(!moveYAxis){
				points[i] = new Vector3(patrolWaypoints[i].transform.position.x, transform.position.y, patrolWaypoints[i].transform.position.z);
			}else{
				points[i] = new Vector3(patrolWaypoints[i].transform.position.x, patrolWaypoints[i].transform.position.y, patrolWaypoints[i].transform.position.z);
			}
		}
		
		if(points.Length == 1){
			vectorIndex = 0;
		}
		//Patrol instance END
		
		enemyStats = GetComponent<EnemyStats>();
		enemyAttack = GetComponent<Attack>();
		enemyAttack.setMoveYAxis(moveYAxis);

		if(enemyStats.spriteObject != null){
			enemyAnimator = enemyStats.spriteObject.GetComponent<Animator>();
			hashAnimator = enemyStats.spriteObject.GetComponent<HashAnimatorUnit>();
		}
	}
	
	public virtual void Update () {
		if(enemyAnimator != null){
			enemyAnimator.SetFloat(hashAnimator.velocity, agent.desiredVelocity.magnitude);
		}

		if(!enemyStats.getIsDead() && lastPlayerSeenPosition == getLastPlayerSeenResetPosition() && !enemyAttack.getAttackingTarget()){
			Patrol();
		}
		if(!enemyStats.getIsDead() && lastPlayerSeenPosition != getLastPlayerSeenResetPosition() && !enemyAttack.getAttackingTarget()){
			//Stalk();
			if(getLastCharacterSeen() != null){
				if(!isLastCharacterSeenDead()){
					enemyAttack.AttackTarget(getLastCharacterSeen());
				}else{
					resetLastPlayerSeenPosition();
				}
			}
		}
	}
	
	//########Patrol Movement START
	//###########################################
	void Patrol(){
		patrolling = true;
		stalking = false;
		
		if(!moveYAxis){
			destination = new Vector3(points[vectorIndex].x,transform.position.y,points[vectorIndex].z);
		}else{
			destination = new Vector3(points[vectorIndex].x,points[vectorIndex].y,points[vectorIndex].z);
		}
		
		if( Vector3.Distance(transform.position, destination) > distanceToChangeWaypoint ){
			if(agent.destination != destination){
				agent.SetDestination(destination);

			}
		}else{
			if(timePassedOnWaypoint >= timeOnWaypoint){
				timePassedOnWaypoint = 0f;
				if(vectorIndex < points.Length-1){
					vectorIndex++;
				}else{
					vectorIndex = 0;
				}
			}else{
				timePassedOnWaypoint += Time.deltaTime;
			}
		}
		
	}
	//########Patrol Movement END
	//###########################################
	
	//########Stalk Movement START
	//###########################################
	void Stalk(){
		stalking = true;
		patrolling = false;

		timePassedOnWaypoint = 0f;

		setLastPlayerSeenPosition(getLastCharacterSeen().transform.position);

		if(!moveYAxis){
			destination = new Vector3(lastPlayerSeenPosition.x,transform.position.y,lastPlayerSeenPosition.z);
		}else{
			destination = new Vector3(lastPlayerSeenPosition.x,lastPlayerSeenPosition.y,lastPlayerSeenPosition.z);
		}
		if(Vector3.Distance(transform.position, destination) > agent.stoppingDistance){
			if(agent.destination != destination){
				agent.SetDestination(destination);
			}
		}else if(!getIsSeeingPlayer()){
			resetLastPlayerSeenPosition();
		}
		
	}
	

	//########Stalk Movement END
	//###########################################
	
	
	public bool getPatrolling(){
		return patrolling;
	}
	
	public bool getStalking(){
		return stalking;
	}
	
	public bool getAttacking(){
		return attacking;
	}
	public void setAttacking(bool attacking){
		this.attacking = attacking;
	}
	
	public Vector3 getDestination(){
		return destination;
	}
}
