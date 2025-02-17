using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Panel
{
    public Button EasyButton;
    public Button NormalButton;
    public Button HardButton;
    public Button HellButton;
    public Button CustomButton;
    public Button QuitButton;

    public override void Init()
    {
        base.Init();
        EasyButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame(DifficultyHelp.GetSetting(Difficulty.Easy));
        });
        NormalButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame(DifficultyHelp.GetSetting(Difficulty.Normal));
        });
        HardButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame(DifficultyHelp.GetSetting(Difficulty.Hard));
        });
        HellButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame(DifficultyHelp.GetSetting(Difficulty.Hell));
        });
        CustomButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetCustomDifficulty();
        });
        QuitButton.onClick.AddListener(Application.Quit);
    }
}