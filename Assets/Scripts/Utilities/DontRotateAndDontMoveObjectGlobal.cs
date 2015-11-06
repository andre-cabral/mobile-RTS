using UnityEngine;
using System.Collections;

public class DontRotateAndDontMoveObjectGlobal : MonoBehaviour {
	
	Quaternion startRotation;
	Vector3 startPosition;
	
	void Awake () {
		startRotation = transform.rotation;
		startPosition = transform.position;
	}
	
	void LateUpdate () {
		transform.rotation = startRotation;
		transform.position = startPosition;
	}
}
