using Godot;

namespace Project.Player;

public partial class PlayerController : CharacterBody3D
{
    [Export] public float MoveSpeed = 5.0f;
    [Export] public float Gravity = 9.8f;
    [Export] public NodePath AnimationPlayerPath = "AnimationPlayer";

    public PlayerStats Stats { get; } = new();

    private AnimationPlayer _animationPlayer = default!;

    public override void _Ready()
    {
        _animationPlayer = GetNodeOrNull<AnimationPlayer>(AnimationPlayerPath);
    }

    public override void _PhysicsProcess(double delta)
    {
        var input = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        var direction = new Vector3(input.X, 0, input.Y).Normalized();

        Velocity = new Vector3(direction.X * MoveSpeed, Velocity.Y - Gravity * (float)delta, direction.Z * MoveSpeed);
        MoveAndSlide();
    }

    public void RestoreStats(int level, int exp, int hp, int stamina)
    {
        Stats.Restore(level, exp, hp, stamina);
    }

    public void PlayAttackAnimation(int comboIndex)
    {
        if (_animationPlayer == null)
        {
            return;
        }

        var animationName = comboIndex switch
        {
            1 => "attack_1",
            2 => "attack_2",
            _ => "attack_3"
        };

        if (_animationPlayer.HasAnimation(animationName))
        {
            _animationPlayer.Play(animationName);
        }
    }
}
