using UnityEngine;
using System.Collections;

public class Prisoner : MonoBehaviour {

	public GameObject characterPrefab;
	public SpriteRenderer SymbolOnPrisonerObject;

	void Awake(){
		SymbolOnPrisonerObject.sprite = characterPrefab.GetComponent<PlayerStats>().characterSymbolWhenPrisoner;
	}

	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag(Tags.player)){
			if(collider.gameObject.GetComponent<PrisonersSaved>().SavePrisoner(characterPrefab)){
				Destroy(gameObject);
			}
		}
	}
}
