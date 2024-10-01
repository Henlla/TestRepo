using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    private bool EditMode;
    private bool DecorMode;

    #region Toggle Edit map

    public void ToggleEditMode()
    {
        EditMode = !EditMode;
        if (EditMode)
        {
            GridBuildingSystem.Instance.ChangeTempTileMapAlpha(255);
            GridBuildingSystem.Instance.ShowGrid(true);
        }
        else
        {
            GridBuildingSystem.Instance.ChangeTempTileMapAlpha(0);
            GridBuildingSystem.Instance.ShowGrid(false);
        }
    }

    public void TurnOnDecorMode()
    {
        DecorMode = true;
        GridBuildingSystem.Instance.ChangeTempTileMapAlpha(255);
        GridBuildingSystem.Instance.ShowGrid(true);
        CameraMovement.Instance.SetCameraMoveDisable(true);
        UIManager.Instance.EditModeButton.SetActive(false);
        UIManager.Instance.DecoratePanel.SetActive(true);
    }

    public void TurnOffDecorMode()
    {
        DecorMode = false;
        GridBuildingSystem.Instance.ChangeTempTileMapAlpha(0);
        GridBuildingSystem.Instance.ShowGrid(false);
        CameraMovement.Instance.SetCameraMoveDisable(false);
        UIManager.Instance.EditModeButton.SetActive(true);
        UIManager.Instance.DecoratePanel.SetActive(false);
    }

    public void TurnOnEditMode()
    {
        GridBuildingSystem.Instance.ChangeTempTileMapAlpha(255);
        GridBuildingSystem.Instance.ShowGrid(true);
        EditMode = true;
    }

    public void TurnOffEditMode()
    {
        GridBuildingSystem.Instance.ChangeTempTileMapAlpha(0);
        GridBuildingSystem.Instance.ShowGrid(false);
        EditMode = false;
    }

    #endregion

    #region Check edit state

    public bool IsDecorEditMode()
    {
        return DecorMode;
    }

    public bool IsMapEditMode()
    {
        return EditMode;
    }
    #endregion


    public void CloseShop()
    {
        ShopManager.Instance.CloseShop();
    }
}
