namespace Project.Core;

public readonly record struct EnemyDiedEvent(string EnemyId) : IGameEvent;
public readonly record struct PlayerDamagedEvent(int CurrentHp) : IGameEvent;
public readonly record struct QuestProgressChangedEvent(string QuestId, int Current, int Required) : IGameEvent;
public readonly record struct QuestCompletedEvent(string QuestId) : IGameEvent;
public readonly record struct PlayerLevelUpEvent(int Level) : IGameEvent;
public readonly record struct PlayerStatsChangedEvent(int CurrentHp, int MaxHp, int CurrentStamina, int MaxStamina, int Level) : IGameEvent;
public readonly record struct SaveOperationEvent(string Message, bool Success) : IGameEvent;
public readonly record struct CombatFeedbackEvent(string Message) : IGameEvent;
