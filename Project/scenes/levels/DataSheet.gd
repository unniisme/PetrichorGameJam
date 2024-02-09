extends Node2D

@onready var continue_button: Button = find_child("ContinueButton")

func _ready():
	AudioManager.StopStream("footsteps")
	continue_button.pressed.connect(next)
	
func next():
	GameManager.LoadNextLevel()
