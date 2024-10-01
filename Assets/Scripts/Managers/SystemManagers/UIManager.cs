using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Config Shop Panel")]
    public GameObject ShopPanel;
    public Transform ShopContainer;

    [Header("Edit Mode Button")]
    public GameObject EditModeButton;

    [Header("Decorate Confirm")]
    public GameObject DecoratePanel;

    private ClickManager clickManager;

    public void OpenPanel()
    {
        clickManager.enabled = false; // Disable ClickManager
    }

    public void ClosePanel()
    {

        clickManager.enabled = true; // Enable ClickManager 
    }
}
