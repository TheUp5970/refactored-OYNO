extends Node

var newGameDialog : PopupPanel

func _ready() -> void:
	newGameDialog = get_node("MainMenuContainer/MainPanel/HBoxContainer/MarginContainer/VBoxtContainer/PanelContainer/NewGameDialog/PopupPanel")

func _on_NewGameButton_pressed() -> void:
	newGameDialog.visible = not newGameDialog.visible
	
	#Starts new Game immediately
	#var gameHandlerScene = preload("res://GameHandler.tscn")
	#Global.goto_scene("res://GameSceneMain.tscn", gameHandlerScene.instance())

func _on_QuitButton_pressed() -> void:
	get_tree().quit()
