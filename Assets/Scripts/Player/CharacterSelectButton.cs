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

	void Awake(){
		charactersManager = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<CharactersManager>();
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

	void Start(){
		GameObject[] allCharacters = charactersManager.GetAllCharacters();
		if(characterNumber-1 >= 0 && characterNumber-1 < allCharacters.Length){
			if(allCharacters[characterNumber - 1] != null){
				character = allCharacters[characterNumber - 1].GetComponent<CharacterMovement>();
				character.setCharacterButton(this);
			}
		}
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
