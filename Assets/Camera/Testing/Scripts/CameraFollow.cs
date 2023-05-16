using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float zoomAmount = 4f;
    [SerializeField] float yOffset = 2f;
    private Vector3 velocity = Vector3.zero; // The velocity of the camera

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float upLimit;
    [SerializeField] private float downLimit;

    private float camHeight;
    private float camWidth;


    private void Start()
    {
        camHeight = Camera.main.orthographicSize * 2f;
        camWidth = camHeight * Camera.main.aspect;

    }

    void LateUpdate()
    {
        // Calculate the target position for the camera to move towards
        Vector3 targetPosition = target.position + new Vector3(0, yOffset, -10); // Subtracting 10 from z-axis to ensure camera is in front of objects

        // Smoothly move the camera towards the target position using exponential smoothing
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(
                Mathf.Clamp(targetPosition.x, leftLimit + camWidth / 2, rightLimit - camWidth / 2),
                Mathf.Clamp(targetPosition.y, downLimit + camHeight / 2, upLimit - camHeight / 2),
                targetPosition.z),
            ref velocity, smoothTime, Mathf.Infinity, Time.deltaTime);

        // Apply zoom to the camera
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomAmount, Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, downLimit), new Vector2(leftLimit, upLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, downLimit), new Vector2(rightLimit, upLimit));
        
        Gizmos.DrawLine(new Vector2(leftLimit, upLimit), new Vector2(rightLimit, upLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, downLimit), new Vector2(rightLimit, downLimit));

        

    }
}