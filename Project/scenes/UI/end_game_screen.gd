extends Control

func _ready():
	$Button.button_down.connect(GameManager.LoadMainMenu)
	AudioManager.StopStream("happyBackground")
	AudioManager.StopStream("scaryBackground")
	AudioManager.StopStream("footsteps")
	AudioManager.PlayStream("happyBackground")
	
