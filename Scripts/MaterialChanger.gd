extends Node

@export
var mesh: MeshInstance3D
@export
var materials: Array

var current_index: int = 0;

func change_material(index: int):
	mesh.mesh.surface_set_material(0, materials[index])
	current_index = index

func increment_index():
	change_material(current_index + 1)
	
func decrement_index():
	change_material(current_index - 1)

func select_default_material():
	change_material(0)
