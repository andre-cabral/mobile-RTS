using UnityEngine;
using System.Collections;

public class Prisoner : MonoBehaviour {

	public GameObject characterPrefab;
	public SpriteRenderer SymbolOnPrisonerObject;
	bool isSaved = false;

	void Awake(){
		SymbolOnPrisonerObject.sprite = characterPrefab.GetComponent<PlayerStats>().characterSymbolWhenPrisoner;
	}

	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag(Tags.player)){
			if(!isSaved){
				if(collider.gameObject.GetComponent<PrisonersSaved>().SavePrisoner(characterPrefab, transform.position, transform.rotation)){
					isSaved = true;
					Destroy(gameObject);
				}
			}
		}
	}
}
