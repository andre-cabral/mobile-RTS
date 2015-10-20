using UnityEngine;
using System.Collections;

public class DontRotateObjectXAxisUsingLocalRotation : MonoBehaviour {
	
	float startRotationX;
	
	void Awake () {
		startRotationX = transform.localRotation.eulerAngles.x;
	}
	
	void Update () {
		transform.rotation = Quaternion.Euler(new Vector3(startRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
	}
}
