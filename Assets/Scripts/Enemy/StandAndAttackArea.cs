using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(Attack))]
public class StandAndAttackArea : EnemyMovement {
	
	public bool moveYAxis = false;
	public bool isUnitWithSleep = true;
	
	private Animator enemyAnimator;
	private HashAnimatorUnit hashAnimator;
	private HashAnimatorUnitWithSleep hashAnimatorWithSleep;
	
	private EnemyStats enemyStats;

	Attack enemyAttack;
	
	private bool attacking = false;

	int charactersOnArea = 0;

	public virtual void Awake () {		
		enemyStats = GetComponent<EnemyStats>();
		enemyAttack = GetComponent<Attack>();
		enemyAttack.setMoveYAxis(moveYAxis);
		enemyAttack.setMoveToAttack(false);

		if(enemyStats.spriteObject != null){
			enemyAnimator = enemyStats.spriteObject.GetComponent<Animator>();
			hashAnimator = enemyStats.spriteObject.GetComponent<HashAnimatorUnit>();
			if(isUnitWithSleep){
				hashAnimatorWithSleep = enemyStats.spriteObject.GetComponent<HashAnimatorUnitWithSleep>();
			}
		}
	}
	
	public bool getAttacking(){
		return attacking;
	}
	public void setAttacking(bool attacking){
		this.attacking = attacking;
	}

	public void addTarget(GameObject newTarget){
		enemyAttack.addTargetOnArea(newTarget);
		charactersOnArea = enemyAttack.numberOfTargetsOnArea();
		if(isUnitWithSleep){
			enemyAnimator.SetBool(hashAnimatorWithSleep.sleeping, false);
		}else{
			enemyAttack.setAttackingTarget(true);
		}
	}

	public void removeTarget(GameObject newTarget){
		enemyAttack.removeTargetOnArea(newTarget);
		charactersOnArea = enemyAttack.numberOfTargetsOnArea();
		if(charactersOnArea <= 0){
			enemyAttack.setAttackingTarget(false);
			if(isUnitWithSleep){
				enemyAnimator.SetBool(hashAnimatorWithSleep.sleeping, true);
			}
		}
	}

	public void endSleeping(){
		enemyAttack.setAttackingTarget(true);
	}
}
