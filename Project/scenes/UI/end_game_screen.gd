extends Control

var is_morphed = false
var morphlabel

func _ready():
	$Button.button_down.connect(GameManager.LoadMainMenu)
	morphlabel = $MorphLabel
	AudioManager.StopStream("footsteps")
	unmorph()
	
func unmorph():
	morphlabel.visible = false
	AudioManager.StopStream("happyBackground")
	AudioManager.StopStream("scaryBackground")
	AudioManager.PlayStream("happyBackground")
	is_morphed = false
	
func morph():
	morphlabel.visible = true
	AudioManager.StopStream("scaryBackground")
	AudioManager.StopStream("happyBackground")
	AudioManager.PlayStream("scaryBackground")
	is_morphed = true
	
func toggle_morph():
	if is_morphed: unmorph()
	else: morph()
	
func _process(delta):
	
	if Input.is_action_just_pressed("action"):
		toggle_morph()
