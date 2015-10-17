using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(Attack))]
public class StandAndAttack : EnemyMovement {
	
	public bool moveYAxis = false;
	
	private Animator enemyAnimator;
	private HashAnimatorUnit hashAnimator;
	
	private EnemyStats enemyStats;

	Attack enemyAttack;
	
	private bool attacking = false;
	
	public virtual void Awake () {		
		enemyStats = GetComponent<EnemyStats>();
		enemyAttack = GetComponent<Attack>();
		enemyAttack.setMoveYAxis(moveYAxis);
		enemyAttack.setMoveToAttack(false);

		if(enemyStats.spriteObject != null){
			enemyAnimator = enemyStats.spriteObject.GetComponent<Animator>();
			hashAnimator = enemyStats.spriteObject.GetComponent<HashAnimatorUnit>();
		}
	}
	
	public virtual void Update () {
		if(!enemyStats.getIsDead() && lastPlayerSeenPosition != getLastPlayerSeenResetPosition() && !enemyAttack.getAttackingTarget()){
			if(getLastCharacterSeen() != null){
				if(!isLastCharacterSeenDead()){
					enemyAttack.AttackTarget(getLastCharacterSeen());
				}else{
					resetLastPlayerSeenPosition();
				}
			}
		}
		if(getLastCharacterSeen() != null){
			if(Vector3.Distance(transform.position, getLastCharacterSeen().transform.position) > enemyStats.attackRange){
				enemyAttack.setAttackingTarget(false);
				resetLastPlayerSeenPosition();
			}
		}
	}

	public bool getAttacking(){
		return attacking;
	}
	public void setAttacking(bool attacking){
		this.attacking = attacking;
	}
}
