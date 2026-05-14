using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public RectTransform mainPanel;
    public RectTransform shopPanel;
    public RectTransform settingsPanel;

    private Vector2 offScreenRight = new Vector2(1920, 0);
    private Vector2 centerScreen = Vector2.zero;
    private Vector2 offScreenLeft = new Vector2(-1920, 0);

    void Start()
    {
        mainPanel.anchoredPosition = centerScreen;
        shopPanel.anchoredPosition = offScreenRight;
        settingsPanel.anchoredPosition = offScreenRight;
    }

    public void OpenShop()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(mainPanel, offScreenLeft));
        StartCoroutine(SlidePanel(shopPanel, centerScreen));
    }

    public void OpenMain()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(mainPanel, centerScreen));
        StartCoroutine(SlidePanel(shopPanel, offScreenRight));
        StartCoroutine(SlidePanel(settingsPanel, offScreenRight));
    }

    IEnumerator SlidePanel(RectTransform panel, Vector2 targetPos)
    {
        float duration = 0.3f;
        float elapsed = 0;
        Vector2 startPos = panel.anchoredPosition;

        while (elapsed < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        panel.anchoredPosition = targetPos;
    }
}