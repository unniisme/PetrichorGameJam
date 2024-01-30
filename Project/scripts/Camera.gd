extends Camera2D

var follow_player : bool
var base_zoom
var zoom_mult : float = 1
var zoom_speed = 0.05

var zoom_progress = 1

func _ready():
	base_zoom = zoom

func _process(delta):
	if (zoom_progress < 1):
		zoom = lerp(zoom, base_zoom*zoom_mult, zoom_progress)
		zoom_progress += zoom_speed
		
	if follow_player:
		position = GameManager.GetPlayer().position

func set_zoom_mult(mult):
	zoom_mult = mult
	zoom_progress = 0
