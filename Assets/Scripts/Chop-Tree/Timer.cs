using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;

    private int remainingDuration;
    private Tree currentTree;

    public void Begin(int seconds)
    {
        remainingDuration = seconds;
        StartCoroutine(UpdateTimer());
    }

    public void SetTree(Tree tree)
    {
        currentTree = tree;
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, 10, remainingDuration);

            remainingDuration--;
            currentTree.treeRecord.remainingChopTime = remainingDuration;

            yield return new WaitForSeconds(1f);
        }

        currentTree.OnChopComplete();
        Destroy(gameObject);
    }
}
