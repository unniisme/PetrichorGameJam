extends Control

func _ready():
	$Button.button_down.connect(GameManager.LoadMainMenu)
	
