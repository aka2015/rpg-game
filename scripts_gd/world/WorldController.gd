extends Node3D

@export var player_path: NodePath = "Player"
@export var enemy_path: NodePath = "EnemyDummy"
@export var attack_range := 2.25
@export var combo_window_ms := 900
@export var attack_cooldown_ms := 250

var _combo_damages := [18, 26, 34]
var _combo_index := 0
var _last_attack_time_ms := 0

func _ready() -> void:
	pass

func _unhandled_input(event: InputEvent) -> void:
	if event.is_action_pressed("attack"):
		_try_attack_enemy()
	if event.is_action_pressed("quick_save"):
		_quick_save()
	if event.is_action_pressed("quick_load"):
		_quick_load()

func start_quest_from_npc() -> void:
	var gm = get_node("/root/GameManager")
	if not gm.quest_started:
		gm.quest_started = true
		return
	if gm.quest_completed and not gm.quest_turned_in:
		gm.quest_turned_in = true
		gm.gain_exp(100)
		gm.gain_gold(50)

func _try_attack_enemy() -> void:
	var now = Time.get_ticks_msec()
	if now - _last_attack_time_ms < attack_cooldown_ms:
		return
	if now - _last_attack_time_ms > combo_window_ms:
		_combo_index = 0
	_last_attack_time_ms = now

	var player = get_node_or_null(player_path) as CharacterBody3D
	if player == null:
		return

	var from = player.global_position + Vector3.UP
	var forward = -player.global_basis.z
	var to = from + forward * attack_range
	var query = PhysicsRayQueryParameters3D.create(from, to)
	query.collide_with_areas = true
	var result = get_world_3d().direct_space_state.intersect_ray(query)
	if result.is_empty():
		return

	var collider = result.get("collider")
	var enemy = _find_enemy_ancestor(collider)
	if enemy == null:
		return

	var gm = get_node("/root/GameManager")
	var dmg = _combo_damages[_combo_index % _combo_damages.size()]
	_combo_index += 1
	if enemy.apply_damage(dmg):
		gm.add_kill_progress()
		gm.gain_exp(15)

func _find_enemy_ancestor(node: Node) -> Node:
	var current = node
	while current:
		if current.has_method("apply_damage"):
			return current
		current = current.get_parent()
	return null

func _quick_save() -> void:
	var gm = get_node("/root/GameManager")
	var player = get_node_or_null(player_path) as Node3D
	if player == null:
		return
	var data = gm.to_save_dict(player.global_position)
	var file = FileAccess.open("user://save_slot_1.json", FileAccess.WRITE)
	file.store_string(JSON.stringify(data))

func _quick_load() -> void:
	if not FileAccess.file_exists("user://save_slot_1.json"):
		return
	var file = FileAccess.open("user://save_slot_1.json", FileAccess.READ)
	var raw = file.get_as_text()
	var parsed = JSON.parse_string(raw)
	if typeof(parsed) != TYPE_DICTIONARY:
		return
	var gm = get_node("/root/GameManager")
	var pos = gm.load_from_dict(parsed)
	var player = get_node_or_null(player_path) as Node3D
	if player:
		player.global_position = pos
