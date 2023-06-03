using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    winCon winCondision;
    public int mapCount;
    public float map1;
    public float map2;
    public float map3;
    [SerializeField] ScriptbleObject timer;
    private void Awake()
    {
        winCondision = FindObjectOfType<winCon>();
    }
    
    
    public void Timing()
    {
        winCondision = GetComponent<winCon>();
       
    }
    public float GetFinalTime()
    {
        return map1 + map2 + map3;
    }
}
