using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCon2 : MonoBehaviour
{

    float score;
    public float targetTime = 0f;
    GameManger gameManager;
    [SerializeField] ScriptableObject scriptableObject;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManger>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            score = targetTime;
            //gameManger.Timing();
            gameManager.map2 = targetTime;
            
            gameManager.mapCount++;
            Loader.Load(Loader.Scene.Bana3);

        }
    }


    void Update()
    {
        targetTime += Time.deltaTime;
    }

    public float GetTime()
    {
        return targetTime;
    }
}
