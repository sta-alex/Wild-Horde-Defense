using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoverSkript : MonoBehaviour
{
    private float zoom;
    private float zoomMultiplier = 50f;
    private float minZoom = 10f;
    private float maxZoom = 80f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    private float startAngleX = 85f;
    private float zoomedInAngleX = 30f;

    private bool isDragging = false;
    private Vector3 dragOrigin;
    public float cameraMoveSpeed = 10f;
    public float dragSpeed = 2f;

    [SerializeField] private Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        zoom = Camera.fieldOfView;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleZoomInput();
        HandleCameraMovementInput();


    }
    void HandleZoomInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, zoom, ref velocity, smoothTime);
    }
    void HandleCameraMovementInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed) * cameraMoveSpeed;

        transform.Translate(move, Space.World);
    }
}
