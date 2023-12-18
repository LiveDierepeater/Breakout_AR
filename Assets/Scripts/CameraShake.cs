using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;
	
    // How long the object should shake for.
    public float shakeDuration = 0.05f;
	
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.2f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;

    private void Awake()
    {
            camTransform = transform;
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            camTransform.localPosition = new Vector3(camTransform.localPosition.x, camTransform.localPosition.y, originalPos.z);
			
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }

        if (shakeDuration <= 0.01f)
        {
            camTransform.localPosition = originalPos;
            Destroy(this);
        }
    }
}
