using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float dragSpeed = 2f;
    public float zoomSpeed = 2f;
    public float minZoom = 1f;
    public float maxZoom = 4f;

    private Vector3 dragOrigin;
    private PolygonCollider2D boundary;

    void Start()
    {
        // Find the boundary collider in the scene
        boundary = GameObject.Find("MapLimit").GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        HandleMouseDrag();
        HandleMouseScroll();
    }

    private void HandleMouseDrag()
    {
        // Check for mouse down to set the drag origin
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        // Check for mouse drag to move the camera
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(dragOrigin) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = Camera.main.transform.position + difference;

            // Restrict camera movement within the polygon boundary
            if (boundary.OverlapPoint(newPosition))
            {
                Camera.main.transform.position = newPosition;
            }

            dragOrigin = Input.mousePosition;
        }
    }

    private void HandleMouseScroll()
    {
        // Zoom with mouse scroll wheel (optional)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            Zoom(scroll * zoomSpeed);
        }
    }

    public void ZoomIn()
    {
        Zoom(-zoomSpeed);
    }

    public void ZoomOut()
    {
        Zoom(zoomSpeed);
    }

    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + increment, minZoom, maxZoom);
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraMovement : MonoBehaviour
// {
//     public float dragSpeed = 2f; // Tốc độ kéo camera
//     private Vector3 dragOrigin;  // Gốc tọa độ khi bắt đầu kéo
//     public Transform mapBounds;  // Đối tượng chứa collider để giới hạn camera
//     private Vector3 minLimit, maxLimit;  // Giới hạn camera

//     void Start()
//     {
//         // Lấy Collider của map để xác định giới hạn
//         PolygonCollider2D bounds = mapBounds.GetComponent<PolygonCollider2D>();
//         minLimit = bounds.bounds.min;
//         maxLimit = bounds.bounds.max;
//     }

//     void Update()
//     {
//         // Kiểm tra nếu người chơi nhấn nút chuột trái
//         if (Input.GetMouseButtonDown(0))
//         {
//             dragOrigin = Input.mousePosition; // Lưu vị trí chuột khi bắt đầu kéo
//             return;
//         }

//         // Kiểm tra nếu người chơi vẫn giữ nút chuột trái
//         if (Input.GetMouseButton(0))
//         {
//             Vector3 difference = dragOrigin - Input.mousePosition; // Tính toán khoảng cách di chuyển chuột
//             dragOrigin = Input.mousePosition; // Cập nhật lại gốc tọa độ

//             // Di chuyển camera theo khoảng cách chuột di chuyển
//             Vector3 move = new Vector3(difference.x * dragSpeed * Time.deltaTime, difference.y * dragSpeed * Time.deltaTime, 0);
//             transform.Translate(move, Space.World);

//             // Giới hạn camera
//             Vector3 pos = transform.position;
//             pos.x = Mathf.Clamp(pos.x, minLimit.x, maxLimit.x);
//             pos.y = Mathf.Clamp(pos.y, minLimit.y, maxLimit.y);
//             transform.position = pos;
//         }
//     }
// }
