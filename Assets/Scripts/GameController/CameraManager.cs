using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Camera cameraToUse;
	public float zoomSpeed = 0.5f;
	public float minZoom = 4f;
	public float maxZoom = 20f;
	public float dragSpeed = 0.5f;
	public float dragMinX;
	public float dragMaxX;
	public float dragMinY;
	public float dragMaxY;

	public void DragCamera(Vector2 drag){
		cameraToUse.transform.position =
			new Vector3(Mathf.Clamp(cameraToUse.transform.position.x - (drag.x * dragSpeed), dragMinX, dragMaxX),
			            cameraToUse.transform.position.y, 
			            Mathf.Clamp(cameraToUse.transform.position.z - (drag.y * dragSpeed), dragMinY, dragMaxY));
	}

	public void ZoomCamera(float deltaMagnitudeDiff){			
		// If the camera is orthographic...
		if (cameraToUse.isOrthoGraphic)
		{
			// ... change the orthographic size based on the change in distance between the touches.
			// Make sure the orthographic size is within the min and maxzoom.
			cameraToUse.orthographicSize = Mathf.Clamp(cameraToUse.orthographicSize + (deltaMagnitudeDiff * zoomSpeed), minZoom, maxZoom);
		}
		else
		{
			// ... change the z axis of the camera
			// Make sure the z is within the min and maxzoom.
			cameraToUse.transform.localPosition = 
				new Vector3(cameraToUse.transform.localPosition.x, 
				            cameraToUse.transform.localPosition.y, 
				            Mathf.Clamp(cameraToUse.transform.localPosition.z + (deltaMagnitudeDiff * zoomSpeed), minZoom, maxZoom));
		}
	}
}
