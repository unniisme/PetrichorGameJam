class_name HUD extends CanvasLayer

var heart_container : HBoxContainer
var crystal_container : HBoxContainer

func _ready():
	heart_container = $HBoxContainer
	crystal_container = $crystalContainer
	
func connect_signals():
	GameManager.GetPlayer().connect("HealthChanged", _on_player_health_changed)
	GameManager.GetLevel().connect("MorphChargesChanged", _on_morph)


func _on_player_health_changed(health):
	heart_container.updateHearts(health)
	
func _on_morph(charges):
	crystal_container.updateCrystals(int(charges/2), (charges%2))
