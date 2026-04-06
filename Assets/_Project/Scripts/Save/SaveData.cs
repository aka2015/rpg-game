using Project.Quest;

namespace Project.Save;

public sealed class SaveData
{
    public int PlayerLevel { get; set; }
    public int PlayerExperience { get; set; }
    public int PlayerCurrentHp { get; set; }
    public int PlayerCurrentStamina { get; set; }
    public string ActiveSceneName { get; set; } = "Village";
    public QuestSnapshot[] Quests { get; set; } = System.Array.Empty<QuestSnapshot>();
}

public sealed class QuestSnapshot
{
    public string QuestId { get; set; } = string.Empty;
    public int CurrentCount { get; set; }
    public QuestStatus Status { get; set; }
}
