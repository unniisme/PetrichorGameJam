extends MarginContainer

@onready var selector_one = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer/HBoxContainer/Selector
@onready var selector_two = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer2/HBoxContainer/Selector
@onready var selector_three = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer3/HBoxContainer/Selector

var current_selection = 0

func _ready():
	set_current_selection(0)
	AudioManager.PlayStream("happyBackground")
	
func _process(delta):
	if Input.is_action_just_pressed("down") and current_selection < 2:
		AudioManager.PlayStream("chooseOption")		
		current_selection += 1
		set_current_selection(current_selection)
	elif Input.is_action_just_pressed("up") and current_selection > 0:
		AudioManager.PlayStream("chooseOption")		
		current_selection -= 1
		set_current_selection(current_selection)
	elif Input.is_action_just_pressed("ui_accept"):
		AudioManager.PlayStream("select")
		handle_selection(current_selection)
		
func handle_selection(_current_selection):
	if _current_selection == 0:
		GameManager.LoadNextLevel()
	elif _current_selection == 1:
		GameManager.LoadOptionsMenu()
	elif _current_selection == 2:
		get_tree().quit()
		
func set_current_selection(_current_selection):
	selector_one.text = ""
	selector_two.text = ""
	selector_three.text = ""
	if _current_selection == 0:
		selector_one.text = ">"
	elif _current_selection == 1:
		selector_two.text = ">"
	elif _current_selection == 2:
		selector_three.text = ">"
