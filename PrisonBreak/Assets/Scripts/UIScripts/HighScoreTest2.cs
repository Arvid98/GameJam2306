using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTest2 : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void ShowPannel()
    {
        panel.SetActive(true);
    }
    public void ClosePannel()
    {
        panel.SetActive(false);
    }
}
