extends CharacterBody3D

@export var move_speed := 5.0
@export var gravity := 9.8

func _physics_process(delta: float) -> void:
	var input := Input.get_vector("move_left", "move_right", "move_forward", "move_back")
	var direction := Vector3(input.x, 0.0, input.y).normalized()
	velocity = Vector3(direction.x * move_speed, velocity.y - gravity * delta, direction.z * move_speed)
	move_and_slide()
