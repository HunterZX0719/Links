
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LoadingPanel : Panel
{
    public Animator ani;
    public CanvasGroup canvasGroup;
    
    public override async Task Load()
    {
        canvasGroup.alpha = 1;
        ani.Play("Loading", 0, 0);
    }

    public override async Task Unload()
    {
        canvasGroup.DOFade(0, 0.5f);
        await Task.Delay(500);
        ani.Play("C");
    }
}