using UnityEngine;
using System.Collections;

public class HashAnimatorSpawner : MonoBehaviour {

	public int percentagesReached;
	public int spawn;


	void Awake() {
		percentagesReached = Animator.StringToHash("PercentagesReached");
		spawn = Animator.StringToHash("Spawn");
	}
}
