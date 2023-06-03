using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/Timer")]
public class ScriptbleObject : ScriptableObject
{


    // public grejeor
    float time;
    float stopTime;
    Stopwatch stopWatch;

    // ha en reset
    public void StartTimer()
    {
        stopWatch = Stopwatch.StartNew();
        time = Time.time;   
    }

    public float StopTimer()
    {
        stopWatch.Stop();
        return (float) stopWatch.Elapsed.TotalSeconds;
    }

    public void PauseTimer()
    {
        stopWatch.Stop();

    }
    public void CountinueTimer()
    {
        stopWatch.Start();

    }


}
