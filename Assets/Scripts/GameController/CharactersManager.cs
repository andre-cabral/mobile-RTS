using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharactersManager : MonoBehaviour {

	public GameObject[] spawnPoints;
	public GameObject[] allCharacters;

	GameObject[] charactersOnMission;

	CharacterMovement[] allCharactersMovements;
	List<CharacterMovement> selectedCharactersMovements = new List<CharacterMovement>();

	void Awake () {
		charactersOnMission = 
			GameObject.FindGameObjectWithTag(Tags.charactersOnMissionList).GetComponent<CharactersOnMissionList>().getCharactersList();

		allCharacters = new GameObject[Mathf.Min(spawnPoints.Length, charactersOnMission.Length)];

		for(int i=0; i<allCharacters.Length; i++){
			allCharacters[i] = 
				(GameObject)Instantiate(charactersOnMission[i], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
		}

		allCharactersMovements = new CharacterMovement[allCharacters.Length];
		for(int i=0; i<allCharacters.Length; i++){
			allCharactersMovements[i] = allCharacters[i].GetComponent<CharacterMovement>();
		}
	}

	public void CheckClickedPoint(Vector3 point){
		// Construct a ray from the current touch coordinates
		Ray ray = Camera.main.ScreenPointToRay(point);
		RaycastHit hit;
		// Move to hit
		if (Physics.Raycast(ray, out hit, 1000f)){
			switch (hit.transform.gameObject.tag){
			case Tags.enemy:
				AttackWithAllSelected(hit.transform.gameObject);
				break;

			case Tags.enemyClickArea:
				AttackWithAllSelected(hit.transform.parent.gameObject);
				break;

			case Tags.player:
				hit.collider.gameObject.GetComponent<CharacterMovement>().CharacterClicked();
				break;		
			
			case Tags.wall:
				break;

			default:
				MoveAllSelected(hit.point);
				break;
			}
		}
	}

	void MoveAllSelected(Vector3 destination){
		foreach(CharacterMovement characterMovement in selectedCharactersMovements){
			characterMovement.goToPoint(destination);
		}
	}

	void AttackWithAllSelected(GameObject enemy){
		foreach(CharacterMovement characterMovement in selectedCharactersMovements){
			characterMovement.AttackTarget(enemy);
		}
	}


	public void SelectOneCharacter(CharacterMovement characterMovement){
		RemoveAllFromSelected();
		AddToSelected(characterMovement);
	}
	public void SelectAllCharacters(){
		RemoveAllFromSelected();
		for(int i=0; i<allCharactersMovements.Length; i++){
			AddToSelected(allCharactersMovements[i]);
		}
	}

	
	public void AddToSelected(CharacterMovement characterMovement){
		selectedCharactersMovements.Add(characterMovement);
		characterMovement.SetCharacterSelection(true);
	}
	public void RemoveFromSelected(CharacterMovement characterMovement){
		selectedCharactersMovements.Remove(characterMovement);
		characterMovement.SetCharacterSelection(false);
	}
	public void RemoveAllFromSelected(){
		foreach(CharacterMovement characterMovement in selectedCharactersMovements){
			characterMovement.SetCharacterSelection(false);
		}
		selectedCharactersMovements.Clear();
	}

	public GameObject[] GetAllCharacters(){
		return allCharacters;
	}
}
