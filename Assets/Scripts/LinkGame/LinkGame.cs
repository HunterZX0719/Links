using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[DisallowMultipleComponent]
public class LinkGame : MonoBehaviour
{
    public BlockField BlockField;
    private LinkGameController controller;
    private LevelType curLevelType;
    
    public Action<LevelType> OnLevelChangeEvent;
    public Action OnGameFinish;
    
    public int point;
    public int helpPoint = 2;
    public int resortPoint = 10;
    
    private void Awake()
    {
        controller = new LinkGameController();
        controller.OnAllClearEvent += OnAllClear;
        BlockField.OnCompareBlockEvent += OnCompareBlock;
    }

    public void InitGame()
    {
        curLevelType = 0;
        controller.InitGame(GameManager.Instance.GetDifficultySetting().RepeatRate,BlockField.AreaSize);
        point = GameManager.Instance.GetDifficultySetting().StartRemaind;
        GenerateGame();
    }

    private void OnAllClear()
    {
        if (Enum.IsDefined(typeof(LevelType), curLevelType + 1))
        {
            curLevelType++;
            GenerateGame();
        }
        else
        {
            OnGameFinish?.Invoke();
        }
    }

    private void GenerateGame()
    {
        controller.GenerateGame(curLevelType);
        BlockField.SetBlocks(controller.DataDic);
        OnLevelChangeEvent?.Invoke(curLevelType);
    }

    private void OnCompareBlock(Vector2Int a, Vector2Int b)
    {
        var path = controller.Compare(a, b);
        if (path != null)
        {
            BlockField.SetBlocks(path, controller.DataDic);
        }
    }

    public void GetHelp()
    {
        point -= helpPoint;
        var help = controller.GetHelp();
        BlockField.SetHelp(help[0], help[1]);
    }

    public void Resort()
    {
        point -= resortPoint;
        controller.Resort();
        BlockField.SetBlocks(controller.DataDic);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            var help = controller.GetHelp();
            BlockField.SetHelp(help[0], help[1]);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            controller.Resort();
            BlockField.SetBlocks(controller.DataDic);
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            OnAllClear();
        }
    }
}
