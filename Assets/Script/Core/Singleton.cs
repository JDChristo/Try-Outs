using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T s_instance;
    private static bool s_isInitialized;

    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();

                if (!s_isInitialized)
                {
                    s_isInitialized = true;
                    s_instance.Init();
                }
            }
            return s_instance;
        }
    }

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this as T;
        }
        else if (s_instance != this)
        {
            DestroyImmediate(this);
            return;
        }
        if (!s_isInitialized)
        {
            DontDestroyOnLoad(gameObject);
            s_isInitialized = true;
            s_instance.Init();
        }
    }


    public abstract void Init();
}
