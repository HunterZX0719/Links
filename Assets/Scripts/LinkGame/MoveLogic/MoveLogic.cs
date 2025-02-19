using System;
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
        int mid = size.x / 2;
        Vector2Int pos;
        Vector2Int posEmpty;
        
        pos = pos0;
        posEmpty = pos0;
        if (pos0.x < mid)
        {
            for (int i = pos0.x + 1; i < mid; i++)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    if (pos1 == pos)
                    {
                        pos1 = posEmpty;
                    }
                }
                posEmpty.x++;
            }
        }
        else
        {
            for (int i = pos0.x - 1; i >= mid; i--)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                    if (pos1 == pos)
                    {
                        pos1 = posEmpty;
                    }
                }
                posEmpty.x--;
            }
        }
        
        dataDic.Remove(pos1);
        pos = pos1;
        posEmpty = pos1;
        if (pos1.x < mid)
        {
            for (int i = pos1.x + 1; i < mid; i++)
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
            for (int i = pos1.x - 1; i >= mid; i--)
            {
                pos.x = i;
                if (dataDic.ContainsKey(pos))
                {
                    dataDic.Add(posEmpty, dataDic[pos]);
                    dataDic.Remove(pos);
                }
                posEmpty.x--;
            }
        }
    }
    
    private static void MoveIn(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos0,Vector2Int pos1)
    {
        dataDic.Remove(pos0);
        dataDic.Remove(pos1);
        int space0 = GetSpace(dataDic, size, pos0);
        int space1 = GetSpace(dataDic, size, pos1);
        if (space0 == space1)
        {
            MoveInBySpace(dataDic, size, space0);
        }
        else
        {
            MoveInBySpace(dataDic, size, space0);
            MoveInBySpace(dataDic, size, space1);
        }
    }

    private static int GetSpace(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, Vector2Int pos)
    {
        if (pos.x < size.x / 2 && pos.y < size.y / 2)
        {
            return 1;
        }
        else if (pos.x < size.x / 2 && pos.y >= size.y / 2)
        {
            return 2;
        }
        else if (pos.x >= size.x / 2 && pos.y >= size.y / 2)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
    
    private static void MoveInBySpace(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int size, int space)
    {
        Vector2Int pos = Vector2Int.zero;
        switch (space)
        {
            case 1:
                for (int i = size.x / 2 - 1; i > 0; i--)
                {
                    pos.x = i;
                    for (int j = size.y / 2 - 1; j > 0; j--)
                    {
                        pos.y = j;
                        MoveInOne(dataDic, pos, -1, -1, 0, 0);
                    }
                }
                break;
            case 2:
                for (int i = size.x / 2 - 1; i > 0; i--)
                {
                    pos.x = i;
                    for (int j = size.y / 2; j < size.y - 1; j++)
                    {
                        pos.y = j;
                        MoveInOne(dataDic, pos, -1, +1, 0, size.y);
                    }
                }
                break;
            case 3:
                for (int i = size.x / 2; i < size.x - 1; i++)
                {
                    pos.x = i;
                    for (int j = size.y / 2; j < size.y - 1; j++)
                    {
                        pos.y = j;
                        MoveInOne(dataDic, pos, +1, +1, size.x, size.y);
                    }
                }
                break;
            case 4:
                pos.x = size.x / 2;
                pos.y = size.y / 2 - 1;
                
                for (int i = size.x / 2; i < size.x - 1; i++)
                {
                    pos.x = i;
                    for (int j = size.y / 2 - 1; j > 0; j--)
                    {
                        pos.y = j;
                        MoveInOne(dataDic, pos, +1, -1, size.x, 0);
                    }
                }
                break;
            default:
                throw new Exception("象限错误！");
        }
    }

    private static void MoveInOne(Dictionary<Vector2Int, Sprite> dataDic, Vector2Int pos, int xAdd, int yAdd, int xLimit, int yLimit)
    {
        if (!dataDic.ContainsKey(pos))
        {
            var posMove = pos;
            for (int i = pos.x + xAdd; i != xLimit; i += xAdd)
            {
                posMove.x = i;
                if (dataDic.ContainsKey(posMove))
                {
                    dataDic.Add(pos, dataDic[posMove]);
                    dataDic.Remove(posMove);
                    return;
                }
            }
            posMove = pos;
            for (int i = pos.y + yAdd; i != yLimit; i += yAdd)
            {
                posMove.y = i;
                if (dataDic.ContainsKey(posMove))
                {
                    dataDic.Add(pos, dataDic[posMove]);
                    dataDic.Remove(posMove);
                    return;
                }
            }
        }
    }
}
