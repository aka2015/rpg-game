extends Node3D

@export var target_path: NodePath = "../../"
@export var offset := Vector3(0, 6, 8)
@export var smooth_speed := 6.0

@onready var _target: Node3D = get_node_or_null(target_path)

func _process(delta: float) -> void:
	if _target == null:
		return
	var desired := _target.global_position + offset
	global_position = global_position.lerp(desired, smooth_speed * delta)
	look_at(_target.global_position, Vector3.UP)
