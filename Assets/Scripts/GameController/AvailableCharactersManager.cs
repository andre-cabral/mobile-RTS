using UnityEngine;
using System.Collections;

public class AvailableCharactersManager : MonoBehaviour {

	AvailableCharactersList availableCharactersList;
	public GameObject characterAvailablePrefab;
	public GameObject buttonsContainer;
	AvailableCharacterTemplate[] allCharactersAvailableTemplates;

	void Awake(){
		availableCharactersList = GameObject.FindGameObjectWithTag(Tags.availableCharactersList).GetComponent<AvailableCharactersList>();

		GameObject[] allCharactersPrefabs = availableCharactersList.getAllCharactersPrefabs();

		RectTransform prefabButtonAddRect = characterAvailablePrefab.GetComponent<RectTransform>();
		float sizeY = prefabButtonAddRect.anchorMax.y - prefabButtonAddRect.anchorMin.y;

		allCharactersAvailableTemplates = new AvailableCharacterTemplate[allCharactersPrefabs.Length];

		for(int i=0; i<allCharactersPrefabs.Length; i++){
			GameObject button = (GameObject)Instantiate(characterAvailablePrefab);

			button.GetComponent<RectTransform>().anchorMax = new Vector2(prefabButtonAddRect.anchorMax.x, prefabButtonAddRect.anchorMax.y - (sizeY*i));
			button.GetComponent<RectTransform>().anchorMin = new Vector2(prefabButtonAddRect.anchorMin.x, prefabButtonAddRect.anchorMin.y - (sizeY*i));

			button.transform.SetParent(buttonsContainer.transform, false);


			AvailableCharacterTemplate availableCharacterTemplate = button.GetComponent<AvailableCharacterTemplate>();

			availableCharacterTemplate.setAvailableCharactersManager(this);
			availableCharacterTemplate.setCharacterPrefab(allCharactersPrefabs[i]);

			availableCharacterTemplate.changeText();

			allCharactersAvailableTemplates[i] = availableCharacterTemplate;
		}
	}

	public void ChangeAllTextsOnTemplates(){
		foreach(AvailableCharacterTemplate template in allCharactersAvailableTemplates){
			template.changeText();
		}
	}

	public bool AddCharacterToMission(PlayerStats playerStats){
		bool returnedValue = availableCharactersList.removeOneFromCharacterAvailableQuantity(playerStats);
		ChangeAllTextsOnTemplates();
		return returnedValue;
	}

	public void RemoveCharacterFromMission(PlayerStats playerStats){
		availableCharactersList.AddOneToCharacterAvailableQuantity(playerStats);
		ChangeAllTextsOnTemplates();
	}
}
