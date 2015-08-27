using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	NavMeshAgent navMeshAgent;
	bool selected = false;
	public GameObject selectedObjectsToAppearContainer;
	private CharacterSelectButton characterButton;

	void Awake(){
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	public void goToPoint(Vector3 point){
		Vector3 destination = new Vector3(point.x, transform.position.y, point.z);
		navMeshAgent.SetDestination(destination);
	}

	public void SetCharacterSelection(bool isSelected){
		selected = isSelected;
		selectedObjectsToAppearContainer.SetActive(isSelected);
	}

	public void CharacterClicked(){
		characterButton.SelectCharacter();
	}

	public bool getSelected(){
		return selected;
	}

	public void setCharacterButton(CharacterSelectButton characterButton){
		this.characterButton = characterButton;
	}
}
