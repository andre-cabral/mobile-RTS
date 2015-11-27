using UnityEngine;
using System.Collections;

public class HashAnimatorTrap : MonoBehaviour {

	public int damaged;	
	
	void Awake() {
		damaged = Animator.StringToHash("Damaged");
	}
}
