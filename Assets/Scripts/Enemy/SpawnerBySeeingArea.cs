using UnityEngine;
using System.Collections;

public class SpawnerBySeeingArea : Spawner {

	void OnTriggerEnter (Collider col) {
		if(col.CompareTag(Tags.player)){
			setStartSpawnTimer(true);
		}
	}
}
