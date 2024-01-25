class_name HeartContainer extends HBoxContainer
var player_health : int = 3
var curr_health : int = 3

@onready var HeartGuiClass = preload("res://scenes/UI/heartGui.tscn")

func setMaxHearts(max: int):
	for i in range(max):
		var heart = HeartGuiClass.instantiate()
		add_child(heart)

func updateHearts(currHealth: int):
	var hearts = get_children()
	
	for i in range(currHealth):
		hearts[i].update(true)
		
	for i in range(currHealth, hearts.size()):
		hearts[i].update(false)
