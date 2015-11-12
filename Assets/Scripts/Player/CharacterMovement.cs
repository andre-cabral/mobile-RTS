using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Attack))]
public class CharacterMovement : MonoBehaviour {

	NavMeshAgent navMeshAgent;
	bool selected = false;
	public float minimumVelocityToStop = 0.1f;
	public GameObject selectedObjectsToAppearContainer;
	public bool moveYAxis = false;
	private CharacterSelectButton characterButton;
	Animator animator;
	HashAnimatorUnit hashAnimatorUnit;
	Attack playerAttack;
	UnitStats playerStats;
	bool isMoving = false;

	void Awake(){
		navMeshAgent = GetComponent<NavMeshAgent>();
		playerAttack = GetComponent<Attack>();
		playerAttack.setMoveYAxis(moveYAxis);
		playerStats = GetComponent<UnitStats>();

		if(playerStats.spriteObject != null){
			animator = playerStats.spriteObject.GetComponent<Animator>();
			hashAnimatorUnit = playerStats.spriteObject.GetComponent<HashAnimatorUnit>();
		}
	}

	void Update(){
		if(!playerStats.getIsDead()){
			if(!isMoving && navMeshAgent.velocity.magnitude > 0){
				//navMeshAgent.avoidancePriority = 50;
				isMoving = true;
			}
			
			if(isMoving &&  navMeshAgent.velocity.magnitude <= minimumVelocityToStop){
				navMeshAgent.velocity = Vector3.zero;
				navMeshAgent.destination = transform.position;
				//navMeshAgent.avoidancePriority = 0;
				isMoving = false;
			}

			if(animator != null){
				animator.SetFloat(hashAnimatorUnit.velocity, navMeshAgent.velocity.magnitude);
			}
		}
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

	public void CharacterDeathEffects(){
		characterButton.CharacterDeathButtonDeselect();
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

	public void CharacterClickedWithoutDeselectingOthers(){
		characterButton.SelectCharacterWithoutDeselectingOthers();
	}

	public void CharacterClickedDeselectAllButtons(){
		characterButton.DeselectAllCharacters();
	}

	public bool getSelected(){
		return selected;
	}

	public CharacterSelectButton getCharacterButton(){
		return characterButton;
	}
	public void setCharacterButton(CharacterSelectButton characterButton){
		this.characterButton = characterButton;
	}

	public Attack getPlayerAttack(){
		return playerAttack;
	}

	public bool getIsDead(){
		return playerStats.getIsDead();
	}
}
