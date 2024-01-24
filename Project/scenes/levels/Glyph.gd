extends TileMap

var downcolor = 0xb4afaea0
var upcolor = 0xffffffff

func _ready():
	light_down()

func light_up():
	modulate = upcolor

func light_down():
	modulate = downcolor
	
