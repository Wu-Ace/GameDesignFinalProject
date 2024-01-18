using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestoryOnLoad : MonoBehaviour
{
    private static DoNotDestoryOnLoad instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}