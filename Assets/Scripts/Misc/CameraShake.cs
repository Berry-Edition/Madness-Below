using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity = 0.2f; // Intensité du shake
    public float shakeSpeed = 1f; // Vitesse du shake

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        float shakeX = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0) - 0.5f) * shakeIntensity;
        float shakeY = (Mathf.PerlinNoise(0, Time.time * shakeSpeed) - 0.5f) * shakeIntensity;

        transform.localPosition = originalPosition + new Vector3(shakeX, shakeY, 0);
    }
}