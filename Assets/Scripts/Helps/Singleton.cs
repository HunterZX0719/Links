using System;
using Unity.VisualScripting;
using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance => instance;

    public static bool Initialized => instance != null;
    
    protected virtual void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = (T)this;
        }
    }

    protected void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}