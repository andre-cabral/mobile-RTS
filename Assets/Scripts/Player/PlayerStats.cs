using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats {

	public override void DeathEffects(){
		base.DeathEffects();
		gameObject.tag = Tags.playerDead;
	}
}
