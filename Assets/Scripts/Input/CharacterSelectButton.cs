﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectButton : MonoBehaviour {

	public int characterNumber;
	CharacterMovement character;
	CharactersManager charactersManager;
	public bool selected = false;
	CharacterSelectButton[] characterSelectButtons;
	Button button;
	Sprite buttonSprite;
	Sprite buttonSelectedSprite;
	Sprite buttonDeathSprite;

	ControlsManager controlsManager;

	void Awake(){
		charactersManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<CharactersManager>();
		GameObject[] buttons = GameObject.FindGameObjectsWithTag(Tags.characterSelectButton);
		characterSelectButtons = new CharacterSelectButton[buttons.Length];
		for(int i=0; i<buttons.Length; i++){
			characterSelectButtons[i] = buttons[i].GetComponent<CharacterSelectButton>();
		}

		if(charactersManager.getAllButtons() == null){
			charactersManager.setAllButtons(characterSelectButtons);
		}

		controlsManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ControlsManager>();


		button = GetComponent<Button>();
		
		if(characterNumber != 0){
			button.interactable = false;
		}
	}

	void Start(){
		SetButtonCharacter();
	}

	public void SetButtonCharacter(){
		GameObject[] allCharacters = charactersManager.GetAllCharacters();
		if(characterNumber-1 >= 0 && characterNumber-1 < allCharacters.Length){
			if(allCharacters[characterNumber - 1] != null){
				character = allCharacters[characterNumber - 1].GetComponent<CharacterMovement>();
				if(character.getCharacterButton() == null){
					character.setCharacterButton(this);
					button.interactable = true;

					PlayerStats playerStats  = character.gameObject.GetComponent<PlayerStats>();
					buttonSprite = playerStats.buttonSprite;
					buttonSelectedSprite = playerStats.buttonSelectedSprite;
					buttonDeathSprite = playerStats.buttonDeathSprite;

					button.image.sprite = buttonSprite;
				}
			}
		}
	}

	public void CharacterDeathButtonDeselect(){
		SetButtonSelected(false);
		button.interactable = false;
		button.image.sprite = buttonDeathSprite;
		DeselectCharacter();
	}

	public void SetButtonSelected(bool isSelected){
		if(!controlsManager.getGamePaused() && character != null){
			if(!character.getIsDead()){
				if(isSelected){
					selected = true;
					button.image.sprite = buttonSelectedSprite;
				}else{
					selected = false;
					button.image.sprite = buttonSprite;
				}
			}
		}
	}

	public void SetAllButtonsSelection(bool isSelected){
		for(int i=0; i<characterSelectButtons.Length; i++){
			characterSelectButtons[i].SetButtonSelected(isSelected);
		}
	}

	public void DeselectCharacter(){
		if(!controlsManager.getGamePaused() && character != null){
			SetButtonSelected(false);
			charactersManager.RemoveFromSelected(character);
		}
	}

	public void DeselectAllCharacters(){
		if(!controlsManager.getGamePaused() && character != null){
			SetAllButtonsSelection(false);
			charactersManager.RemoveAllFromSelected();
		}
	}

	public void SelectCharacter(){
		if(!controlsManager.getGamePaused() && character != null){
			SetAllButtonsSelection(false);
			SetButtonSelected(true);
			charactersManager.SelectOneCharacter(character);
		}
	}
	public void SelectCharacterWithoutDeselectingOthers(){
		if(!controlsManager.getGamePaused() && character != null){
			SetButtonSelected(true);
			charactersManager.SelectOneCharacterWithoutDeselectingOthers(character);
		}
	}
	public void SelectAllCharacters(){
		if(!controlsManager.getGamePaused()){
			SetAllButtonsSelection(true);
			charactersManager.SelectAllCharacters();
		}
	}
}
