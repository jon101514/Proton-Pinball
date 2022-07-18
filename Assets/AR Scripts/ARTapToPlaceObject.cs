/* 
 * Based on "Getting Started with ARFoundation in Unity" by The Unity Workbench, available here: "https://www.youtube.com/watch?v=Ml2UakwRxjk"
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour{

    // PUBLIC
	public GameObject placementIndicator; 
	public GameObject objectToPlace; // Prefab object to place in our world.

	private GameObject objectInstance;

	private ARSessionOrigin arOrigin;
	private ARRaycastManager arRayManager;
	private Pose placementPose; // Describes position and rotation of a 3D point
	private bool placementPoseIsValid = false;

    void Start() {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
		arRayManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update() {
		UpdatePlacementPose();
		UpdatePlacementIndicator();
        // If the player touches the screen, place the object.
		if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			// Handheld.Vibrate();
			PlaceObject();
		}
    }

	private void UpdatePlacementPose() {
		var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
		var hits = new List<ARRaycastHit>();
		arRayManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
		placementPoseIsValid = hits.Count > 0;
		if (placementPoseIsValid) {
			placementPose = hits[0].pose;
            // Rotate the placement pose object.
			var cameraForward = Camera.current.transform.forward;
			var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
			placementPose.rotation = Quaternion.LookRotation(cameraBearing);
		}
	}

    /** Show or hide the placement indicator depending on 
     * whether or not the placement pose is valid (there's a raycast hit).
     */
	private void UpdatePlacementIndicator() {
		if (placementPoseIsValid) {
			placementIndicator.SetActive(true);
			placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
		} else {
			placementIndicator.SetActive(false);
		}
	}

	public void PlaceObject() {
		// if (!placementPoseIsValid || !StateManager.instance.GetState().Equals(StateManager.GameState.PLACEMENT)) { return; }
		if (!placementPoseIsValid) { return; }
		if (objectInstance == null) {
			objectInstance = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
		} else {
			objectInstance.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
		}
		// Hide the placement indicator
		placementIndicator.SetActive(false);
		// StateManager.instance.ChangeState();
	}
}
