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

    public QuestRuntime? GetQuest(string questId)
    {
        return _quests.TryGetValue(questId, out var quest) ? quest : null;
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

    public void RegisterEnemyDeath(string enemyId)
    {
        foreach (var quest in _quests.Values)
        {
            var wasCompleted = quest.Status == QuestStatus.Completed;
            quest.RegisterEnemyKill(enemyId);

            _eventBus.Publish(new QuestProgressChangedEvent(quest.QuestId, quest.CurrentCount, quest.RequiredCount));
            if (!wasCompleted && quest.Status == QuestStatus.Completed)
            {
                _eventBus.Publish(new QuestCompletedEvent(quest.QuestId));
            }
        }
    }

    public void RestoreQuest(string questId, int currentCount, QuestStatus status)
    {
        if (!_quests.TryGetValue(questId, out var quest))
        {
            return;
        }

        quest.RestoreProgress(currentCount, status);
        _eventBus.Publish(new QuestProgressChangedEvent(quest.QuestId, quest.CurrentCount, quest.RequiredCount));
        if (quest.Status == QuestStatus.Completed)
        {
            _eventBus.Publish(new QuestCompletedEvent(quest.QuestId));
        }
    }
}
