extends ColorRect

@onready var animator: AnimationPlayer = $AnimationPlayer
@onready var play_button: Button = find_child("ResumeButton")
@onready var quit_button: Button = find_child("QuitButton")
@onready var restart_button: Button = find_child("RestartButton")

func _ready():
	play_button.pressed.connect(unpause)
	quit_button.pressed.connect(quit)
	restart_button.pressed.connect(restart)

func unpause():
	animator.play("Unpause")
	AudioManager.PlayStream("select")
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
	AudioManager.PlayStream("select")
	GameManager.Restart()
	
func quit():
	animator.stop()
	AudioManager.PlayStream("select")
	GameManager.LoadMainMenu()


func _on_resume_button_focus_entered():
	AudioManager.PlayStream("chooseOption")


func _on_restart_button_focus_entered():
	AudioManager.PlayStream("chooseOption")


func _on_quit_button_focus_entered():
	AudioManager.PlayStream("chooseOption")
