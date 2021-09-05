using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] Transform t1;
    [SerializeField] Transform t2;
    [SerializeField] Transform t3;
    public void FixedCameraFollowSmooth(/*Camera cam, Transform t1, Transform t2*/)
    {
        // How many units should we keep from the players
        float zoomFactor = 1.5f;
        float followTimeDelta = 0.8f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;

        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;
        distance = Mathf.Clamp(distance, 4.4f, 6f);

        var cam = Camera.main;
        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
        {
            // The camera's forward vector is irrelevant, only this size will matter
            cam.orthographicSize = distance;
        }
        // You specified to use MoveTowards instead of Slerp
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;


        cam.transform.rotation = Quaternion.LookRotation((t3.position - 3 * Vector3.up) - midpoint, Vector3.up);
    }

    void Update()
    {
        FixedCameraFollowSmooth();
    }
}
