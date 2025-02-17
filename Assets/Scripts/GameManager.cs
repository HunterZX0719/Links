public class GameManager : Singleton<GameManager>
{
    private DifficultySetting currentDifficulty;
    
    public void StartGame(DifficultySetting setting)
    {
        this.currentDifficulty = setting;
        PanelManager.Instance.ToGamePanel();
    }

    public DifficultySetting GetDifficultySetting()
    {
        return currentDifficulty;
    }

    public void SetCustomDifficulty()
    {
        PanelManager.Instance.ToCustomSettingPanel();
    }
    
    public void ToStartPanel()
    {
        PanelManager.Instance.ToStartPanel();
    }
}