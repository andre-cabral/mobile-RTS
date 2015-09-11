using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	GameObject unitUsing;
	UnitStats unitStats;
	public float velocity = 10f;
	string targetTag;
	float totalTranslated = 0f;
	bool startProjectile = false;

	void Update () {
		if(startProjectile){
			transform.Translate(Vector3.forward * velocity * Time.deltaTime);
			totalTranslated += (velocity * Time.deltaTime);
			if(totalTranslated > unitStats.attackRange){
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider collider){
		if(startProjectile){
			if(collider.CompareTag(Tags.wall)){
				Destroy(gameObject);
			}
			if(collider.CompareTag(targetTag)){
				collider.gameObject.GetComponent<UnitStats>().takeDamage(unitUsing, unitStats.attack);
				Destroy(gameObject);
			}
		}
	}

	public void setTargetTag(string targetTag){
		this.targetTag = targetTag;
	}

	public void setUnitUsing(GameObject unitUsing){
		this.unitUsing = unitUsing;
	}

	public void setUnitStats(UnitStats unitStats){
		this.unitStats = unitStats;
	}

	public void setStartProjectile(bool startProjectile){
		this.startProjectile = startProjectile;
	}
}