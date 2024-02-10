class_name InstructionArea extends Trigger

@onready var label = $Label
@export var _fade_time: float = 1

func _on_body_entered(body):
	var tween: Tween = create_tween()
	tween.tween_property(label, "modulate:a", 1, _fade_time)
	
func _on_body_exited(body):
	var tween: Tween = create_tween()
	tween.tween_property(label, "modulate:a", 0, _fade_time)
