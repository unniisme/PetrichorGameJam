class_name CamSwitchArea extends Trigger

## The target node to move the camera to
@export var targetPos : Node2D

## Whether to override the camera to follow the player
@export var target_player : bool = false
	
func _on_body_entered(body):
	var camera = GameManager.GetLevel().GetCamera() 
	camera.position = targetPos.position
	camera.follow_player = target_player
	
