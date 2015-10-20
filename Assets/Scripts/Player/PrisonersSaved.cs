using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrisonersSaved : MonoBehaviour {

	public SpriteRenderer[] prisonerUIs;
	List<GameObject> prisonersSaved = new List<GameObject>();

	public bool SavePrisoner(GameObject prisoner){
		if(prisonersSaved.Count < prisonerUIs.Length){
			prisonersSaved.Add(prisoner);
			prisonerUIs[prisonersSaved.Count - 1].sprite = prisoner.GetComponent<PlayerStats>().characterSymbolWhenPrisoner;
			return true;
		}else{
			return false;
		}
	}

	public List<GameObject> getPrisonersSaved(){
		return prisonersSaved;
	}
}
