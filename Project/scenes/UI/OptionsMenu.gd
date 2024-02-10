extends ColorRect

@onready var animator: AnimationPlayer = $AnimationPlayer
@onready var back_button: Button = find_child("BackButton")

func _ready():
	animator.play("Option")
	back_button.pressed.connect(back)
	
func back():
	animator.play("Back")
	GameManager.LoadMainMenu()
