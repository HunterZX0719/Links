using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    protected virtual void Awake()
    {
        
    }

    public virtual void Init()
    {
        
    }

    public virtual async Task Load()
    {
        
    }

    public virtual void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public virtual async Task Unload()
    {
        
    }
    
    public virtual void HidePanel()
    {
        gameObject.SetActive(false);
    }
}