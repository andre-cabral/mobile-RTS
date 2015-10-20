using UnityEngine;
using System.Collections;

public class SymbolOnPrisonerObject : MonoBehaviour {

	Quaternion startRotation;
	SpriteRenderer spriteRenderer;
	public float distanceFromPrisoner = 1f;
	Transform parentTransform;
	
	void Awake () {
		parentTransform = gameObject.transform.parent;
		startRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.identity.y, Quaternion.identity.z);
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		transform.rotation = startRotation;
		transform.position = new Vector3(parentTransform.position.x, transform.position.y, parentTransform.position.z + distanceFromPrisoner);
	}
}
