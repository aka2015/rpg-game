extends Node

var quest_id: String = "kill_001"
var quest_started := false
var quest_progress := 0
var quest_required := 3
var quest_completed := false
var quest_turned_in := false

var player_level := 1
var player_exp := 0
var player_gold := 0

func add_kill_progress() -> void:
	if not quest_started or quest_completed:
		return
	quest_progress += 1
	if quest_progress >= quest_required:
		quest_progress = quest_required
		quest_completed = true

func gain_exp(amount: int) -> void:
	player_exp += amount
	while player_exp >= player_level * 100:
		player_exp -= player_level * 100
		player_level += 1

func gain_gold(amount: int) -> void:
	player_gold = max(0, player_gold + amount)

func to_save_dict(player_position: Vector3) -> Dictionary:
	return {
		"version": 1,
		"player_level": player_level,
		"player_exp": player_exp,
		"player_gold": player_gold,
		"player_pos": [player_position.x, player_position.y, player_position.z],
		"quest_started": quest_started,
		"quest_progress": quest_progress,
		"quest_completed": quest_completed,
		"quest_turned_in": quest_turned_in,
	}

func load_from_dict(data: Dictionary) -> Vector3:
	player_level = int(data.get("player_level", 1))
	player_exp = int(data.get("player_exp", 0))
	player_gold = int(data.get("player_gold", 0))
	quest_started = bool(data.get("quest_started", false))
	quest_progress = int(data.get("quest_progress", 0))
	quest_completed = bool(data.get("quest_completed", false))
	quest_turned_in = bool(data.get("quest_turned_in", false))
	var arr = data.get("player_pos", [0, 0, 0])
	return Vector3(float(arr[0]), float(arr[1]), float(arr[2]))
