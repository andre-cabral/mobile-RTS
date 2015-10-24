using UnityEngine;
using System.Collections;

public class HashAnimatorUnit : MonoBehaviour {

	public int velocity;
	public int attacking;
	public int facingDown;
	public int isDead;


	void Awake() {
		velocity = Animator.StringToHash("Velocity");
		attacking = Animator.StringToHash("Attacking");
		facingDown = Animator.StringToHash("FacingDown");
		isDead = Animator.StringToHash("IsDead");
	}
}
