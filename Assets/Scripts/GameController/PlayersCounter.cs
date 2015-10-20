using UnityEngine;
using System.Collections;

public class PlayersCounter : MonoBehaviour {

	int totalCharacters = 0;
	int charactersAlive = 0;
	int charactersDead = 0;
	StageEnd stageEnd;

	void Awake(){
		stageEnd = GetComponent<StageEnd>();
	}
	
	public void characterSpawned(){
		totalCharacters++;
		charactersAlive++;
	}
	
	public void characterKilled(){
		charactersAlive--;
		charactersDead++;
		if(areAllCharactersDead()){
			stageEnd.Lose();
		}
	}
	
	public bool areAllCharactersDead(){
		return charactersAlive <= 0;
	}
}
