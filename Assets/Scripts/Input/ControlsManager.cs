using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (CharactersManager))]
[RequireComponent (typeof (CameraManager))]
public class ControlsManager : MonoBehaviour {

	CharactersManager charactersManager;
	CameraManager cameraManager;
	bool pinching = false;
	public float dragThresholdTime = 0.05f;
	float dragTimePassed = 0f;

	Rect clickableArea;
	public Button[] allButtons;
	CharacterSelectButton[] characterSelectButtons;
	Rect[] buttonsRects;

	bool gamePaused = false;
	GameObject[] objectsToShowOnPause;

	public Texture mytexture;

	void Awake () {
		objectsToShowOnPause = GameObject.FindGameObjectsWithTag(Tags.pauseObject);
		setObjectsToShowOnPause(false);

		charactersManager = GetComponent<CharactersManager>();
		cameraManager = GetComponent<CameraManager>();

		//get the width from the button, and set the clickable area to remove the buttons width
		RectTransform rectTransform = allButtons[0].GetComponent<RectTransform>();
		float buttonWidth = GetWidthFromRectTransformUI(rectTransform);
		clickableArea = new Rect(0,0,Screen.width - buttonWidth, Screen.height);

		characterSelectButtons = new CharacterSelectButton[allButtons.Length];
		buttonsRects = new Rect[allButtons.Length];
		for(int i=0; i<allButtons.Length;i++){
			characterSelectButtons[i] = allButtons[i].GetComponent<CharacterSelectButton>();
			buttonsRects[i] = GetRectFromRectTransformUI( allButtons[i].GetComponent<RectTransform>() );
		}
		setPause(false);
	}
	
	void Update () {

#if !UNITY_IOS
//####MOBILE exit button. IOS don't have an exit button
		if(Input.GetKeyDown(KeyCode.Escape)){
			setPause(!gamePaused);
		}
#endif

#if UNITY_ANDROID || UNITY_WP8 || UNITY_IOS
//####MOBILE
		if(Input.touchCount == 1 && !pinching){
			if(clickableArea.Contains(Input.GetTouch(0).position)){
				//move camera mobile
				if (Input.GetTouch(0).phase == TouchPhase.Moved) {
					/*if(dragTimePassed >= dragThresholdTime){*/
						cameraManager.DragCamera( DragMovement(Input.GetTouch(0)) );
					/*}*/
					dragTimePassed += Time.deltaTime;
				}

				//move character mobile
				if (Input.GetTouch(0).phase == TouchPhase.Ended && dragTimePassed < dragThresholdTime) {
					charactersManager.CheckClickedPoint(Input.GetTouch(0).position);
				}
				//reset dragTimePassed
				if (Input.GetTouch(0).phase == TouchPhase.Ended) {
					dragTimePassed = 0f;
				}
			}else{
				//start selecting buttons
				if (Input.GetTouch(0).phase == TouchPhase.Began) {
					CharacterSelectButton buttonTouched = CheckButtonsTouched(Input.GetTouch(0).position);
					if(buttonTouched != null){
							buttonTouched.DeselectAllCharacters();
							buttonTouched.SelectCharacterWithoutDeselectingOthers();
					}
				}

				//drag buttons
				if (Input.GetTouch(0).phase == TouchPhase.Moved) {
					CharacterSelectButton buttonTouched = CheckButtonsTouched(Input.GetTouch(0).position);
					if(buttonTouched != null){
						if(!buttonTouched.selected){
							buttonTouched.SelectCharacterWithoutDeselectingOthers();
						}
					}
				}
			}
		}
		//Zoom mobile
		if (Input.touchCount == 2){
			if(clickableArea.Contains(Input.GetTouch(0).position) && clickableArea.Contains(Input.GetTouch(1).position)){
				pinching = true;
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);
				
				cameraManager.ZoomCamera( PinchMovement(touchZero, touchOne) );
			}
		}
		if(Input.touchCount == 0){
			pinching = false;
		}
#endif
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_WP8 && !UNITY_IOS)
		//####DESKTOP
		//move character desktop
		if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f){
			cameraManager.DragCamera(new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")));
		}
		if(clickableArea.Contains(Input.mousePosition)){
			if (Input.GetMouseButtonDown(0)) {
				charactersManager.CheckClickedPoint(Input.mousePosition);
			}
			
			if(Input.GetMouseButton(0)){
				dragTimePassed += Time.deltaTime;
			}
			
			if(Input.GetMouseButtonUp(0)){
				dragTimePassed = 0f;
			}
		}
#endif
	}

	float GetWidthFromRectTransformUI(RectTransform rectTransform){
		//the rect is a percentage. the maximum number, 1, represents 100% from the screen width
		//so we multiply the screen width by the rect to get the real width of the button
		float rectWidth = rectTransform.anchorMax.x - rectTransform.anchorMin.x;
		rectWidth *= Screen.width;

		return rectWidth;
	}

	float GetHeightFromRectTransformUI(RectTransform rectTransform){
		//the rect is a percentage. the maximum number, 1, represents 100% from the screen width
		//so we multiply the screen width by the rect to get the real width of the button
		float rectHeight = rectTransform.anchorMax.y - rectTransform.anchorMin.y;
		rectHeight *= Screen.height;
		
		return rectHeight;
	}

	Rect GetRectFromRectTransformUI(RectTransform rectTransform){
		return new Rect(rectTransform.anchorMin.x*Screen.width,rectTransform.anchorMin.y*Screen.height, GetWidthFromRectTransformUI(rectTransform), GetHeightFromRectTransformUI(rectTransform));
	}

	CharacterSelectButton CheckButtonsTouched(Vector2 position){
		for(int i=0; i<buttonsRects.Length; i++){
			if(buttonsRects[i].Contains(position)){
				return characterSelectButtons[i];
			}
		}
		return null;
	}

	Vector2 DragMovement(Touch touch){
		Vector2 currentDrag = new Vector2();
		currentDrag = touch.position - (touch.position - touch.deltaPosition);
		return currentDrag;
	}
	
	float PinchMovement(Touch touchZero, Touch touchOne){
		// Find the position in the previous frame of each touch.
		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
		
		// Find the magnitude of the vector (the distance) between the touches in each frame.
		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
		
		// Find the difference in the distances between each frame.
		float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

		return deltaMagnitudeDiff;
	}

	public void setPause(bool pause){
		if(pause){
			gamePaused = true;
			setObjectsToShowOnPause(true);
			Time.timeScale = 0f;
		}else{
			gamePaused = false;
			setObjectsToShowOnPause(false);
			Time.timeScale = 1f;
		}
	}

	public void StageEndPause(){
		gamePaused = true;
		Time.timeScale = 0f;
	}

	void setObjectsToShowOnPause(bool show){
		foreach(GameObject objectToSet in objectsToShowOnPause){
			objectToSet.SetActive(show);
		}
	}

	public bool getGamePaused(){
		return gamePaused;
	}
}
