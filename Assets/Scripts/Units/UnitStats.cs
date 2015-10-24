using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

	public string characterName = "";
	public GameObject spriteObject;
	public int startingLife = 100;
	public int attack = 1;
	public float attackRange = 0f;
	public float attackDelay = 0.5f;
	public int defense = 1;
	public int life = 999;
	public Lifebar lifebar;
	public float lookAtEnemySpeed = 3f;
	bool isDead = false;
	NavMeshAgent navMeshAgent;
	Collider colliderComponent;
	Animator animator;
	HashAnimatorUnit hashAnimatorUnit;
	
	public virtual void Awake(){
		life = startingLife;
		navMeshAgent = GetComponent<NavMeshAgent>();
		colliderComponent = GetComponent<Collider>();

		if(spriteObject != null){
			animator = spriteObject.GetComponent<Animator>();
			hashAnimatorUnit = spriteObject.GetComponent<HashAnimatorUnit>();
		}
	}
	
	public virtual void takeDamage(GameObject attacker, int attackingValue){
		int damage = attackingValue - ((attackingValue*defense)/100);
		if(Mathf.Max(1, damage) <= life){
			life -= Mathf.Max(1, damage);
		}else{
			life = 0;
		}
		CheckDeath();
		LifeChanged();
	}

	public void recoverLife(int lifeToRecover){
		int lifeTotalRecovered = life + lifeToRecover;
		life = Mathf.Min(startingLife, lifeTotalRecovered);
		LifeChanged();
	}
	
	void CheckDeath(){
		if(life <= 0){
			DeathEffects();
		}
	}

	void LifeChanged(){
		float lifeFloat = life;
		float startingLifeFloat = startingLife;
		lifebar.changePercentage(lifeFloat/startingLifeFloat);
	}
	
	public bool getIsDead(){
		return isDead;
	}

	public virtual void DeathEffects(){
		isDead = true;
		navMeshAgent.enabled = false;
		colliderComponent.enabled = false;

		//play the animation
		if(animator != null){
			animator.SetBool(hashAnimatorUnit.isDead, true);
		}
	}
}
