using System;
using System.Text.Json;
using Godot;

namespace Project.Save;

public sealed class SaveManager
{
    public string Serialize(SaveData data)
    {
        return JsonSerializer.Serialize(data);
    }

    public SaveData Deserialize(string json)
    {
        return JsonSerializer.Deserialize<SaveData>(json) ?? new SaveData();
    }

    public void SaveToUserPath(string fileName, SaveData data)
    {
        data.Version = SaveData.CurrentVersion;
        data.TimestampUtc = DateTime.UtcNow.ToString("O");

        var path = $"user://{fileName}";
        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file.StoreString(Serialize(data));
    }

    public SaveData LoadFromUserPath(string fileName)
    {
        var path = $"user://{fileName}";
        if (!FileAccess.FileExists(path))
        {
            return new SaveData();
        }

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        var json = file.GetAsText();
        var data = Deserialize(json);

        if (data.Version > SaveData.CurrentVersion)
        {
            GD.PrintErr($"Unsupported save version: {data.Version}");
            return new SaveData();
        }

        return MigrateIfNeeded(data);
    }

    public void SaveToSlot(int slotIndex, SaveData data)
    {
        SaveToUserPath(GetSlotFileName(slotIndex), data);
    }

    public SaveData LoadFromSlot(int slotIndex)
    {
        return LoadFromUserPath(GetSlotFileName(slotIndex));
    }

    private static string GetSlotFileName(int slotIndex)
    {
        var safeIndex = slotIndex < 0 ? 0 : slotIndex;
        return $"save_slot_{safeIndex}.json";
    }

    private static SaveData MigrateIfNeeded(SaveData data)
    {
        if (data.Version < 2)
        {
            if (string.IsNullOrWhiteSpace(data.TimestampUtc))
            {
                data.TimestampUtc = DateTime.UtcNow.ToString("O");
            }

            data.Version = 2;
        }

        return data;
    }
}
