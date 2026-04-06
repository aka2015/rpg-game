using System.Collections.Generic;
using Project.Core;

namespace Project.Quest;

public sealed class QuestManager
{
    private readonly IEventBus _eventBus;
    private readonly Dictionary<string, QuestRuntime> _quests = new();

    public QuestManager(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void AddQuest(QuestRuntime quest)
    {
        _quests[quest.QuestId] = quest;
    }

    public void StartQuest(string questId)
    {
        if (!_quests.TryGetValue(questId, out var quest))
        {
            return;
        }

        quest.Start();
        _eventBus.Publish(new QuestProgressChangedEvent(quest.QuestId, quest.CurrentCount, quest.RequiredCount));
    }

    public void OnEnemyDied(string enemyId)
    {
        foreach (var quest in _quests.Values)
        {
            var previousStatus = quest.Status;
            quest.RegisterEnemyKill(enemyId);

            _eventBus.Publish(new QuestProgressChangedEvent(quest.QuestId, quest.CurrentCount, quest.RequiredCount));
            if (previousStatus != QuestStatus.Completed && quest.Status == QuestStatus.Completed)
            {
                _eventBus.Publish(new QuestCompletedEvent(quest.QuestId));
            }
        }
    }
}
