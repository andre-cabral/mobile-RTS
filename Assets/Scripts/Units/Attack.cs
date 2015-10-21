using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitStats))]
public class Attack : MonoBehaviour {

	bool moveToAttack = true;
	public GameObject projectile;
	public bool attackingTarget = false;
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

		if(unitStats.spriteObject != null){
			animator = unitStats.spriteObject.GetComponent<Animator>();
			hashAnimatorUnit = unitStats.spriteObject.GetComponent<HashAnimatorUnit>();
		}
	}

	void Update(){
		if( attackingTarget && !unitStats.getIsDead() ){
			if(!targetStats.getIsDead()){
				AttackTargetFollow();
			}
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

	void AttackTargetFollow(){
		if( (isRanged && Vector3.Distance(transform.position, targetToAttack.transform.position) > unitStats.attackRange)
		   || (!isRanged && !targetToAttack.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds)) ){
			FollowTargetPosition(targetToAttack.transform.position);
		}else{
			if(navMeshAgent.isActiveAndEnabled){
				navMeshAgent.velocity = Vector3.zero;
				navMeshAgent.SetDestination(transform.position);
			}

			FlipWithSpeed(targetToAttack.transform.position, unitStats.lookAtEnemySpeed);
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
		//Throw the projectile code
		if(projectile != null){
			GameObject newProjectile = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
			newProjectile.transform.LookAt(targetToAttack.transform.position);

			Projectile projectileScript = newProjectile.GetComponent<Projectile>();
			projectileScript.setTargetTag(targetTag);
			projectileScript.setUnitUsing(gameObject);
			projectileScript.setUnitStats(unitStats);

			projectileScript.setStartProjectile(true);
		}
	}

	public void CauseDamageOnTarget(){
		targetToAttack.GetComponent<UnitStats>().takeDamage(gameObject, unitStats.attack);
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
		transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * speed);
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
}
