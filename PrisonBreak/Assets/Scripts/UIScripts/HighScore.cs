using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplet;

    private void Awake()
    {
        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplet = transform.Find("highScoreEntryTemplate");

        entryTemplet.gameObject.SetActive(false);

        float templateHeight = 30f;
        for(int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplet, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryRectTransform.gameObject.SetActive(true);
        }
    }
}
