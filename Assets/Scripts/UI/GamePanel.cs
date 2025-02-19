using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    public RectTransform TopArea;
    public TimeCounter TimeCounter;
    public Text LevelText;
    public Text PointText;
    
    public Button QuitButton;
    public Button HelpButton;
    public Button SortButton;
    public Button PauseButton;
    
    public YesOrNoWindow WindowHelp; 
    public YesOrNoWindow WindowOvetTime; 
    public YesOrNoWindow WindowFinish; 
    public YesOrNoWindow WindowNeedPoint;
    public YesOrNoWindow WindowPause;
    
    public LinkGame LinkGame;
    
    protected override void Awake()
    {
        base.Awake();
        Rect safeArea = Screen.safeArea;
        float topDis = (Screen.height - safeArea.height) / Screen.height * 1920f;
        TopArea.anchoredPosition -= new Vector2(0f, topDis);
    }

    public override void Init()
    {
        base.Init();
        QuitButton.onClick.AddListener(() => WindowHelp.Show("返回主界面?", OnQuit));
        HelpButton.onClick.AddListener(OnHelp);
        SortButton.onClick.AddListener(OnResort);
        PauseButton.onClick.AddListener(OnPause);
        LinkGame.OnLevelChangeEvent += OnLevelChange;
        LinkGame.OnGameFinish += OnGameFinish;
    }

    private void OnHelp()
    {
        if (LinkGame.point < LinkGame.helpPoint)
        {
            WindowNeedPoint.Show($"点数不足以获得提示!\r\n<size=32>剩余{LinkGame.point}点点数，需要花费{LinkGame.helpPoint}点点数</size>", null);
        }
        else
        {
            WindowHelp.Show($"是否需要提示?\r\n<size=32>将花费{LinkGame.helpPoint}点点数</size>", ()=>
            {
                LinkGame.GetHelp();
                PointText.text = LinkGame.point.ToString();
            });
        }
    }

    private void OnResort()
    {
        if (LinkGame.point < LinkGame.resortPoint)
        {
            WindowNeedPoint.Show($"点数不足以重新排列!\r\n<size=32>剩余{LinkGame.point}点点数，需要花费{LinkGame.resortPoint}点点数</size>", null);
        }
        else
        {
            WindowHelp.Show($"是否重新排列?\r\n<size=32>将花费{LinkGame.resortPoint}点点数</size>", () =>
            {
                LinkGame.Resort();
                PointText.text = LinkGame.point.ToString();
            });
        }
    }

    private void OnPause()
    {
        TimeCounter.StopCount();
        LinkGame.Pause();
        WindowPause.Show(Continue);
    }

    private void Continue()
    {
        TimeCounter.StartCount();
        LinkGame.Continue();
    }

    public override Task Load()
    {
        var setting = GameManager.Instance.GetDifficultySetting();
        LinkGame.InitGame();
        TimeCounter.SetTimeCounter(setting.MaxTime, OnTimeZero);
        return base.Load();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        TimeCounter.StartCount();
    }

    private void OnTimeZero()
    {
        WindowOvetTime.Show("时间到了!!!\r\n<size=32>You Are Spider</size>", () => GameManager.Instance.ToStartPanel());
    }

    private void OnLevelChange(LevelType newLevelType)
    {
        TimeCounter.RewardTime(GameManager.Instance.GetDifficultySetting().RewardTimeRate);
        LevelText.text = newLevelType.ToString();
        PointText.text = LinkGame.point.ToString();
    }

    private void OnGameFinish()
    {
        TimeCounter.StopCount();
        var time = TimeCounter.GetUsedTime();
        WindowFinish.Show($"成功通关!!!\r\n<size=32>用时{(time/60).ToString("00")}:{(time%60).ToString("00")}</size>", () => GameManager.Instance.ToStartPanel());
    }

    private void OnQuit()
    {
        TimeCounter.StopCount();
        GameManager.Instance.ToStartPanel();
    }
}