using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winCon : MonoBehaviour
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
        if(collision.gameObject.CompareTag("Player"))
        {            
            score = targetTime;
            //gameManger.Timing();
            if (gameManager.mapCount == 0)
            {
                gameManager.map1 = targetTime;
            }
            else if (gameManager.mapCount == 1)
            {
                gameManager.map2 = targetTime;
            }
            else if (gameManager.mapCount == 2)
            {
                gameManager.map3 = targetTime;
            }
            gameManager.mapCount++;
            Loader.Load(Loader.Scene.Bana2);
           
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
