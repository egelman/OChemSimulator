using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandInteraction : MonoBehaviour
{
    // References to hand tracking components
    private OVRHand handTracking;

    // Object to manipulate
    public GameObject targetObject;

    // Variables to track pinch states and initial hand position
    private bool isIndexFingerPinching = false;
    private bool isMiddleFingerPinching = false;
    private Vector3 initialHandPosition;

    void Start()
    {
        // Initialize hand tracking component
        handTracking = GetComponent<OVRHand>();
    }

    void Update()
    {
        // Check for index and middle finger pinches and update states
        bool currentIndexPinchState = handTracking.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool currentMiddlePinchState = handTracking.GetFingerIsPinching(OVRHand.HandFinger.Middle);

        // Handle index finger pinch
        if (currentIndexPinchState && !isIndexFingerPinching)
        {
            // Index pinch started
            isIndexFingerPinching = true;
            initialHandPosition = handTracking.transform.position;
        }
        else if (!currentIndexPinchState && isIndexFingerPinching)
        {
            // Index pinch ended
            isIndexFingerPinching = false;
        }

        // Handle middle finger pinch
        if (currentMiddlePinchState && !isMiddleFingerPinching)
        {
            // Middle pinch started
            isMiddleFingerPinching = true;
            initialHandPosition = handTracking.transform.position;
        }
        else if (!currentMiddlePinchState && isMiddleFingerPinching)
        {
            // Middle pinch ended
            isMiddleFingerPinching = false;
        }
        
        // Move the object if either index or middle finger is pinching
        if (isIndexFingerPinching || isMiddleFingerPinching)
        {
            MoveObjectWithHand();
        }
    }

    void MoveObjectWithHand()
    {
        // Calculate the movement based on hand position change
        Vector3 handMovement = handTracking.transform.position - initialHandPosition;
        // Apply the movement
        targetObject.transform.position += handMovement;
        // Update the initial position for the next frame
        initialHandPosition = handTracking.transform.position;
    }
}
