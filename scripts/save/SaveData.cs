using Project.Quest;

namespace Project.Save;

public sealed class SaveData
{
    public const int CurrentVersion = 1;

    public int Version { get; set; } = CurrentVersion;
    public string TimestampUtc { get; set; } = string.Empty;

    public int PlayerLevel { get; set; }
    public int PlayerExperience { get; set; }
    public int PlayerCurrentHp { get; set; }
    public int PlayerCurrentStamina { get; set; }
    public float PlayerPosX { get; set; }
    public float PlayerPosY { get; set; }
    public float PlayerPosZ { get; set; }
    public string ActiveSceneName { get; set; } = "world";
    public QuestSnapshot[] Quests { get; set; } = System.Array.Empty<QuestSnapshot>();
}

public sealed class QuestSnapshot
{
    public string QuestId { get; set; } = string.Empty;
    public int CurrentCount { get; set; }
    public QuestStatus Status { get; set; }
}
