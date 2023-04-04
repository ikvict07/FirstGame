using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float zoomAmount = 5f;
    [SerializeField] float yOffset = 2f;
    private Vector3 velocity = Vector3.zero; // The velocity of the camera

    void LateUpdate()
    {
        // Calculate the target position for the camera to move towards
        Vector3 targetPosition = target.position + new Vector3(0, yOffset, -10); // Subtracting 10 from z-axis to ensure camera is in front of objects

        // Smoothly move the camera towards the target position using exponential smoothing
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.deltaTime);

        // Apply zoom to the camera
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomAmount, Time.deltaTime);
    }
}