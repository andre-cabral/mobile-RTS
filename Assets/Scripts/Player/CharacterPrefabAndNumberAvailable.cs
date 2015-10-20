using UnityEngine;
using System.Collections;

public class CharacterPrefabAndNumberAvailable {
	GameObject prefab;
	int availableCharacterQuantity;

	public CharacterPrefabAndNumberAvailable(GameObject prefab, int availableCharacterQuantity){
		this.prefab = prefab;
		this.availableCharacterQuantity = availableCharacterQuantity;
	}
}
