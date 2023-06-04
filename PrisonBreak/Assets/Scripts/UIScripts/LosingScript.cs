using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LosingScript : MonoBehaviour
{
    GameManger gameManager;
    int oldMapCount;

    public TextMeshProUGUI text;
    
    private float time;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManger>();
        ResetStats();
    }

    void ResetStats()
    {
        gameManager.map1 = 0;
        gameManager.map2 = 0;
        gameManager.map3 = 0;
        oldMapCount = gameManager.mapCount;
        gameManager.mapCount = 0;
    }

    void DisplayText()
    {
        if(oldMapCount == 0)
        {
            text.text = " Try better you only got to the brake rooms";
        }
        else if (oldMapCount == 1) 
        {
            text.text = "So close to the outer yard";
        }
        else if (oldMapCount == 2)
        {
            text.text = "You got the smell of freedom";
        }
    }

    private void Update()
    {
        DisplayText();
        time += Time.deltaTime;

        if(time >= 5)
        {
            Loader.Load(Loader.Scene.Menu);
        }
    }
}
