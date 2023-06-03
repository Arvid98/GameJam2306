using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void ChangeScene()
    {
        Debug.Log("Clicked Play game");
        Loader.Load(Loader.Scene.Bana1);
    }
   

    public void ChangeSceneToMenu()
    {
        Loader.Load(Loader.Scene.Menu);
    }

    public void DoExitGame()
    {
        Application.Quit();
    }
}
