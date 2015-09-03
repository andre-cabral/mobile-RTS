using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float velocity = 10f;
	string targetTag;
	int attack;

	void Update () {
		transform.Translate(Vector3.forward * velocity * Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag(Tags.wall)){
			Destroy(gameObject);
		}
		if(collider.CompareTag(targetTag)){
			collider.gameObject.GetComponent<UnitStats>().takeDamage(attack);
			Destroy(gameObject);
		}
	}

	public void setTargetTag(string targetTag){
		this.targetTag = targetTag;
	}

	public void setAttack(int attack){
		this.attack = attack;
	}
}
