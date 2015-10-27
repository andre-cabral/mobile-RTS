using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectButton : MonoBehaviour {

	public int characterNumber;
	CharacterMovement character;
	CharactersManager charactersManager;
	public bool selected = false;
	CharacterSelectButton[] characterSelectButtons;
	Button button;
	ColorBlock normal = new ColorBlock();
	ColorBlock pressed = new ColorBlock();

	ControlsManager controlsManager;

	void Awake(){
		charactersManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<CharactersManager>();
		GameObject[] buttons = GameObject.FindGameObjectsWithTag(Tags.characterSelectButton);
		characterSelectButtons = new CharacterSelectButton[buttons.Length];
		for(int i=0; i<buttons.Length; i++){
			characterSelectButtons[i] = buttons[i].GetComponent<CharacterSelectButton>();
		}

		controlsManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ControlsManager>();

		button = GetComponent<Button>();
		normal = button.colors;
		normal.normalColor = button.colors.normalColor;
		pressed = button.colors;
		pressed.normalColor = button.colors.pressedColor;
		pressed.highlightedColor = button.colors.pressedColor;
	}

	void Start(){
		GameObject[] allCharacters = charactersManager.GetAllCharacters();
		if(characterNumber-1 >= 0 && characterNumber-1 < allCharacters.Length){
			if(allCharacters[characterNumber - 1] != null){
				character = allCharacters[characterNumber - 1].GetComponent<CharacterMovement>();
				character.setCharacterButton(this);
			}
		}
	}

	public void CharacterDeathButtonDeselect(){
		SetButtonSelected(false);
		button.interactable = false;
		DeselectCharacter();
	}

	public void SetButtonSelected(bool isSelected){
		if(!controlsManager.getGamePaused() && character != null){
			if(!character.getIsDead()){
				if(isSelected){
					selected = true;
					button.colors = pressed;
				}else{
					selected = false;
					button.colors = normal;
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
