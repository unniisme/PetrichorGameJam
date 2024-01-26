class_name CamSwitchArea extends Trigger

@export var targetPos : Node2D
	
func _on_body_entered(body):
	GameManager.GetLevel().GetCamera().position = targetPos.position
