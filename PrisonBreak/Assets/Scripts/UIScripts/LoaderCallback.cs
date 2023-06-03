using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUppdate = true;

    private void Update()
    {
        if (isFirstUppdate)
        {
            isFirstUppdate = false;
            Loader.LoaderCallBack();
        }
    }
}
