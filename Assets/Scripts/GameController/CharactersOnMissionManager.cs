using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharactersOnMissionManager : MonoBehaviour {
	List<GameObject> charactersPrefabs = new List<GameObject>();
	public GameObject characterOnMissionPrefab;
	public GameObject buttonsContainer;
	public Toggle hardCoreModeToggle;
	public int charactersOnMission = 4;
	CharacterOnMissionTemplate[] allCharacterOnMissionTemplates;
	CharactersOnMissionList charactersOnMissionList;


	void Awake(){
		charactersOnMissionList = GameObject.FindGameObjectWithTag(Tags.charactersOnMissionList).GetComponent<CharactersOnMissionList>();
		allCharacterOnMissionTemplates = new CharacterOnMissionTemplate[charactersOnMission];

		RectTransform prefabButtonAddRect = characterOnMissionPrefab.GetComponent<RectTransform>();
		float sizeY = prefabButtonAddRect.anchorMax.y - prefabButtonAddRect.anchorMin.y;
		
		for(int i=0; i<charactersOnMission; i++){
			GameObject button = (GameObject)Instantiate(characterOnMissionPrefab);
			
			button.GetComponent<RectTransform>().anchorMax = new Vector2(prefabButtonAddRect.anchorMax.x, prefabButtonAddRect.anchorMax.y - (sizeY*i));
			button.GetComponent<RectTransform>().anchorMin = new Vector2(prefabButtonAddRect.anchorMin.x, prefabButtonAddRect.anchorMin.y - (sizeY*i));
			
			button.transform.SetParent(buttonsContainer.transform, false);
			
			
			CharacterOnMissionTemplate characterOnMissionTemplate = button.GetComponent<CharacterOnMissionTemplate>();			

			allCharacterOnMissionTemplates[i] = characterOnMissionTemplate;
		}
	}

	public bool HasEmptyCharacter(){
		foreach(CharacterOnMissionTemplate character in allCharacterOnMissionTemplates){
			if(character.getCharacterPrefab() == null){
				return true;
			}
		}
		return false;
	}

	public void AddCharacter(GameObject prefab){
		foreach(CharacterOnMissionTemplate character in allCharacterOnMissionTemplates){
			if(character.getCharacterPrefab() == null){
				character.setCharacterPrefab(prefab);
				character.changeText();
				break;
			}
		}
	}

	public bool clickStartMission(){
		List<GameObject> characters = new List<GameObject>();
		foreach(CharacterOnMissionTemplate template in allCharacterOnMissionTemplates){
			if(template.getCharacterPrefab() != null){
				characters.Add(template.getCharacterPrefab());
			}
		}

		if(characters.Count > 0){
			GameObject[] listOfCharacters = new GameObject[Mathf.Min(characters.Count, charactersOnMission)];
			for(int i=0; i < Mathf.Min(characters.Count, charactersOnMission); i++){
				listOfCharacters[i] = characters[i];
			}
			charactersOnMissionList.setCharactersList(listOfCharacters);

			charactersOnMissionList.setHardCoreMode(hardCoreModeToggle.isOn);

			return true;

		}else{
			return false;
		}
	}
}