class_name PushArea extends Trigger

@export var direction : Vector2 = Vector2.RIGHT

func _on_body_entered(body : Node):
	if (body is Player):
		body.inputEnabled = false
		body.Move(direction)
		await get_tree().create_timer(1.0).timeout
		body.inputEnabled = true

func _on_body_exited(body : Node):
	if (body is Player):
		body.inputEnabled = true
