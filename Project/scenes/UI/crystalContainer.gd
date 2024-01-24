extends HBoxContainer

var max_crystal : int = 5
var curr_crystal : int = 2
var used_crystal : bool = true

@onready var CrystalGuiClass = preload("res://scenes/UI/crystalGui.tscn")
# Called when the node enters the scene tree for the first time.
func _ready():
	setMaxCrystals(max_crystal)
	updateCrystals(curr_crystal, used_crystal)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func setMaxCrystals(max: int):
	for i in range(max):
		var crystal = CrystalGuiClass.instantiate()
		add_child(crystal)
	
func updateCrystals(currCryst: int, usedCryst: bool):
	var crystals = get_children()
	for i in range(currCryst-1):
		crystals[i].update(0)
	if(usedCryst):
		crystals[currCryst-1].update(1)
	else:
		crystals[currCryst-1].update(0)
	for i in range(currCryst, crystals.size()):
		crystals[i].update(2)

