extends TileMap

var downcolor = 0xb4afaea0
var upcolor = 0xffffffff

func _ready():
	light_down()

func light_up():
	modulate = upcolor

func light_down():
	modulate = downcolor
	
func change_light(val):
	if val: light_up()
	else: light_down()
