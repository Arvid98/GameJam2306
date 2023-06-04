using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public enum Scene
    {
        Loading, Menu, Bana1, Bana2, ExitScene, LosingScene
    }
    private static Action onLoaderCallBack;

    public static void Load(Scene scene)
    {
        //set the loader callback action to load the target scene
        onLoaderCallBack = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        // load the Loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());

    }

    public static void LoaderCallBack()
    {
        // Triger after the first Update which lets the scene refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }

    }
}
