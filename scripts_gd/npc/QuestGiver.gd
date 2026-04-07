extends Node3D

@export var world_controller_path: NodePath = "../World"

func _unhandled_input(event: InputEvent) -> void:
	if not event.is_action_pressed("interact"):
		return
	var world = get_node_or_null(world_controller_path)
	if world:
		world.start_quest_from_npc()
