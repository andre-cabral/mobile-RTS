using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

	public string characterName = "";
	public GameObject spriteObject;
	float speed;
	float speedReduction = 0;
	float speedReductionDivider = 1;
	public int startingLife = 100;
	public int attack = 1;
	public float attackRange = 0f;
	public float attackDelay = 0.5f;
	public int defense = 1;
	public int life = 999;
	public Lifebar lifebar;
	public GameObject[] objectsToHideOnDeath;
	public float lookAtEnemySpeed = 3f;
	public Transform footPosition;
	bool isDead = false;
	GameObject unitWhoKilledThis;
	const int maxDeathAnimations = 2;
	NavMeshAgent navMeshAgent;
	Collider colliderComponent;
	Animator animator;
	HashAnimatorUnit hashAnimatorUnit;
	
	public virtual void Awake(){
		life = startingLife;
		navMeshAgent = GetComponent<NavMeshAgent>();
		colliderComponent = GetComponent<Collider>();

		speed = navMeshAgent.speed;

		if(spriteObject != null){
			animator = spriteObject.GetComponent<Animator>();
			hashAnimatorUnit = spriteObject.GetComponent<HashAnimatorUnit>();
		}

		if(footPosition == null){
			footPosition = this.transform;
		}
	}

	public void ReduceSpeedWithDivider(float speedToReduce){
		speedReductionDivider += speedToReduce;
		SpeedChange();
	}

	public void AddSpeedWithDivider(float speedToReduce){
		speedReductionDivider -= speedToReduce;
		SpeedChange();
	}

	public void ReduceSpeed(float speedToReduce){
		speedReduction += speedToReduce;
		SpeedChange();
	}

	public void AddSpeed(float speedToAdd){
		speedReduction -= speedToAdd;
		SpeedChange();
	}

	void SpeedChange(){
		if(speedReductionDivider == 0f){
			speedReductionDivider = 1f;
		}
		navMeshAgent.speed = (speed - speedReduction)/speedReductionDivider;
	}
	
	public virtual void takeDamage(GameObject attacker, int attackingValue){
		int damage = attackingValue - ((attackingValue*defense)/100);
		if(Mathf.Max(1, damage) <= life){
			life -= Mathf.Max(1, damage);
		}else{
			life = 0;
		}
		CheckDeath(attacker);
		LifeChanged();
	}

	public void setLife(GameObject changer, int life){
		this.life = life;
		CheckDeath(changer);
		LifeChanged();
	}


	public void recoverLife(int lifeToRecover){
		int lifeTotalRecovered = life + lifeToRecover;
		life = Mathf.Min(startingLife, lifeTotalRecovered);
		LifeChanged();
	}
	
	void CheckDeath(GameObject attacker){
		if(life <= 0){
			unitWhoKilledThis = attacker;
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

		foreach(GameObject objectToHide in objectsToHideOnDeath){
			objectToHide.SetActive(false);
		}

		//play the animation
		if(animator != null){
			animator.SetBool(hashAnimatorUnit.isDead, true);
			animator.SetInteger(hashAnimatorUnit.deathAnimationNumber, Random.Range(0, maxDeathAnimations));
		}
	}

	public GameObject getUnitWhoKilledThis(){
		return unitWhoKilledThis;
	}
}
