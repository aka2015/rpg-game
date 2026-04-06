namespace Project.Quest;

public sealed class QuestRuntime
{
    public string QuestId { get; }
    public string TargetEnemyId { get; }
    public int RequiredCount { get; }
    public int CurrentCount { get; private set; }
    public QuestStatus Status { get; private set; }

    public QuestRuntime(string questId, string targetEnemyId, int requiredCount)
    {
        QuestId = questId;
        TargetEnemyId = targetEnemyId;
        RequiredCount = requiredCount;
        Status = QuestStatus.NotStarted;
    }

    public void Start()
    {
        if (Status == QuestStatus.NotStarted)
        {
            Status = QuestStatus.InProgress;
        }
    }

    public void RegisterEnemyKill(string enemyId)
    {
        if (Status != QuestStatus.InProgress || enemyId != TargetEnemyId)
        {
            return;
        }

        CurrentCount += 1;
        if (CurrentCount >= RequiredCount)
        {
            CurrentCount = RequiredCount;
            Status = QuestStatus.Completed;
        }
    }

    public void TurnIn()
    {
        if (Status == QuestStatus.Completed)
        {
            Status = QuestStatus.TurnedIn;
        }
    }
}
