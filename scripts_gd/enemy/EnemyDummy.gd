extends Node3D

@export var max_hp := 50
@export var hit_feedback_scale := 1.08

var current_hp := 50

func _ready() -> void:
	current_hp = max_hp

func apply_damage(damage: int) -> bool:
	current_hp = max(0, current_hp - damage)
	_play_hit_feedback()
	if current_hp <= 0:
		queue_free()
		return true
	return false

func _play_hit_feedback() -> void:
	var original := scale
	var tween := create_tween()
	tween.tween_property(self, "scale", original * hit_feedback_scale, 0.06)
	tween.tween_property(self, "scale", original, 0.08)
