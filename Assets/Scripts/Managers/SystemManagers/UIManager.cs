using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ClickManager clickManager;

    public void OpenPanel()
    {
        clickManager.enabled = false; // Disable ClickManager
    }

    public void ClosePanel()
    {

        clickManager.enabled = true; // Enable ClickManager 
    }
}
