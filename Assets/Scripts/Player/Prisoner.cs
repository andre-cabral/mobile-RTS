using UnityEngine;
using System.Collections;

public class Prisoner : MonoBehaviour {

	public GameObject characterPrefab;
	public Transform spawnPoint;
	public Collider colliderComponent;
	public SpriteRenderer spriteRenderer;
	public Sprite spriteOpened;
	bool isSaved = false;

	public void SavePrisoner(GameObject savior){
		if(!isSaved){
			if(savior.GetComponent<PrisonersSaved>().SavePrisoner(characterPrefab, spawnPoint.position, spawnPoint.rotation)){
				isSaved = true;
				spriteRenderer.sprite = spriteOpened;
				colliderComponent.enabled = false;
			}
		}
	}
}
