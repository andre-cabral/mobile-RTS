using UnityEngine;
using System.Collections;

public class HashAnimatorUnit : MonoBehaviour {

	public int velocity;
	public int attacking;
	public int facingDown;
	public int isDead;
	public int deathAnimationNumber;


	void Awake() {
		velocity = Animator.StringToHash("Velocity");
		attacking = Animator.StringToHash("Attacking");
		facingDown = Animator.StringToHash("FacingDown");
		isDead = Animator.StringToHash("IsDead");
		deathAnimationNumber = Animator.StringToHash("DeathAnimationNumber");
	}
}
