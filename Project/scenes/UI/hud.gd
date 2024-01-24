class_name HUD extends CanvasLayer

var heart_container : HBoxContainer
var crystal_container : HBoxContainer
var pause_menu

@export var max_hearts = 3
@export var max_crystals = 3

func _ready():
	heart_container = $HBoxContainer
	crystal_container = $crystalContainer
	pause_menu = $PauseMenu
	
func connect_signals():
	GameManager.GetPlayer().connect("HealthChanged", _on_player_health_changed)
	GameManager.GetLevel().connect("MorphChargesChanged", _on_morph)
	GameManager.GetLevel().connect("Menu", _open_menu)

func _on_player_health_changed(health):
	heart_container.updateHearts(health)
	
func _on_morph(charges):
	crystal_container.updateCrystals(int(charges/2), (charges%2))
	
func _open_menu():
	if (get_tree().paused):
		pause_menu.unpause()
	else:
		pause_menu.pause()
