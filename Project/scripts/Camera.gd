extends Camera2D

var follow_player : bool

func _process(delta):
	
	if follow_player:
		position = GameManager.GetPlayer().position
