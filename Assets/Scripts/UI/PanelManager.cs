using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    public StartPanel StartPanel;
    public GamePanel GamePanel;
    public CustomSettingPanel CustomSettingPanel;
    public LoadingPanel LoadingPanel;
    private List<Panel> panels = new List<Panel>();
    
    //private Dictionary<string, Panel> panelDic = new Dictionary<string, Panel>();
    private Panel currentPanel;
    
    private void Awake()
    {
        base.Awake();
        /*foreach (var panel in Panels)
        {
            panelDic.Add(panel.gameObject.name,panel);
        }*/
        panels.Add(StartPanel);
        panels.Add(CustomSettingPanel);
        panels.Add(GamePanel);
        panels.Add(LoadingPanel);
        foreach (var panel in panels)
        {
            panel.ShowPanel();
            panel.HidePanel();
        }
        Init();
    }

    private async void Init()
    {
        foreach (var panel in panels)
        {
            panel.Init();
        }
        await StartPanel.Load();
        StartPanel.ShowPanel();
        currentPanel = StartPanel;
    }

    public async Task ShowLodingPanel()
    {
        await LoadingPanel.Load();
        LoadingPanel.ShowPanel();
    }

    public async Task HideLodingPanel()
    {
        await LoadingPanel.Unload();
        LoadingPanel.HidePanel();
    }
    
    public async void ToGamePanel()
    {
        await currentPanel.Unload();
        currentPanel.HidePanel();
        
        await ShowLodingPanel();
        
        await GamePanel.Load();
        GamePanel.ShowPanel();

        await HideLodingPanel();
        currentPanel = GamePanel;
    }
    
    public async void ToCustomSettingPanel()
    {
        await currentPanel.Unload();
        currentPanel.HidePanel();
        await CustomSettingPanel.Load();
        CustomSettingPanel.ShowPanel();
        currentPanel = CustomSettingPanel;
    }

    public async void ToStartPanel()
    {
        await currentPanel.Unload();
        currentPanel.HidePanel();
        await StartPanel.Load();
        StartPanel.ShowPanel();
        currentPanel = StartPanel;
    }
    
}
/*
public enum PanelType
{
    StartPanel,
    CustomSettingsPanel,
    GamePanel
}*/