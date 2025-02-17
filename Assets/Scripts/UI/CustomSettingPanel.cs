using System.Threading.Tasks;
using UnityEngine.UI;

public class CustomSettingPanel : Panel
{
    public Slider MaxTimeSlider;
    public Slider RewardTimeRateSlider;
    public Slider StartRemaindSlider;
    public Slider RewardRemaindSlider;
    public Slider RepeatRateSlider;
    
    public Text MaxTimeText;
    public Text RewardTimeRateText;
    public Text StartRemaindText;
    public Text RewardRemaindText;
    public Text RepeatRateText;
    
    public Button StartButton;
    public Button CancelButton;
    
    private DifficultySetting setting;

    protected override void Awake()
    {
        base.Awake();
        MaxTimeSlider.onValueChanged.AddListener((value) =>
        {
            setting.MaxTime = value;
            MaxTimeText.text = value.ToString("0");
        });
        RewardTimeRateSlider.onValueChanged.AddListener((value) =>
        {
            setting.RewardTimeRate = value;
            RewardTimeRateText.text = value.ToString("0.00");
        });
        StartRemaindSlider.onValueChanged.AddListener((value) =>
        {
            setting.StartRemaind = (int)value;
            StartRemaindText.text = value.ToString("0");
        });
        RewardRemaindSlider.onValueChanged.AddListener((value) =>
        {
            setting.RewardRemaind = (int)value;
            RewardRemaindText.text = value.ToString("0");
        });
        RepeatRateSlider.onValueChanged.AddListener((value) =>
        {
            setting.RepeatRate = value;
            RepeatRateText.text = value.ToString("0.00");
        });
    }

    public override void Init()
    {
        base.Init();
        StartButton.onClick.AddListener(() => GameManager.Instance.StartGame(setting));
        CancelButton.onClick.AddListener(() => GameManager.Instance.ToStartPanel());
    }

    public override Task Load()
    {
        setting = DifficultyHelp.GetSetting(Difficulty.Custom);
        MaxTimeText.text = setting.MaxTime.ToString("0");
        RewardTimeRateText.text = setting.RewardTimeRate.ToString("0.00");
        StartRemaindText.text = setting.StartRemaind.ToString("0");
        RewardRemaindText.text = setting.RewardRemaind.ToString("0");
        RepeatRateText.text = setting.RepeatRate.ToString("0.00");
        MaxTimeSlider.value = setting.MaxTime;
        RewardTimeRateSlider.value = setting.RewardTimeRate;
        StartRemaindSlider.value = setting.StartRemaind;
        RewardRemaindSlider.value = setting.RewardRemaind;
        RepeatRateSlider.value = setting.RepeatRate;
        return base.Load();
    }
}