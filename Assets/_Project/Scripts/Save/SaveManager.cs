using System.Text.Json;

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
}
