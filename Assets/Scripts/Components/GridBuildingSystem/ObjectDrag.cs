using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 startPos;
    private float deltaX, deltaY;

    #region Unity Method

    private void Start()
    {
        startPos = Input.mousePosition;
        startPos = Camera.main.ScreenToWorldPoint(startPos);

        deltaX = startPos.x - transform.position.x;
        deltaY = startPos.y - transform.position.y;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = new Vector3(mousePos.x - deltaX, mousePos.y - deltaY);
        Vector3Int cellPos = GridBuildingSystem.Instance.gridLayout.WorldToCell(pos);
        transform.position = GridBuildingSystem.Instance.gridLayout.CellToLocalInterpolated(cellPos);
        GridBuildingSystem.Instance.FollowBuilding(transform.GetComponent<ObjectPlaceable>());
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<ObjectPlaceable>().CheckPlacement();
            //ObjectManage.Instance.SaveObject(gameObject.GetComponent<ObjectPlaceable>());
            Destroy(this);
        }
    }

    #endregion

}
