class_name LevelEndArea extends Trigger


func _on_body_entered(body):
	call_deferred("end_level")
	
func end_level():
	GameManager.LoadNextLevel()
