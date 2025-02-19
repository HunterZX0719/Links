using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LineRender : MonoBehaviour
{
    public GameObject LinePrefab;
    
    public Transform LineParent;
    public Transform LinePool;

    public async Task SetLine(Vector2 ST, Vector2 ED, float showTime)
    {
        ST += new Vector2(50, -50);
        ED += new Vector2(50, -50);
        Transform line;
        if (LinePool.childCount > 0)
        {
            line = LinePool.GetChild(0);
            line.SetParent(LineParent);
        }
        else
        {
            line = Instantiate(LinePrefab, LineParent).transform;
        }

        line.localPosition = (ST + ED) / 2;
        if (ST.x == ED.x)
        {
            line.GetComponent<RectTransform>().sizeDelta = new Vector2(8f, Mathf.Abs(ED.y - ST.y) + 8f);
        }
        else
        {
            line.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(ED.x - ST.x) + 8f, 8f);
        }
        line.GetComponent<Image>().color = Color.red;
        line.GetComponent<Image>().DOFade(0f, showTime);
        await Task.Delay((int)(showTime * 1000));
        line.SetParent(LinePool);
    }
    
}
