class_name HUD extends CanvasLayer

var heart_container : HBoxContainer
var crystal_container : HBoxContainer

func _ready():
	heart_container = $HBoxContainer
	crystal_container = $crystalContainer


func _on_player_health_changed(health):
	heart_container.updateHearts(health)
