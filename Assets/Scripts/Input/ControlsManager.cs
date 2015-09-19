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
	public float dragThreshold = 0.2f;
	float dragTimePassed = 0f;

	Rect clickableArea;
	public Button buttonToRemoveFromWidth;

	bool gamePaused = false;
	GameObject[] objectsToShowOnPause;

	void Awake () {
		objectsToShowOnPause = GameObject.FindGameObjectsWithTag(Tags.pauseObject);
		setObjectsToShowOnPause(false);

		charactersManager = GetComponent<CharactersManager>();
		cameraManager = GetComponent<CameraManager>();

		//get the width from the button, and set the clickable area to remove the buttons width
		RectTransform rectTransform = buttonToRemoveFromWidth.GetComponent<RectTransform>();
		float buttonWidth = GetWidthFromRectTransformUI(rectTransform);
		clickableArea = new Rect(0,0,Screen.width - buttonWidth, Screen.height);

		setPause(false);
	}
	
	void Update () {


#if UNITY_ANDROID || UNITY_WP8
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
					cameraManager.DragCamera( DragMovement(Input.GetTouch(0)) );
					dragTimePassed += Time.deltaTime;
				}

				//move character mobile
				if (Input.GetTouch(0).phase == TouchPhase.Ended && dragTimePassed < dragThreshold) {
					charactersManager.CheckClickedPoint(Input.GetTouch(0).position);
				}
				//reset dragTimePassed
				if (Input.GetTouch(0).phase == TouchPhase.Ended) {
					dragTimePassed = 0f;
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

//#else
//####DESKTOP
		//move character desktop
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
