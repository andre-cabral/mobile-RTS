using UnityEngine;
using System.Collections;

public class AvailableCharactersList : MonoBehaviour {

	public static AvailableCharactersList control;
	public GameObject[] allCharactersPrefabs;
	PlayerStats[] allPlayerStats;

	void Awake(){
		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
			allPlayerStats = new PlayerStats[allCharactersPrefabs.Length];
			for(int i=0; i<allCharactersPrefabs.Length; i++){
				allPlayerStats[i] = allCharactersPrefabs[i].GetComponent<PlayerStats>();
			}
		}else if(control != this){
			Destroy (gameObject);
		}
	}
	
	public bool removeOneFromCharacterAvailableQuantity(PlayerStats playerStats){
		for(int i=0; i<allPlayerStats.Length; i++){
			if(playerStats.characterCode == allPlayerStats[i].characterCode){
				return allPlayerStats[i].removeOneFromCharacterAvailableQuantity();
			}
		}
		return false;
	}
	
	public void AddOneToCharacterAvailableQuantity(PlayerStats playerStats){
		for(int i=0; i<allPlayerStats.Length; i++){
			if(playerStats.characterCode == allPlayerStats[i].characterCode){
				allPlayerStats[i].addCharacterAvailableQuantity(1);
			}
		}
	}

	public GameObject[] getAllCharactersPrefabs(){
		return allCharactersPrefabs;
	}
}
