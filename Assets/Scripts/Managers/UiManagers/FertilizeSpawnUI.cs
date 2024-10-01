using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FertilizeSpawnUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject iconPrefab; // Prefab của biểu tượng (icon) để kéo
    private GameObject spawnedIcon; // Đối tượng biểu tượng được spawn
    private bool isDragging = false; // Kiểm tra trạng thái kéo

    // Khi nhấn giữ nút trong Canvas
    public void OnPointerDown(PointerEventData eventData)
    {
        if (spawnedIcon == null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f; 
            CameraMovement.Instance.SetCameraMoveDisable(true);
            spawnedIcon = Instantiate(iconPrefab, worldPosition, Quaternion.identity);
            isDragging = true; // Bắt đầu quá trình kéo
        }
    }

    // Khi kéo chuột di chuyển biểu tượng
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && spawnedIcon != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f; 
            spawnedIcon.transform.position = worldPosition;
        }
    }

    // Khi thả chuột
    public void OnPointerUp(PointerEventData eventData)
    {
        if (spawnedIcon != null)
        {
            Destroy(spawnedIcon); // Hủy đối tượng khi thả chuột
            CameraMovement.Instance.SetCameraMoveDisable(false);
            isDragging = false; // Ngừng quá trình kéo
        }
    }
}
