extends ColorRect

@onready var animator: AnimationPlayer = $AnimationPlayer
@onready var play_button: Button = find_child("ResumeButton")
@onready var quit_button: Button = find_child("QuitButton")
@onready var restart_button: Button = find_child("RestartButton")

func _ready():
	play_button.pressed.connect(unpause)
	quit_button.pressed.connect(get_tree().quit)
	restart_button.pressed.connect(restart)

func unpause():
	animator.play("Unpause")
	get_tree().paused = false
	
func pause():
	animator.play("Pause")
	get_tree().paused = true
# Called when the node enters the scene tree for the first time.

func restart():
	animator.stop()
	GameManager.Restart()
