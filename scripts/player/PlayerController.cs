using Godot;

namespace Project.Player;

public partial class PlayerController : CharacterBody3D
{
    [Export] public float MoveSpeed = 5.0f;

    public override void _PhysicsProcess(double delta)
    {
        var input = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        var direction = new Vector3(input.X, 0, input.Y);

        Velocity = direction * MoveSpeed;
        MoveAndSlide();
    }
}
