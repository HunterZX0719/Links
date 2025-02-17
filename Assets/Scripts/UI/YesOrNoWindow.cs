using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class YesOrNoWindow : MonoBehaviour
{
    public Text text;
    private Action YesAction;

    public Button YesButton;
    public Button NoButton;

    private void Awake()
    {
        if (YesButton != null)
        {
            YesButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                YesAction?.Invoke();
            });
        }

        if (NoButton != null)
        {
            NoButton.onClick.AddListener(() => gameObject.SetActive(false));
        }
        
        gameObject.SetActive(false);
    }

    public void Show(string text, Action yesAction)
    {
        if (this.text != null)
        {
            this.text.text = text;
        }
        this.YesAction = yesAction;
        gameObject.SetActive(true);
    }
}
