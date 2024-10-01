#region Current

//using UnityEngine;

//public class CameraMovement : MonoBehaviour
//{
//    public float dragSpeed = 2f;
//    public float zoomSpeed = 2f;
//    public float minZoom = 1f;
//    public float maxZoom = 4f;

//    private Vector3 dragOrigin;
//    private PolygonCollider2D boundary;

//    void Start()
//    {
//        // Find the boundary collider in the scene
//        boundary = GameObject.Find("MapLimit").GetComponent<PolygonCollider2D>();
//    }

//    void Update()
//    {
//        HandleMouseDrag();
//        HandleMouseScroll();
//    }

//    private void HandleMouseDrag()
//    {
//        // Check for mouse down to set the drag origin
//        if (Input.GetMouseButtonDown(0))
//        {
//            dragOrigin = Input.mousePosition;
//            return;
//        }

//        // Check for mouse drag to move the camera
//        if (Input.GetMouseButton(0))
//        {
//            Vector3 difference = Camera.main.ScreenToWorldPoint(dragOrigin) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 newPosition = Camera.main.transform.position + difference;

//            // Restrict camera movement within the polygon boundary
//            if (boundary.OverlapPoint(newPosition))
//            {
//                Camera.main.transform.position = newPosition;
//            }

//            dragOrigin = Input.mousePosition;
//        }
//    }

//    private void HandleMouseScroll()
//    {
//        // Zoom with mouse scroll wheel (optional)
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        if (scroll != 0.0f)
//        {
//            Zoom(scroll * zoomSpeed);
//        }
//    }

//    public void ZoomIn()
//    {
//        Zoom(-zoomSpeed);
//    }

//    public void ZoomOut()
//    {
//        Zoom(zoomSpeed);
//    }

//    private void Zoom(float increment)
//    {
//        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + increment, minZoom, maxZoom);
//    }
//}

#endregion

#region Old
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
#endregion


#region New
using UnityEngine;

public class CameraMovement : Singleton<CameraMovement>
{
    [Header("Camera limit")]
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;

    [Header("Config Camera")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomMin;
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float followSpeed;

    private bool disableCameraMove;
    private Camera cam;
    private Vector3 dragStart;

    private Transform objectToFollow;
    private Bounds objectBounds;
    private Vector3 prevPos;

    #region Unity Method

    protected override void Awake()
    {
        base.Awake();
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (objectToFollow != null)
        {
            MakeObjectFollow();
        }
        else
        {
            MoveCamera();
        }
        ZoomCamera();
    }

    #endregion

    #region Camera Move

    private void MoveCamera()
    {
        if (!disableCameraMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragStart = Input.mousePosition;
                return;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = cam.ScreenToViewportPoint(dragStart - Input.mousePosition);
                Vector3 move = new Vector3(direction.x * moveSpeed, direction.y * moveSpeed, 0);
                cam.transform.Translate(move, Space.World);

                MoveLimit();

                dragStart = Input.mousePosition;
            }
        }
    }

    private void MoveLimit()
    {
        float posX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        float posY = Mathf.Clamp(transform.position.y, bottomLimit, topLimit);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    public void SetCameraMoveDisable(bool value)
    {
        disableCameraMove = value;
    }

    #endregion

    #region Camera zoom 
    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomMin, zoomMax);
        }
    }

    #endregion

    #region Camera Follow Object 

    private void MakeObjectFollow()
    {
        if (objectToFollow != null)
        {
            Vector3 targetPosition = objectToFollow.position;
            Vector3 smoothedPosition = Vector3.Lerp(cam.transform.position, targetPosition, followSpeed * Time.deltaTime);

            float cameraHeight = cam.orthographicSize;
            float cameraWidth = cameraHeight * cam.aspect;

            float minX = objectBounds.min.x - cameraWidth;
            float maxX = objectBounds.max.x + cameraWidth;
            float minY = objectBounds.min.y - cameraHeight;
            float maxY = objectBounds.max.y + cameraHeight;

            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            cam.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, cam.transform.position.z);

            MoveLimit();
        }
    }

    public void FollowObject(Transform objToFollow)
    {
        objectToFollow = objToFollow;
        objectBounds = objToFollow.GetComponent<PolygonCollider2D>().bounds;
        prevPos = cam.ScreenToViewportPoint(Vector3.zero);
    }

    public void UnFollowObject()
    {
        objectToFollow = null;
    }

    #endregion

    #region Draw Limit Area 

    private void OnDrawGizmos()
    {
        Vector3 center = new Vector3((rightLimit + leftLimit) / 2.0f,
            (topLimit + bottomLimit) / 2.0f);
        Vector3 size = new Vector3(rightLimit - leftLimit, topLimit - bottomLimit, 0);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }

    #endregion

}
#endregion