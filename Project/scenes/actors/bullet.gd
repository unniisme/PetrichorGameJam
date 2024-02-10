class_name bullet extends Area2D

var dir : Vector2
var speed = 100

func _physics_process(delta):
	position = position + dir*speed*delta


func _on_body_entered(body:Node2D):
	if not (body.is_in_group("shooter")):
		queue_free()
