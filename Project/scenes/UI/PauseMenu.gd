extends ColorRect

@onready var animator: AnimationPlayer = $AnimationPlayer
@onready var play_button: Button = find_child("ResumeButton")
@onready var quit_button: Button = find_child("QuitButton")
@onready var restart_button: Button = find_child("RestartButton")

func _ready():
	pause()
	play_button.pressed.connect(unpause)
	quit_button.pressed.connect(get_tree().quit)
	restart_button.pressed.connect(get_tree().reload_current_scene)

func unpause():
	animator.play("Unpause")
	get_tree().paused = false
	
func pause():
	animator.play("Pause")
	get_tree().paused = true
# Called when the node enters the scene tree for the first time.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
