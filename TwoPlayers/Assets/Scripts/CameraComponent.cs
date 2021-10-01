using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] Transform t1;
    [SerializeField] Transform t2;
    public void FixedCameraFollowSmooth()
    {
        const float zoomFactor = 1.7f;
        const float followTimeDelta = 0.8f;

        Vector3 midpoint = (t1.position + t2.position) * 0.5f;

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

        var v = new Vector3(0, (t1.position.y + t2.position.y) * 0.35f - 3, 0);
        cam.transform.rotation = Quaternion.LookRotation(v - midpoint, Vector3.up);
    }

    void Update()
    {
        FixedCameraFollowSmooth();
    }
}
