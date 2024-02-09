extends ColorRect

@onready var animator: AnimationPlayer = $AnimationPlayer
@onready var play_button: Button = find_child("ResumeButton")
@onready var quit_button: Button = find_child("QuitButton")
@onready var options_button: Button = find_child("OptionsButton")
@onready var restart_button: Button = find_child("RestartButton")
@onready var back_button: Button = find_child("BackButton")
@onready var pause_menu: CenterContainer = $VBoxContainer/PauseContainer
@onready var options_menu: CenterContainer = $VBoxContainer/OptionsContainer
@onready var data_sheet: CenterContainer = $VBoxContainer/DataContainer
@onready var continue_button: Button = find_child("ContinueButton")

func _ready():
	hide_settings()
	play_button.pressed.connect(unpause)
	quit_button.pressed.connect(quit)
	options_button.pressed.connect(options)
	back_button.pressed.connect(back)
	restart_button.pressed.connect(restart)
	continue_button.pressed.connect(next)

func back():
	AudioManager.PlayStream("select")
	AudioManager.StopStream("happyBackgroundUI")
	animator.play("OptionsClosed")
	options_menu.hide()
	animator.play("PauseComing")
	pause_menu.show()

func next():
	AudioManager.PlayStream("select")
	animator.play("DataSheetClosed")
	data_sheet.hide()

func open_data():
	animator.play("DataSheetOpened")
	data_sheet.show()
	
	
func options():
	AudioManager.PlayStream("select")
	animator.play("PauseGoing")
	pause_menu.hide()
	animator.play("OptionsOpened")
	options_menu.show()
	AudioManager.PlayStream("happyBackgroundUI")
	
func unpause():
	AudioManager.PlayStream("select")
	animator.play("Unpause")
	hide_settings()
	AudioManager.StopStream("happyBackgroundUI")
	get_tree().paused = false
	play_button.mouse_filter = Control.MOUSE_FILTER_IGNORE
	quit_button.mouse_filter = Control.MOUSE_FILTER_IGNORE
	restart_button.mouse_filter = Control.MOUSE_FILTER_IGNORE
	
func pause():
	animator.play("Pause")
	pause_menu.show()
	get_tree().paused = true
	play_button.mouse_filter = Control.MOUSE_FILTER_STOP
	quit_button.mouse_filter = Control.MOUSE_FILTER_STOP
	restart_button.mouse_filter = Control.MOUSE_FILTER_STOP
# Called when the node enters the scene tree for the first time.

func restart():
	AudioManager.PlayStream("select")
	animator.play("Unpause")
	hide_settings()
	animator.stop()
	GameManager.Restart()
	
func quit():
	AudioManager.PlayStream("select")
	animator.play("Unpause")
	hide_settings()
	animator.stop()
	GameManager.LoadMainMenu()

func hide_settings():
	pause_menu.hide()
	options_menu.hide()
	data_sheet.hide()

func _on_resume_button_focus_entered():
	AudioManager.PlayStream("chooseOption")


func _on_restart_button_focus_entered():
	AudioManager.PlayStream("chooseOption")


func _on_quit_button_focus_entered():
	AudioManager.PlayStream("chooseOption")


func _on_options_button_focus_entered():
	AudioManager.PlayStream("chooseOption")
