using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectButton : MonoBehaviour {

	public CharacterMovement character;
	CharactersManager charactersManager;
	public bool selected = false;
	CharacterSelectButton[] characterSelectButtons;
	Button button;
	ColorBlock normal = new ColorBlock();
	ColorBlock pressed = new ColorBlock();

	void Awake(){
		if(character != null){
			character.setCharacterButton(this);
		}

		charactersManager = GameObject.FindGameObjectWithTag(Tags.charactersController).GetComponent<CharactersManager>();

		GameObject[] buttons = GameObject.FindGameObjectsWithTag(Tags.characterSelectButton);
		characterSelectButtons = new CharacterSelectButton[buttons.Length];
		for(int i=0; i<buttons.Length; i++){
			characterSelectButtons[i] = buttons[i].GetComponent<CharacterSelectButton>();
		}

		button = GetComponent<Button>();
		normal = button.colors;
		normal.normalColor = button.colors.normalColor;
		pressed = button.colors;
		pressed.normalColor = button.colors.pressedColor;
		pressed.highlightedColor = button.colors.pressedColor;
	}

	public void SetButtonSelected(bool isSelected){
		if(isSelected){
			selected = true;
			button.colors = pressed;
		}else{
			selected = false;
			button.colors = normal;
		}
	}

	void SetAllButtonsSelection(bool isSelected){
		for(int i=0; i<characterSelectButtons.Length; i++){
			characterSelectButtons[i].SetButtonSelected(isSelected);
		}
	}

	public void SelectCharacter(){
		if(character != null){
			SetAllButtonsSelection(false);
			SetButtonSelected(true);
			charactersManager.SelectOneCharacter(character);
		}
	}
	public void SelectAllCharacters(){
		SetAllButtonsSelection(true);
		charactersManager.SelectAllCharacters();
	}
}
