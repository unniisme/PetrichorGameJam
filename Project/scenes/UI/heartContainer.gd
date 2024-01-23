extends HBoxContainer
var player_health : int = 3
var curr_health : int = 1
var crystal_number : int = 3
@onready var HeartGuiClass = preload("res://scenes/UI/heartGui.tscn")
# Called when the node enters the scene tree for the first time.
func _ready():
	setMaxHearts(player_health)
	updateHearts(curr_health)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

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
