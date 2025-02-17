using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LinkGamePathWayFinder
{
    internal class PathPool
    {
        private List<Path> pathList;
        private Stack<Path> pathPool;
        
        public PathPool()
        {
            pathList = new List<Path>();
            pathPool = new Stack<Path>();
        }

        public void Add(Path prePath,Vector2Int pos,Vector2Int target)
        {
            Path newPath;
            if (pathPool.Count > 0)
            {
                newPath = pathPool.Pop();
                newPath.SetPath(prePath, pos, target);
            }
            else
            {
                newPath = new Path(prePath, pos, target);
            }
            pathList.Add(newPath);
        }

        public void Remove()
        {
            
        }
    
    }
}

