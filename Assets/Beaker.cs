using UnityEngine;

public class Beaker : MonoBehaviour
{
    public Material beakerMaterial;
    public float transitionSpeed = 0.2f;
    private bool isHeating = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            isHeating = true;
        }
        else if (other.CompareTag("Scale"))
        {
            isHeating = false;
            beakerMaterial.SetFloat("_TransitionValue", 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Exited trigger with: {other.tag}");

        if (other.CompareTag("Fire"))
        {
            isHeating = false;
        }
    }

    private void Update()
    {
        if (isHeating)
        {
            float transitionValue = beakerMaterial.GetFloat("_TransitionValue");
            transitionValue += Time.deltaTime * transitionSpeed;
            transitionValue = Mathf.Clamp(transitionValue, 0f, 1f);
            beakerMaterial.SetFloat("_TransitionValue", transitionValue);
        }
    }
}
