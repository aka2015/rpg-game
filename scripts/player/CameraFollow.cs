using Godot;

namespace Project.Player;

public partial class CameraFollow : Node3D
{
    [Export] public NodePath TargetPath = "../../";
    [Export] public Vector3 Offset = new(0, 6, 8);
    [Export] public float SmoothSpeed = 6.0f;

    private Node3D _target = default!;

    public override void _Ready()
    {
        _target = GetNodeOrNull<Node3D>(TargetPath);
    }

    public override void _Process(double delta)
    {
        if (_target == null)
        {
            return;
        }

        var desiredPosition = _target.GlobalPosition + Offset;
        GlobalPosition = GlobalPosition.Lerp(desiredPosition, SmoothSpeed * (float)delta);
        LookAt(_target.GlobalPosition, Vector3.Up);
    }
}
