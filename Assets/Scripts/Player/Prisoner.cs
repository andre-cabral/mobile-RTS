using UnityEngine;
using System.Collections;

public class Prisoner : MonoBehaviour {

	public GameObject characterPrefab;
	public Transform spawnPoint;
	public Collider colliderComponent;
	public SpriteRenderer spriteRenderer;
	public Sprite spriteOpened;
	bool isSaved = false;

	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag(Tags.player)){
			if(!isSaved){
				if(collider.gameObject.GetComponent<PrisonersSaved>().SavePrisoner(characterPrefab, spawnPoint.position, spawnPoint.rotation)){
					isSaved = true;
					spriteRenderer.sprite = spriteOpened;
					colliderComponent.enabled = false;
				}
			}
		}
	}
}
