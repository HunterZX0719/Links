using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinkGamePathWayFinder
{
    public static class PathWayFinder
    {
        private static List<Vector2Int> DirectionList = new List<Vector2Int>()
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
        
        public static List<Vector2Int> FindPath(Dictionary<Vector2Int,Sprite> dataDic,Vector2Int fieldSize, Vector2Int startPoint, Vector2Int endPoint)
        {
            List<Path> pathList = new List<Path>();
            Path currentPath = new Path(null, startPoint, endPoint);
            pathList.Add(currentPath);
            while (pathList.Count > 0)
            {
                pathList.Remove(currentPath);
                foreach (var dir in DirectionList)
                {
                    Vector2Int newPos = currentPath.Pos + dir;
                    if (newPos == endPoint || newPos.x>=0 && newPos.x<fieldSize.x && newPos.y>=0 && newPos.y<fieldSize.y && !dataDic.ContainsKey(newPos) && dir != -currentPath.Direction)
                    {
                        Path newPath = new Path(currentPath, currentPath.Pos + dir, endPoint);
                        if (newPath.TurnTime < 3)
                        {
                            if (newPos == endPoint)
                            {
                                return newPath.GetPath();
                            }
                            pathList.Add(newPath);
                        }
                    }
                }
                
                if (pathList.Count == 0) return null;
                currentPath = pathList[0];
                for (int i = 1; i < pathList.Count; i++)
                {
                    currentPath = Path.ComparePath(currentPath, pathList[i]);
                }
            }
            return null;
        }
    }
}

