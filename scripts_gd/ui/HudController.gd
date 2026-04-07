extends CanvasLayer

@onready var quest_label: Label = $QuestLabel
@onready var status_label: Label = $StatusLabel
@onready var stats_label: Label = $StatsLabel

func _process(_delta: float) -> void:
	var gm = get_node_or_null("/root/GameManager")
	if gm == null:
		return

	quest_label.text = "Quest %s: %d/%d" % [gm.quest_id, gm.quest_progress, gm.quest_required]
	if gm.quest_turned_in:
		status_label.text = "Quest turned in"
	elif gm.quest_completed:
		status_label.text = "Quest completed - talk to NPC"
	elif gm.quest_started:
		status_label.text = "Quest in progress"
	else:
		status_label.text = "Quest not started"

	stats_label.text = "LV %d | EXP %d | Gold %d" % [gm.player_level, gm.player_exp, gm.player_gold]
