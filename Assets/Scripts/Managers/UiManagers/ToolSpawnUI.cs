using UnityEngine;
using UnityEngine.EventSystems;

public class ToolSpawnUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject iconPrefab;
    private GameObject spawnedIcon;
    private bool isDragging = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (spawnedIcon == null)
        {
            CameraMovement.Instance.SetCameraMoveDisable(true);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f;
            spawnedIcon = Instantiate(iconPrefab, worldPosition, Quaternion.identity);
            isDragging = true;
        }
    }

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

    public void OnPointerUp(PointerEventData eventData)
    {
        if (spawnedIcon != null)
        {
            Destroy(spawnedIcon);
            CameraMovement.Instance.SetCameraMoveDisable(false);
            isDragging = false;
        }
    }
}
