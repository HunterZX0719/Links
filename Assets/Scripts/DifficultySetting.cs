public struct DifficultySetting
{
    public float MaxTime;
    public float RewardTimeRate;
    
    public int StartRemaind;
    public int RewardRemaind;
    
    public float RepeatRate;
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Hell,
    Custom
}

public static class DifficultyHelp
{
    public static DifficultySetting GetSetting(Difficulty type)
    {
        DifficultySetting setting = new DifficultySetting();
        switch (type)
        {
            case Difficulty.Easy:
                setting.MaxTime = 3000f;
                setting.RewardTimeRate = 0.1f;
                setting.StartRemaind = 100;
                setting.RewardRemaind = 10;
                setting.RepeatRate = 0.4f;
                break;
            case Difficulty.Normal:
                setting.MaxTime = 2400f;
                setting.RewardTimeRate = 0.1f;
                setting.StartRemaind = 10;
                setting.RewardRemaind = 5;
                setting.RepeatRate = 0.3f;
                break;
            case Difficulty.Hard:
                setting.MaxTime = 1500f;
                setting.RewardTimeRate = 0.1f;
                setting.StartRemaind = 10;
                setting.RewardRemaind = 2;
                setting.RepeatRate = 0.2f;
                break;
            case Difficulty.Hell:
                setting.MaxTime = 900f;
                setting.RewardTimeRate = 0.1f;
                setting.StartRemaind = 0;
                setting.RewardRemaind = 0;
                setting.RepeatRate = 0.1f;
                break;
            case Difficulty.Custom:
                setting.MaxTime = 2000f;
                setting.RewardTimeRate = 0.2f;
                setting.StartRemaind = 10;
                setting.RewardRemaind = 5;
                setting.RepeatRate = 0.3f;
                break;
        }
        return setting;
    }
}