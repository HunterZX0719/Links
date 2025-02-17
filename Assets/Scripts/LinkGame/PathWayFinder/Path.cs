using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LinkGamePathWayFinder
{
    internal class Path
    {
        private Vector2Int pos;
        public Vector2Int Pos => pos;
        private int usedNum;
        public int UsedNum => usedNum;
        private int needNum;
        public int NeedNum => needNum;
        public int SumNum => usedNum + needNum;
    
        private int turnTime;
        public int TurnTime => turnTime;
        
        private Path prePath;
        private Vector2Int direction;
        public Vector2Int Direction => direction;
        
        public Path(Path prePath,Vector2Int pos,Vector2Int target)
        {
            SetPath(prePath,pos,target);
        }

        public void SetPath(Path prePath, Vector2Int pos, Vector2Int target)
        {
            
            this.prePath = prePath;
            this.pos = pos;
            needNum = Mathf.Abs(pos.x - target.x) + Mathf.Abs(pos.y - target.y);
            if (prePath != null)
            {
                usedNum = prePath.usedNum + 1;
                direction = pos - prePath.pos;
                turnTime = prePath.turnTime;
                if (direction != prePath.direction && prePath.direction != Vector2Int.zero)
                {
                    turnTime++;
                }
            }
            else
            {
                usedNum = 0;
                turnTime = 0;
                direction = Vector2Int.zero;
            }
        }
    
        public List<Vector2Int> GetPath()
        {
            List<Vector2Int> path;
            if (prePath == null)
            {
                path = new List<Vector2Int>();
            }
            else
            {
                path = prePath.GetPath();
            }
            path.Add(pos);
            return path;
        }

        public static Path ComparePath(Path path0, Path path1)
        {
            if (path0.SumNum < path1.SumNum)
            {
                return path0;
            }
            else if (path0.SumNum == path1.SumNum)
            {
                if (path0.NeedNum < path1.NeedNum)
                {
                    return path0;
                }
                else if (path0.NeedNum == path1.NeedNum)
                {
                    if (path0.UsedNum < path1.UsedNum)
                    {
                        return path0;
                    }
                    else if (path0.UsedNum == path1.UsedNum)
                    {
                        if (path0.TurnTime < path1.TurnTime)
                        {
                            return path0;
                        }
                    }
                }
            }
            return path1;
        }
    }
}

