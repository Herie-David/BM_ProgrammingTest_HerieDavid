using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    public float smoothSpeed = 5f; // The speed at which the camera will follow the target
    Vector3 offset = new Vector3(-14, 9, -12); // The initial offset between the camera and the target

    void LateUpdate()
    {
        Vector3 targetpos = target.position + offset;
        if (target != null)
        {
            // Smoothly interpolate between the camera's current position and the target's position
            transform.position = Vector3.Lerp(transform.position, targetpos, smoothSpeed * Time.deltaTime);
        }
    }
}