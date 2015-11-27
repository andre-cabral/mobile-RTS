using UnityEngine;
using System.Collections;

public class DamageTrap : MonoBehaviour {

	public int damage = 50;
	public bool damagePlayers = true;
	public bool damageEnemies = true;
	public bool damageOnlyOnce = true;
	bool damaged = false;
	Animator animator;
	HashAnimatorTrap hashAnimator;

	void Awake(){
		animator = GetComponent<Animator>();
		hashAnimator = GetComponent<HashAnimatorTrap>();
	}

	void OnTriggerEnter(Collider col){
		if(!damageOnlyOnce || !damaged){
			if(col.gameObject.tag == Tags.player){
				animator.SetBool(hashAnimator.damaged, true);
				col.gameObject.GetComponent<UnitStats>().takeDamage(gameObject, damage);
				damaged = true;
			}
			if(col.gameObject.tag == Tags.enemy){
				animator.SetBool(hashAnimator.damaged, true);
				col.gameObject.GetComponent<UnitStats>().takeDamage(gameObject, damage);
				damaged = true;
			}
		}
	}
}
