using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitStats))]
public class Attack : MonoBehaviour {

	bool moveToAttack = true;
	public bool isAreaAttack = false;
	public GameObject projectile;
	public bool attackingTarget = false;
	public Transform projectileSpawnPoint;
	GameObject targetToAttack;
	UnitStats targetStats;
	UnitStats unitStats;
	Animator animator;
	HashAnimatorUnit hashAnimatorUnit;
	float attackDelayCount = 0f;
	bool isRanged = false;
	string targetTag;
	NavMeshAgent navMeshAgent;
	bool moveYAxis = false;
	List<GameObject> targetsOnArea = new List<GameObject>();

	void Awake(){
		navMeshAgent = GetComponent<NavMeshAgent>();
		unitStats = GetComponent<UnitStats>();

		attackDelayCount = unitStats.attackDelay;

		isRanged = unitStats.attackRange > 0f;

		if(gameObject.tag == Tags.player){
			targetTag = Tags.enemy;
		}
		if(gameObject.tag == Tags.enemy){
			targetTag = Tags.player;
		}

		if(projectileSpawnPoint == null){
			projectileSpawnPoint = transform;
		}

		if(unitStats.spriteObject != null){
			animator = unitStats.spriteObject.GetComponent<Animator>();
			hashAnimatorUnit = unitStats.spriteObject.GetComponent<HashAnimatorUnit>();
		}
	}

	void Update(){
		if(!isAreaAttack){
			if( attackingTarget && !unitStats.getIsDead() ){

				if(!targetStats.getIsDead()){
					AttackTargetFollow();
				}
				if( attackDelayCount < unitStats.attackDelay && !unitStats.getIsDead() ){
					attackDelayCount += Time.deltaTime;
				}

				if(targetStats != null){
					if(targetStats.getIsDead() && attackingTarget){
						attackingTarget = false;
					}
				}
			}
		}else{
			if( attackingTarget && !unitStats.getIsDead() ){
				if(targetsOnArea.Count > 0){
					AreaAttackWithoutFollow();
				}

				if( attackDelayCount < unitStats.attackDelay && targetsOnArea.Count > 0 ){
					attackDelayCount += Time.deltaTime;
				}
				if(targetsOnArea.Count <= 0 && attackingTarget){
					attackingTarget = false;
				}
			}
		}
	}

	void AttackTargetFollow(){
		if( (isRanged && Vector3.Distance(transform.position, targetToAttack.transform.position) > unitStats.attackRange)
		   || (!isRanged && !targetToAttack.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds)) ){
			FollowTargetPosition(targetToAttack.transform.position);
		}else{
			if(navMeshAgent.isActiveAndEnabled){
				navMeshAgent.velocity = Vector3.zero;
				navMeshAgent.SetDestination(transform.position);
			}

			if(transform.position != targetToAttack.transform.position){
				FlipWithSpeed(targetToAttack.transform.position, unitStats.lookAtEnemySpeed);
			}
			//transform.LookAt(targetToAttack.transform.position);
			if(attackDelayCount >= unitStats.attackDelay){
				if(!isRanged){

					MeleeAttack();
					attackDelayCount = 0f;
				}else{
					RangedAttack();
					attackDelayCount = 0f;
				}
			}
		}

	}

	void AreaAttackWithoutFollow(){
		if(attackDelayCount >= unitStats.attackDelay){

			AreaAttack();
			attackDelayCount = 0f;
		}
	}

	void MeleeAttack(){
		//play the animation
		if(animator != null){
			animator.SetBool(hashAnimatorUnit.attacking, true);
		}

		//the animation will call the next function, CauseDamageOnTarget (see if this will be really used)
		CauseDamageOnTarget();

		//end the animation bool
		//used instead of trigger because in the future can be used as bool to set the point in the animation
		//whete the damage will be caused.
		/*
		if(animator != null){
			animator.SetBool(hashAnimatorUnit.attacking, false);
		}
		*/

	}

	void RangedAttack(){
		//play the animation
		if(animator != null){
			animator.SetBool(hashAnimatorUnit.attacking, true);
		}

		//Throw the projectile code
		if(projectile != null){
			GameObject newProjectile = (GameObject)Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
			newProjectile.transform.LookAt(targetToAttack.transform.position);

			Projectile projectileScript = newProjectile.GetComponent<Projectile>();
			projectileScript.setTargetTag(targetTag);
			projectileScript.setUnitUsing(gameObject);
			projectileScript.setUnitStats(unitStats);

			projectileScript.setStartProjectile(true);
		}
	}

	void AreaAttack(){
		if(targetsOnArea.Count > 0){
		
			List<GameObject> newTargetsOnArea = new List<GameObject>();
			foreach(GameObject specificTarget in targetsOnArea){
				if(specificTarget.tag == targetTag){
					newTargetsOnArea.Add(specificTarget);
				}
			}
			targetsOnArea = newTargetsOnArea;
		


			foreach(GameObject specificTarget in targetsOnArea){
				//intantiate a projectile without collisions on targets
				if(projectile != null){
					GameObject newProjectile = (GameObject)Instantiate(projectile, specificTarget.transform.position, Quaternion.identity);
				}

				CauseDamageOnSpecificTarget(specificTarget);
			}

			//play the animation if there is at least one target
			if(animator != null){
				animator.SetBool(hashAnimatorUnit.attacking, true);
			}
		}

	}

	public void CauseDamageOnTarget(){
		targetToAttack.GetComponent<UnitStats>().takeDamage(gameObject, unitStats.attack);
	}

	public void CauseDamageOnSpecificTarget(GameObject specificTarget){
		if(specificTarget.tag == targetTag){
			specificTarget.GetComponent<UnitStats>().takeDamage(gameObject, unitStats.attack);
		}
	}


	public void FollowTargetPosition(Vector3 point){
		if(moveToAttack){
			Vector3 destination;
			if(!moveYAxis){
				destination = new Vector3(point.x, transform.position.y, point.z);
			}else{
				destination = new Vector3(point.x, point.y, point.z);
			}
			navMeshAgent.SetDestination(destination);
		}
	}

	public void AttackTarget(GameObject target){
		if(target.tag == targetTag){
			attackingTarget = true;
			targetToAttack = target;
			targetStats = target.GetComponent<UnitStats>();
		}
	}

	public bool FlipWithSpeed(Vector3 end, float speed){

		Quaternion finalRotation = Quaternion.LookRotation(end - transform.position);
		if(finalRotation != transform.rotation){
			//transform.rotation = finalRotation;
			transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * speed);
		}
		return finalRotation == transform.rotation;
	}

	public void setAttackingTarget(bool attackingTarget){
		this.attackingTarget = attackingTarget;
	}

	public bool getAttackingTarget(){
		return attackingTarget;
	}

	public void setMoveYAxis(bool moveYAxis){
		this.moveYAxis = moveYAxis;
	}

	public void setMoveToAttack(bool moveToAttack){
		this.moveToAttack = moveToAttack;
	}

	public bool getMoveToAttack(){
		return moveToAttack;
	}

	public void addTargetOnArea(GameObject newTarget){
		if(newTarget.tag == targetTag){
			targetsOnArea.Add(newTarget);
		}
	}

	public void removeTargetOnArea(GameObject newTarget){
		targetsOnArea.Remove(newTarget);
		if(targetsOnArea.Count <= 0){
			attackingTarget = false;
		}
	}

	public void clearTargetsOnArea(){
		targetsOnArea.Clear();
	}

	public int numberOfTargetsOnArea(){
		return targetsOnArea.Count;
	}
}
