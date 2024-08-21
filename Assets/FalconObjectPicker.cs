using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FalconObjectPicker : MonoBehaviour
{
    public int falconNum = 0;
    public List<GameObject> controllableObjects;
    public List<GameObject> controllableTexts;
    public List<GameObject> controllableTexts2;
    public List<GameObject> controllableTexts3;
    private int currentObjectIndex = 0;
    private bool[] currentButtonStates = new bool[4];
    private bool[] previousButtonStates = new bool[4];
    private Vector3 lastPosition;
    public float forceFactor = 1.0f;
    public float damping = 0.5f;
    public float movementThreshold = 0.001f;
    private Vector3 smoothedPosition;
    public float smoothingFactor = 0.1f;

    void Start()
    {
        for (int i = 0; i < currentButtonStates.Length; i++)
        {
            currentButtonStates[i] = false;
            previousButtonStates[i] = false;
        }

        if (controllableObjects.Count > 0)
        {
            currentObjectIndex = 0;
            lastPosition = GetTipPosition();
            UpdateObjectVisibility();
        }
    }

    void Update()
    {
        bool gotButtonStates = FalconUnity.getFalconButtonStates(falconNum, out currentButtonStates);

        if (!gotButtonStates)
        {
            Debug.LogError("Error getting button states");
            return;
        }
        HandleCycling();

        Vector3 newPosition = GetTipPosition();
        if (newPosition != Vector3.zero)
        {
            smoothedPosition = Vector3.Lerp(smoothedPosition, newPosition, smoothingFactor);

            GameObject currentObject = controllableObjects[currentObjectIndex];
            
            // Update position without causing rotation
            currentObject.transform.position = smoothedPosition;
            currentObject.transform.rotation = Quaternion.identity;

            Vector3 movement = smoothedPosition - lastPosition;

            if (movement.magnitude > movementThreshold && movement.magnitude != Mathf.Infinity)
            {
                ApplyHapticFeedback(movement, currentObject.GetComponent<Rigidbody>().mass);
            }
            else
            {
                FalconUnity.applyForce(falconNum, Vector3.zero, 0);
            }

            lastPosition = smoothedPosition;
        }
        else
        {
            Debug.LogError("Error getting Falcon's tip position");
        }
        previousButtonStates = (bool[])currentButtonStates.Clone();
    }

    private Vector3 GetTipPosition()
    {
        Vector3 pos;
        if (FalconUnity.getTipPosition(falconNum, out pos))
        {
            return pos;
        }
        return Vector3.zero;
    }

    private void ApplyHapticFeedback(Vector3 movement, float objectMass)
    {
        Vector3 force = -movement.normalized * objectMass * forceFactor * damping;
        FalconUnity.applyForce(falconNum, force, Time.deltaTime);
    }


    private void HandleCycling()
    {
        if (currentButtonStates[1] && !previousButtonStates[1])
        {
            currentObjectIndex--;
            if (currentObjectIndex < 0)
                currentObjectIndex = controllableObjects.Count - 1;
            UpdateObjectVisibility();
            Debug.Log("Cycled Left to: " + controllableObjects[currentObjectIndex].name);
        }

        if (currentButtonStates[3] && !previousButtonStates[3])
        {
            currentObjectIndex++;
            if (currentObjectIndex >= controllableObjects.Count)
                currentObjectIndex = 0; // Wrap around to the first object
            UpdateObjectVisibility();
            Debug.Log("Cycled Right to: " + controllableObjects[currentObjectIndex].name);
        }
    }

    private void UpdateObjectVisibility()
    {
        for (int i = 0; i < controllableObjects.Count; i++)
        {
            controllableObjects[i].SetActive(false);
            controllableTexts[i].SetActive(false);
            controllableTexts2[i].SetActive(false);
            controllableTexts3[i].SetActive(false);
        }
        controllableObjects[currentObjectIndex].SetActive(true);
        controllableTexts[currentObjectIndex].SetActive(true);
        controllableTexts2[currentObjectIndex].SetActive(true);
        controllableTexts3[currentObjectIndex].SetActive(true);
    }
}
