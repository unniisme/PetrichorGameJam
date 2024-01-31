extends ColorRect

@onready var animator: AnimationPlayer = $AnimationPlayer
@onready var play_button: Button = find_child("ResumeButton")
@onready var quit_button: Button = find_child("QuitButton")
@onready var restart_button: Button = find_child("RestartButton")

func _ready():
	pause()
	play_button.pressed.connect(unpause)
	quit_button.pressed.connect(quit)
	restart_button.pressed.connect(restart)

func unpause():
	animator.play("Unpause")
	get_tree().paused = false
	play_button.mouse_filter = Control.MOUSE_FILTER_IGNORE
	quit_button.mouse_filter = Control.MOUSE_FILTER_IGNORE
	restart_button.mouse_filter = Control.MOUSE_FILTER_IGNORE
	
func pause():
	animator.play("Pause")
	get_tree().paused = true
	play_button.mouse_filter = Control.MOUSE_FILTER_STOP
	quit_button.mouse_filter = Control.MOUSE_FILTER_STOP
	restart_button.mouse_filter = Control.MOUSE_FILTER_STOP
# Called when the node enters the scene tree for the first time.

func restart():
	animator.stop()
	GameManager.Restart()
	
func quit():
	animator.stop()
	GameManager.LoadMainMenu()
