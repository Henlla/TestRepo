using UnityEngine;
using UnityEngine.UI;

public class ChopMenuController : MonoBehaviour
{
    public Button chopButton;  // Nút "Chặt Cây"
    public Button deselectButtonChop;  // Nút "Bỏ chọn" trong ChopPanel
    public Button cancelChopButton;  // Nút "Hủy Chặt"
    public Button deselectButtonCancelChop;  // Nút "Bỏ chọn" trong CancelChopPanel

    private void Start()
    {
        chopButton.onClick.AddListener(OnChopButtonClicked);
        deselectButtonChop.onClick.AddListener(OnDeselectButtonClicked);
        cancelChopButton.onClick.AddListener(OnCancelChopButtonClicked);
        deselectButtonCancelChop.onClick.AddListener(OnDeselectButtonClicked);
    }

    private void OnChopButtonClicked()
    {
        if (Tree.selectedTree != null && Tree.selectedTree.treeRecord.treeState == 0)
        {
            Tree.selectedTree.StartChopping(10);  // Bắt đầu chặt cây
        }
    }

    private void OnCancelChopButtonClicked()
    {
        if (Tree.selectedTree != null && Tree.selectedTree.treeRecord.treeState == 1)
        {
            Tree.selectedTree.CancelChopping();  // Hủy quá trình chặt cây
        }
    }

    private void OnDeselectButtonClicked()
    {
        if (Tree.selectedTree != null)
        {
            Tree.selectedTree.DeselectTree();  // Hủy chọn cây
        }
    }
}
