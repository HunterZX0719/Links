using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveLogic
{
    public static void Move(LevelType levelType,Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos0, Vector2Int pos1)
    {
        switch (levelType)
        {
            case LevelType.新芽:
            case LevelType.百花齐放:
                MoveNomal(dataDic,size,pos0,pos1);
                break;
            case LevelType.地心引力:
                MoveDown(dataDic,size,pos0,pos1);
                break;
            case LevelType.飘向天空:
                MoveUp(dataDic,size,pos0,pos1);
                break;
            case LevelType.开天辟地:
                MoveOut(dataDic,size,pos0,pos1);
                break;
            case LevelType.黑洞:
                MoveIn(dataDic,size,pos0,pos1);
                break;
        }
    }

    private static void MoveNomal(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos0,Vector2Int pos1)
    {
        dataDic.Remove(pos0);
        dataDic.Remove(pos1);
    }
    
    //X横Y竖
    private static void MoveDown(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos0,Vector2Int pos1)
    {
        dataDic.Remove(pos0);
        dataDic.Remove(pos1);

        Vector2Int pos;
        Vector2Int posEmpty;
        if (pos0.y != pos1.y)
        {
            pos = pos0;
            posEmpty = pos0;
            for (int i = pos0.x - 1; i > 0; i--)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    posEmpty = pos;
                }
                else
                {
                    break;
                }
            }
            pos = pos1;
            posEmpty = pos1;
            for (int i = pos1.x - 1; i > 0; i--)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    posEmpty = pos;
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            if (pos0.x < pos1.x)
            {
                pos = pos1;
                posEmpty = pos1;
            }
            else
            {
                pos = pos0;
                posEmpty = pos0;
            }
            int emptyCount = 0;
            for (int i = Mathf.Max(pos0.x,pos1.x) - 1; i > 0; i--)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    posEmpty.x--;
                }
                else
                {
                    emptyCount++;
                    if (emptyCount >= 2)
                    {
                        break;
                    }
                }
            }
        }
    }
    
    private static void MoveUp(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos0,Vector2Int pos1)
    {
        dataDic.Remove(pos0);
        dataDic.Remove(pos1);
        Vector2Int pos;
        Vector2Int posEmpty;
        if (pos0.y != pos1.y)
        {
            pos = pos0;
            posEmpty = pos0;
            for (int i = pos0.x + 1; i < size.x-1; i++)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    posEmpty = pos;
                }
                else
                {
                    break;
                }
            }
            pos = pos1;
            posEmpty = pos1;
            for (int i = pos1.x + 1; i < size.x-1; i++)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    posEmpty = pos;
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            if (pos0.x > pos1.x)
            {
                pos = pos1;
                posEmpty = pos1;
            }
            else
            {
                pos = pos0;
                posEmpty = pos0;
            }
            int emptyCount = 0;
            for (int i = Mathf.Min(pos0.x,pos1.x) + 1; i < size.x-1; i++)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    posEmpty.x++;
                }
                else
                {
                    emptyCount++;
                    if (emptyCount >= 2)
                    {
                        break;
                    }
                }
            }
        }
    }
    
    private static void MoveOut(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos0,Vector2Int pos1)
    {
        dataDic.Remove(pos0);
        dataDic.Remove(pos1);
        int mid = size.x / 2;
        Vector2Int pos;
        Vector2Int posEmpty;
        if (pos0.x < mid)
        {
            pos = pos0;
            posEmpty = pos0;
            for (int i = pos0.x + 1; i < mid; i++)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                }
                posEmpty.x++;
            }
        }
        else
        {
            
        }
    }
    
    private static void MoveIn(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos0,Vector2Int pos1)
    {
        dataDic.Remove(pos0);
        dataDic.Remove(pos1);
    }
}
