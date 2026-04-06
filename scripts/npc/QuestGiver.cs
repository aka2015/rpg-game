using Godot;
using Project.World;

namespace Project.Npc;

public partial class QuestGiver : Node3D
{
    [Export] public NodePath WorldControllerPath = "../World";

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!@event.IsActionPressed("interact"))
        {
            return;
        }

        var world = GetNodeOrNull<WorldController>(WorldControllerPath);
        world?.StartQuestFromNpc();
        GD.Print("Quest started from QuestGiver.");
    }
}
