class_name PushArea extends Trigger

@export var direction : Vector2 = Vector2.RIGHT

func _on_body_entered(body : Node):
	if (body is Player):
		body.inputEnabled = false
		body.Move(direction)

func _on_body_exited(body : Node):
	if (body is Player):
		body.inputEnabled = true
