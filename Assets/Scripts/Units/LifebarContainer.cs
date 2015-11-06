using UnityEngine;
using System.Collections;

public class LifebarContainer : MonoBehaviour {

	Quaternion startRotation;
	public float distanceFromUnit = 1f;
	Transform parentTransform;

	void Awake () {
		parentTransform = gameObject.transform.parent;
		startRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.identity.y, Quaternion.identity.z);
	}
	
	void LateUpdate () {
		transform.rotation = startRotation;
		transform.position = new Vector3(parentTransform.position.x, transform.position.y, parentTransform.position.z + distanceFromUnit);
	}
}
