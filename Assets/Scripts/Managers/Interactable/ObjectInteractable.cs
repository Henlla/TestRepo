using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectInteractable : Interactable
{
    private float timer = 0f;
    private bool touching = false;
    private ObjectPlaceable temp;

    private void Update()
    {
        if (MapManager.Instance.IsMapEditMode())
        {
            StageEditBehaviour();
        }
    }

    public override void OnClicked()
    {
        bool isMapEdit = MapManager.Instance.IsMapEditMode();
        bool isDecorEdit = MapManager.Instance.IsDecorEditMode();
        if (!isMapEdit && !isDecorEdit)
        {
            StageClickBehaviour();
        }
    }

    public void InEditMode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnGetObject();
        }
        if (!temp)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            OnObjectDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnObjectDrop();
        }
    }

    #region Object Movement

    public void OnGetObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            temp = hit.collider.gameObject.GetComponent<ObjectPlaceable>();
        }
        else
        {
            timer = 0;
            temp = null;
        }
    }

    public void OnObjectDrag()
    {
        if (!temp)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }
        if (!touching && temp.Placed)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                touching = true;
                temp.gameObject.AddComponent<ObjectDrag>();
                Vector3Int positionInt = GridBuildingSystem.Instance.gridLayout.WorldToCell(temp.transform.position);
                BoundsInt areaTemp = temp.area;
                areaTemp.position = positionInt;
                GridBuildingSystem.Instance.ClearArea(areaTemp, GridBuildingSystem.Instance.MainFarm);

                CameraMovement.Instance.SetCameraMoveDisable(true);
                GridBuildingSystem.Instance.MoveSmooth(temp);
                CameraMovement.Instance.FollowObject(temp.transform);
            }
        }
    }

    public void OnObjectDrop()
    {
        if (touching)
        {
            touching = false;
            if (!ShopManager.Instance.isItemFromShop)
            {
                temp.CheckPlacement();
                ObjectManager.Instance.SaveMap(temp);
            }
            CameraMovement.Instance.SetCameraMoveDisable(false);
            CameraMovement.Instance.UnFollowObject();
        }
        timer = 0;
        temp = null;
    }

    #endregion

    #region Click or Move

    public void StageEditBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnGetObject();
        }
        if (!temp)
        {
            return;
        }
        switch (temp.objectLib.GetObjectType())
        {
            case ObjectType.Shop:
                InEditMode();
                break;
            case ObjectType.Enclosure:
                InEditMode();
                break;
            case ObjectType.Factory:
                InEditMode();
                break;
            default:
                break;
        }
    }

    public void StageClickBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnGetObject();
        }
        if (!temp)
        {
            return;
        }
        switch (temp.objectLib.GetObjectType())
        {
            case ObjectType.Shop:
                ShopManager.Instance.OpenShop();
                break;
            case ObjectType.Enclosure:
                break;
            case ObjectType.Factory:
                break;
            default:
                break;
        }
    }

    #endregion

}
