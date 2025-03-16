using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    static bool isInitialized = false;
    private void Awake()
    {
        if (isInitialized)
            return;
        DataManager.Instance.Init();
        isInitialized = true;
    }
}
