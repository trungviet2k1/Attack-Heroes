using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

    public void Init()
    {
        if (target == null)
        {
            Debug.LogError("Target not assigned for CameraFollow.");
            return;
        }

        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    public void RotateCamera(float rotateX, float rotateY)
    {
        transform.RotateAround(target.position, Vector3.up, rotateX);
        transform.RotateAround(target.position, transform.right, rotateY);
        transform.LookAt(target);
    }
}