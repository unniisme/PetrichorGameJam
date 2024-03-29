class_name CrystalContainer extends HBoxContainer

var max_crystal : int = 3
var curr_crystal : int = 3
var used_crystal : bool = false

@onready var CrystalGuiClass = preload("res://scenes/UI/crystalGui.tscn")

func setMaxCrystals(max: int):
	for i in range(max):
		var crystal = CrystalGuiClass.instantiate()
		add_child(crystal)
	
func updateCrystals(currCryst: int, usedCryst: bool):
	var crystals = get_children()
	for i in range(currCryst):
		crystals[i].update(0)
	if(currCryst<crystals.size()):
		if(usedCryst):
			crystals[currCryst].update(1)
		else:
			crystals[currCryst].update(2)
		for i in range(currCryst+1, crystals.size()):
			crystals[i].update(2)

