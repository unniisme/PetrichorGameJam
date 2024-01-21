extends StaticBody2D

var active_sprite
var inactive_sprite
var collision_shape : CollisionShape2D

# Called when the node enters the scene tree for the first time.
func _ready():
	active_sprite = $active
	inactive_sprite = $inactive
	collision_shape = $CollisionShape2D

	set_open(false)

func set_open(val : bool):
	active_sprite.visible = val
	inactive_sprite.visible = not val
	collision_shape.disabled = val
	
	if val: GameManager.RemoveObjectFromGrid(self)
	else: GameManager.PlaceObjectOnGrid(self)
		

func open():
	set_open(true)

func close():
	set_open(false)
	
