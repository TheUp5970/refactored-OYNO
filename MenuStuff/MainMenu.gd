extends Node


func _on_NewGameButton_pressed() -> void:
	Global.goto_scene("res://GameSceneMain.tscn")

func _on_QuitButton_pressed() -> void:
	get_tree().quit()
