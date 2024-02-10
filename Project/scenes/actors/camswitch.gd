extends Area2D

@export var targetPos : Node2D

func _ready():
	connect("body_entered", _on_body_entered)
	
func _on_body_entered(body):
	GameManager.GetLevel().GetCamera().position = targetPos.position
