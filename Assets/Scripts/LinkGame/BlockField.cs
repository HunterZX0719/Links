using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(GridLayoutGroup))]
public class BlockField : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Vector2Int AreaSize;
    private GridLayoutGroup layoutGroup;
    private Dictionary<Vector2Int, LinkBlock> blockDic;
    private Dictionary<LinkBlock, Vector2Int> posDic;
    
    private LinkBlock lastBlock;
    public Action<Vector2Int, Vector2Int> OnCompareBlockEvent;

    public float LinkLineShowTime;
    private bool allowClick;
    private void Awake()
    {
        layoutGroup = GetComponent<GridLayoutGroup>();
        blockDic = new Dictionary<Vector2Int, LinkBlock>();
        posDic = new Dictionary<LinkBlock, Vector2Int>();

        layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layoutGroup.constraintCount = AreaSize.y;
        
        GenerateBlocks();
        allowClick = true;
    }

    private void GenerateBlocks()
    {
        for (int i = 0; i < AreaSize.x; i++)
        {
            for (int j = 0; j < AreaSize.y; j++)
            {
                GameObject block = Instantiate(BlockPrefab, layoutGroup.transform);
                LinkBlock linkBlock = block.GetComponent<LinkBlock>();
                blockDic.Add(new Vector2Int(i,j), linkBlock);
                posDic.Add(linkBlock, new Vector2Int(i,j));
                linkBlock.OnClickEvent += OnClickBlock;
            }
        }
        lastBlock = null;
    }

    public void SetBlocks(Dictionary<Vector2Int, Sprite> dataDic)
    {
        foreach (var key in blockDic.Keys)
        {
            if (dataDic.ContainsKey(key))
            {
                blockDic[key].Set(dataDic[key]);
            }
            else
            {
                blockDic[key].Hide();
            }
        }
        lastBlock = null;
    }

    public async void SetBlocks(List<Vector2Int> path, Dictionary<Vector2Int, Sprite> dataDic)
    {
        allowClick = false;
        //todo:显示连接线
        
        await Task.Delay((int)(LinkLineShowTime * 1000));
        SetBlocks(dataDic);
        allowClick = true;
    }

    private void OnClickBlock(LinkBlock linkBlock)
    {
        if(!allowClick || !linkBlock.IsActive) return;
        if (lastBlock == null)
        {
            linkBlock.OnChose();
            lastBlock = linkBlock;
        }
        else if(lastBlock == linkBlock)
        {
            linkBlock.OnDisChose();
            lastBlock = null;
        }
        else
        {
            OnCompareBlockEvent?.Invoke(posDic[lastBlock], posDic[linkBlock]);
            lastBlock.OnDisChose();
            lastBlock = null;
        }
    }

    public void SetHelp(Vector2Int pos0, Vector2Int pos1)
    {
        blockDic[pos0].OnHelp();
        blockDic[pos1].OnHelp();
    }
}
