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
        return Deserialize(json);
    }
}
