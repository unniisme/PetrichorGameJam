class_name Trigger extends Area2D

## Whether this trigger will only trigger for player
@export var only_player = false

## Whether the area gets destroyed on use
@export var kill_on_use = false

func _ready():
	connect("body_entered", _on_body_entered_handler)
	connect("body_exited", _on_body_exited_handler)
	

func _on_body_entered_handler(body : Node2D):
	if only_player:
		if not body is Player:
			return
	_on_body_entered(body)
	
	if kill_on_use:
		queue_free()
	
func _on_body_exited_handler(body : Node2D):
	if only_player:
		if not body is Player:
			return
	_on_body_exited(body)

func _on_body_entered(body : Node2D):
	# override
	pass
	
func _on_body_exited(body : Node2D):
	# override
	pass
