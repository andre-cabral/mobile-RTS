using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (CharactersManager))]
[RequireComponent (typeof (CameraManager))]
public class ControlsManager : MonoBehaviour {

	CharactersManager charactersManager;
	CameraManager cameraManager;

	void Awake () {
		charactersManager = GetComponent<CharactersManager>();
		cameraManager = GetComponent<CameraManager>();
	}
	
	void Update () {
		//####DESKTOP
		//move character desktop
		if (Input.GetMouseButtonDown(0)) {
			charactersManager.CheckClickedPoint(Input.mousePosition);
			
		}


		//####MOBILE
		#if UNITY_ANDROID || UNITY_WP8
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
		#endif

		if(Input.touchCount == 1){
			//move character mobile
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				charactersManager.CheckClickedPoint(Input.GetTouch(0).position);
			}

			//move camera mobile
			if (Input.GetTouch(0).phase == TouchPhase.Moved) {
				cameraManager.DragCamera( DragMovement(Input.GetTouch(0)) );
			}
		}
		//Zoom mobile
		if (Input.touchCount == 2){
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			cameraManager.ZoomCamera( PinchMovement(touchZero, touchOne) );
		}

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
}
