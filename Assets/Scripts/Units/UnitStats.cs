using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

	public string characterName = "";
	public int startingLife = 100;
	public int attack = 1;
	public float attackRange = 0f;
	public float attackDelay = 0.5f;
	public int defense = 1;
	public int life = 999;
	bool isDead = false;
	NavMeshAgent navMeshAgent;
	Collider colliderComponent;
	
	void Awake(){
		life = startingLife;
		navMeshAgent = GetComponent<NavMeshAgent>();
		colliderComponent = GetComponent<Collider>();
	}
	
	public void takeDamage(int attackingValue){
		int damage = attackingValue - defense;
		life -= Mathf.Max(1, damage);
		CheckDeath();
	}

	public void recoverLife(int lifeToRecover){
		int lifeTotalRecovered = life + lifeToRecover;
		life = Mathf.Min(startingLife, lifeTotalRecovered);
	}
	
	void CheckDeath(){
		if(life <= 0){
			DeathEffects();
		}
	}
	
	public bool getIsDead(){
		return isDead;
	}

	public virtual void DeathEffects(){
		isDead = true;
		navMeshAgent.enabled = false;
		colliderComponent.enabled = false;
	}
}
