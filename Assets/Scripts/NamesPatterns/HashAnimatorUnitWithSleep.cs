using UnityEngine;
using System.Collections;

public class HashAnimatorUnitWithSleep : MonoBehaviour {

	public int sleeping;


	void Awake() {
		sleeping = Animator.StringToHash("Sleeping");
	}
}
