extends TileMap

var downcolor = 0xb4afaea0
var upcolor = 0xffffffff

@export var pressure_pad : Node2D

func _ready():
	light_down()
	
	if (pressure_pad != null):
		pressure_pad.connect("Activated", light_up)
		pressure_pad.connect("Deactivated", light_down)

func light_up():
	modulate = upcolor

func light_down():
	modulate = downcolor
	
func change_light(val):
	if val: light_up()
	else: light_down()
