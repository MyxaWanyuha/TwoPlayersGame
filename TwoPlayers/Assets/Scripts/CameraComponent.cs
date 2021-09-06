using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] Transform t1;
    [SerializeField] Transform t2;
    [SerializeField] Transform t3;
    public void FixedCameraFollowSmooth()
    {
        const float zoomFactor = 1.5f;
        const float followTimeDelta = 0.8f;

        Vector3 midpoint = (t1.position + t2.position) / 2f;

        float distance = (t1.position - t2.position).magnitude;
        distance = Mathf.Clamp(distance, 4.4f, 6f);

        var cam = Camera.main;
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        if (cam.orthographic)
        {
            cam.orthographicSize = distance;
        }
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;


        cam.transform.rotation = Quaternion.LookRotation((t3.position - 3 * Vector3.up) - midpoint, Vector3.up);
    }

    void Update()
    {
        FixedCameraFollowSmooth();
    }
}
