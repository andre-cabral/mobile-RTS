using UnityEngine;
using System.Collections;

public class RandomAnimator : MonoBehaviour {

	public Animator animatorComponent;
	public RuntimeAnimatorController[] animatorsToRandomize;

	void Awake () {
		animatorComponent.runtimeAnimatorController = animatorsToRandomize[Random.Range(0, animatorsToRandomize.Length)];
	}
}
