using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    GameManger gameManager;
    public TextMeshProUGUI map1Time;
    public TextMeshProUGUI map2Time;
    public TextMeshProUGUI map3Time;
    public TextMeshProUGUI finalTime;
    void Start()
    {
        gameManager = FindObjectOfType<GameManger>();
    }

 
    void Update()
    {
        map1Time.text = " Time first map " + gameManager.map1.ToString();
        map2Time.text = " Time second map " + gameManager.map2.ToString();
        map3Time.text = " Time third map " + gameManager.map3.ToString();
        finalTime.text = " Final time " + gameManager.GetFinalTime();

    }
}
