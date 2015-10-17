using UnityEngine;
using System.Collections;

public enum FourDiagonalDirectionNames {
	upRight,
	downRight,
	upLeft,
	downLeft
}

public class FourDiagonalAnimation : MonoBehaviour {

	public GameObject objectToGetRotation;
	Animator animator;
	HashAnimatorUnit hashAnimatorUnit;
	bool isFacingRight = true;
	FourDiagonalDirectionNames direction;


	void Awake(){
		animator = GetComponent<Animator>();
		hashAnimatorUnit = GetComponent<HashAnimatorUnit>();
	}
	
	void Update () {
		CheckDirection();
	}

	void Flip(bool willFaceRight){
		if(isFacingRight != willFaceRight){
			transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
			isFacingRight = willFaceRight;
		}
	}

	void CheckDirection(){
		float yRotation = objectToGetRotation.transform.rotation.eulerAngles.y;

		if(yRotation >= 0 && yRotation< 90){
			direction = FourDiagonalDirectionNames.upRight;
			animator.SetBool(hashAnimatorUnit.facingDown, false);
			Flip(true);
		}
		if(yRotation >= 90 && yRotation<= 180){
			direction = FourDiagonalDirectionNames.downRight;
			animator.SetBool(hashAnimatorUnit.facingDown, true);
			Flip(true);
		}
		if(yRotation > 180 && yRotation<= 270){
			direction = FourDiagonalDirectionNames.downLeft;
			animator.SetBool(hashAnimatorUnit.facingDown, true);
			Flip(false);
		}
		if(yRotation > 270 && yRotation< 360){
			direction = FourDiagonalDirectionNames.upLeft;
			animator.SetBool(hashAnimatorUnit.facingDown, false);
			Flip(false);
		}
	}
}
