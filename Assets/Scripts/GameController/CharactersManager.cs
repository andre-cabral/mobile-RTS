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

		// Move to hit
		RaycastHit[] allHits = Physics.RaycastAll(ray);
		List<RaycastHit> closestHit = new List<RaycastHit>();

		bool containsPriority = false;

		if(allHits.Length > 0){
			for(int i=0; i<allHits.Length;i++){
				if( i==0 ){
					if(!closestHit.Contains(allHits[i])){
						closestHit.Add(allHits[i]);
						containsPriority = CheckRaycastHitPriority(allHits[i]) || containsPriority;
					}
				}

				if( CheckRaycastHitPriority(allHits[i]) || !containsPriority ){
					if(!closestHit.Contains(allHits[i])){
						closestHit.Add(allHits[i]);
						containsPriority = CheckRaycastHitPriority(allHits[i]) || containsPriority;
					}
				}
			}

			if(!containsPriority && !HasElementWithTag(closestHit, Tags.wall)){
				MoveAllSelected(closestHit[0].point);
			}

			if(containsPriority && HasElementWithTag(closestHit, Tags.enemyClickArea)){
				closestHit = RemoveElementsWithoutTag(closestHit, Tags.enemyClickArea);
				if (closestHit.Count > 0){
					for(int i=0; i<closestHit.Count; i++){
						AttackWithAllSelected(closestHit[i].collider.transform.parent.gameObject);
					}
				}
			}

			if(containsPriority && HasElementWithTag(closestHit, Tags.playerClickArea)){
				closestHit = RemoveElementsWithoutTag(closestHit, Tags.playerClickArea);
				if (closestHit.Count > 0){

					closestHit[0].collider.transform.parent.GetComponent<CharacterMovement>().CharacterClickedDeselectAllButtons();
					RemoveAllFromSelected();

					for(int i=0; i<closestHit.Count; i++){
						closestHit[i].collider.transform.parent.GetComponent<CharacterMovement>().CharacterClickedWithoutDeselectingOthers();
					}
				}
			}
		}


		//OLD CODE WITH ONLY ONE RAYCASTHIT
		/*
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f)){
			//the hit.collider gets the object directly clicked. If you use only the hit, you will get the parent
			switch (hit.collider.transform.gameObject.tag){
			
			case Tags.enemyClickArea:
				AttackWithAllSelected(hit.collider.transform.parent.gameObject);
				break;

			case Tags.playerClickArea:
				hit.collider.gameObject.GetComponent<CharacterMovement>().CharacterClicked();
				break;		
			
			case Tags.wall:
				break;

			default:
				MoveAllSelected(hit.point);
				break;
			}
		}
		*/
	}

	bool CheckRaycastHitPriority(RaycastHit hitToCheck){
		return 
			hitToCheck.collider.transform.gameObject.tag == Tags.enemyClickArea || 
			hitToCheck.collider.transform.gameObject.tag == Tags.playerClickArea;
	}

	bool HasElementWithTag(List<RaycastHit> list, string tag){
		foreach(RaycastHit hit in list){
			if(hit.collider.transform.gameObject.tag == tag){
				return true;
			}
		}
		return false;
	}

	List<RaycastHit> RemoveElementsWithoutTag(List<RaycastHit> list, string tag){
		List<RaycastHit> listWithTag = new List<RaycastHit>();
		foreach(RaycastHit hit in list){
			if(hit.collider.transform.gameObject.tag == tag){
				listWithTag.Add(hit);
			}
		}
		return listWithTag;
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
	public void SelectOneCharacterWithoutDeselectingOthers(CharacterMovement characterMovement){
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
