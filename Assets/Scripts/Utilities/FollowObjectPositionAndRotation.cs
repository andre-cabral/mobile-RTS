using UnityEngine;
using System.Collections;

public class FollowObjectPositionAndRotation : MonoBehaviour {

	public Transform objectToFollow;
		
	void Update () {
		transform.position = objectToFollow.position;
		transform.rotation = objectToFollow.rotation;
	}
}
