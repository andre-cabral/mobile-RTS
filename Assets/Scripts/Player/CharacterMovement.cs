using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Attack))]
public class CharacterMovement : MonoBehaviour {

	NavMeshAgent navMeshAgent;
	bool selected = false;
	public GameObject selectedObjectsToAppearContainer;
	public bool moveYAxis = false;
	private CharacterSelectButton characterButton;
	Attack playerAttack;
	UnitStats playerStats;

	void Awake(){
		navMeshAgent = GetComponent<NavMeshAgent>();
		playerAttack = GetComponent<Attack>();
		playerAttack.setMoveYAxis(moveYAxis);
		playerStats = GetComponent<UnitStats>();
	}

	public void goToPoint(Vector3 point){
		if( !playerStats.getIsDead() ){
			Vector3 destination;
			if(!moveYAxis){
				destination = new Vector3(point.x, transform.position.y, point.z);
			}else{
				destination = new Vector3(point.x, point.y, point.z);
			}
			navMeshAgent.SetDestination(destination);

			playerAttack.setAttackingTarget(false);
		}
	}

	public void AttackTarget(GameObject enemy){
		if( !playerStats.getIsDead() ){
			playerAttack.AttackTarget(enemy);
		}
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

	public Attack getPlayerAttack(){
		return playerAttack;
	}
}
