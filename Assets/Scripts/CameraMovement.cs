using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera RaycastCamera;
    Vector3 startPoint;
    Vector3 startCameraPosition;

    Plane plane;

    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        plane.Raycast(ray, out distance);

        Vector3 point = ray.GetPoint(distance);

        if (Input.GetMouseButtonDown(2))
        {
            startPoint = point;
            startCameraPosition = transform.position;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 offset = point - startPoint;
            transform.position = startCameraPosition - offset;
        }

        transform.Translate(0f,0f,Input.mouseScrollDelta.y);
        RaycastCamera.transform.Translate(0f,0f,Input.mouseScrollDelta.y);
        
    }
}
