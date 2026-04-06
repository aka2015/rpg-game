using Godot;

namespace Project.Enemy;

public partial class EnemyDummy : Node3D
{
    private readonly EnemyStats _stats = new("enemy_dummy", 50, 8, 2, 15);

    public bool ApplyDamage(int incoming)
    {
        return _stats.ApplyDamage(incoming);
    }
}
