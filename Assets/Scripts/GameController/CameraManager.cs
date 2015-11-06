﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Camera cameraToUse;
	public float zoomSpeed = 90f;
	float zoomSpeedWithDpi = 0f;
	public float minZoom = 4f;
	public float maxZoom = 20f;
	public float dragSpeed = 90f;
	float dragSpeedWithDpi = 0f;
	float dragSpeedWithZoom = 0f;
	float zoomMedium = 0f;
	public float dragMinX;
	public float dragMaxX;
	public float dragMinY;
	public float dragMaxY;

	//
	Text text;
	//

	public void Awake(){

		//
		text = GameObject.FindGameObjectWithTag("debugtext").GetComponent<Text>();
		//

		zoomSpeedWithDpi = zoomSpeed/Screen.dpi;
		dragSpeedWithDpi = dragSpeed/Screen.dpi;

		if (cameraToUse.orthographic){
			zoomMedium = (minZoom+maxZoom)/2;
			ChangeDragSpeedWithZoom(cameraToUse.orthographicSize);
		}else{
			dragSpeedWithZoom = dragSpeedWithDpi;
		}


	}


	public void DragCamera(Vector2 drag){

		//
		text.text = drag.ToString() + "\ndpi: " + Screen.dpi + "\nspeed:" + dragSpeedWithZoom;
		//

		cameraToUse.transform.position =
			new Vector3(Mathf.Clamp(cameraToUse.transform.position.x - (drag.x * dragSpeedWithZoom), dragMinX, dragMaxX),
			            cameraToUse.transform.position.y, 
			            Mathf.Clamp(cameraToUse.transform.position.z - (drag.y * dragSpeedWithZoom), dragMinY, dragMaxY));
	}

	public void ZoomCamera(float deltaMagnitudeDiff){			
		// If the camera is orthographic...
		if (cameraToUse.orthographic)
		{
			// ... change the orthographic size based on the change in distance between the touches.
			// Make sure the orthographic size is within the min and maxzoom.
			cameraToUse.orthographicSize = Mathf.Clamp(cameraToUse.orthographicSize + (deltaMagnitudeDiff * zoomSpeedWithDpi), minZoom, maxZoom);

			ChangeDragSpeedWithZoom(cameraToUse.orthographicSize);
		}
		else
		{
			// ... change the z axis of the camera
			// Make sure the z is within the min and maxzoom.
			cameraToUse.transform.localPosition = 
				new Vector3(cameraToUse.transform.localPosition.x, 
				            cameraToUse.transform.localPosition.y, 
				            Mathf.Clamp(cameraToUse.transform.localPosition.z + (deltaMagnitudeDiff * zoomSpeedWithDpi), minZoom, maxZoom));
		}


	}

	void ChangeDragSpeedWithZoom(float zoomOrthographicSize){
		dragSpeedWithZoom = dragSpeedWithDpi * (zoomOrthographicSize/zoomMedium);
	}
}
