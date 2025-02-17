using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LinkGamePathWayFinder;
using Random = Unity.Mathematics.Random;

public class LinkGameController
{
    private Dictionary<Vector2Int,Sprite> dataDic;
    public Dictionary<Vector2Int,Sprite> DataDic => dataDic;
    private float repeatRate;
    private Vector2Int size;
    private LevelType levelType;
    public Action OnAllClearEvent;
    
    private Dictionary<Sprite, List<Vector2Int>> checkResortDic;
    
    public LinkGameController()
    {
        dataDic = new Dictionary<Vector2Int,Sprite>();
        checkResortDic = new Dictionary<Sprite, List<Vector2Int>>();
    }

    public void InitGame(float repeatRate, Vector2Int size)
    {
        this.repeatRate = repeatRate;
        this.size = size;
    }
    
    public void GenerateGame(LevelType levelType)
    {
        this.levelType = levelType;
        if (levelType == 0)
        {
            int space = size.x / 4;
            int spaceB = space;
            if ((size.x - 2 * space) % 2 != 0)
            {
                spaceB = space + 1;
            }
            GenerateGame(new Vector2Int(space, 1), new Vector2Int(size.x - spaceB - 1, size.y - 2));
        }
        else
        {
            GenerateGame(new Vector2Int(1, 1), new Vector2Int(size.x - 2, size.y - 2));
        }
    }

    private void GenerateGame(Vector2Int startPoint, Vector2Int endPoint)
    {
        Debug.Log($"--------开始生成游戏--------");
        dataDic.Clear();
        int pairNum = (endPoint.x - startPoint.x + 1) * (endPoint.y - startPoint.y + 1);
        if (pairNum % 2 != 0)
        {
            throw new Exception($"方块数量为单数，无法生成游戏。startPoint:{startPoint},endPoint:{endPoint}");
        }

        pairNum /= 2;
        Debug.Log($"总对数{pairNum}");
        int repeatNum = (int)(pairNum * repeatRate);
        Debug.Log($"重复数{repeatNum}");
        pairNum -= repeatNum;
        Debug.Log($"非重复{pairNum}");
        var spriteList = FlowerSpriteManager.Instance.GetUnrepeatedRandomList(pairNum);
        while (repeatNum > 0)
        {
            spriteList.Add(spriteList[UnityEngine.Random.Range(0, spriteList.Count)]);
            repeatNum--;
        }
        
        spriteList.AddRange(spriteList);
        for (int i = startPoint.x; i <= endPoint.x; i++)
        {
            for (int j = startPoint.y; j <= endPoint.y; j++)
            {
                int random = UnityEngine.Random.Range(0, spriteList.Count);
                dataDic.Add(new Vector2Int(i, j), spriteList[random]);
                spriteList.RemoveAt(random);
            }
        }
        Debug.Log($"未使用图片数：{spriteList.Count}");
        Debug.Log($"--------结束生成游戏--------");
    }

    public List<Vector2Int> Compare(Vector2Int a, Vector2Int b)
    {
        List<Vector2Int> result = null;
        if (dataDic[a] == dataDic[b])
        {
            result = PathWayFinder.FindPath(dataDic, size, a, b);
            if (result != null)
            {
                MoveLogic.Move(levelType,dataDic, size, a, b);
                CheckResort();
            }
        }

        if (dataDic.Count == 0)
        {
            OnAllClearEvent?.Invoke();
        }
        return result;
    }

    private void CheckResort()
    {
        if(dataDic.Count == 0) return;
        checkResortDic.Clear();
        foreach (var data in dataDic)
        {
            if (checkResortDic.ContainsKey(data.Value))
            {
                foreach (var v in checkResortDic[data.Value])
                {
                    if (PathWayFinder.FindPath(dataDic, size, data.Key, v) != null) return;
                }
                checkResortDic[data.Value].Add(data.Key);
            }
            else
            {
                checkResortDic.Add(data.Value, new List<Vector2Int>(){data.Key});
            }
        }
        Resort();
    }

    public void Resort()
    {
        Debug.Log("重新排列");
        if(dataDic.Count == 0) return;
        List<Sprite> spriteList = dataDic.Values.ToList();
        List<Vector2Int> vectorList = dataDic.Keys.ToList();
        for (int i = 0; i < vectorList.Count; i++)
        {
            dataDic[vectorList[i]] = spriteList[(i + 3) % spriteList.Count];
        }
        CheckResort();
    }

    public List<Vector2Int> GetHelp()
    {
        if(dataDic.Count == 0) return null;
        checkResortDic.Clear();
        List<Vector2Int> result = null;
        foreach (var data in dataDic)
        {
            if (checkResortDic.ContainsKey(data.Value))
            {
                foreach (var v in checkResortDic[data.Value])
                {
                    result = PathWayFinder.FindPath(dataDic, size, data.Key, v);
                    if (result != null)
                    {
                        return new List<Vector2Int>() { data.Key, v };
                    }
                }
                checkResortDic[data.Value].Add(data.Key);
            }
            else
            {
                checkResortDic.Add(data.Value, new List<Vector2Int>(){data.Key});
            }
        }
        return null;
    }
}
