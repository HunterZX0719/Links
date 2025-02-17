using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
public class LinkBlock : MonoBehaviour
{
    public GameObject Bg;
    public Image Image;
    public Image Border;
    public Image Mask;
    
    private Button btn;
    public Action<LinkBlock> OnClickEvent;

    public bool IsActive => Bg.activeSelf;
    
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => OnClickEvent?.Invoke(this));
    }


    public void Set(Sprite sprite)
    {
        this.Image.sprite = sprite;
        Bg.SetActive(true);
        Border.color = Color.clear;
        Mask.color = Color.clear;
    }
    
    public void OnChose()
    {
        Border.color = Color.red;
    }

    public void OnDisChose()
    {
        Border.color = Color.clear;
    }

    public void OnHelp()
    {
        Mask.color = new Color(1f, 1f, 0f, 0.5f);
    }

    public void ResetState()
    {
        Border.color = Color.clear;
        Mask.color = Color.clear;
    }

    public void Hide()
    {
        Bg.SetActive(false);
    }
}
