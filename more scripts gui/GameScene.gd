extends Control

var options
var p
var c


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	options = get_node("/root/Options")
	print(options.numPlayers)
	
	p = ResourceLoader.load("res://Player.tscn")
	c = ResourceLoader.load("res://CPUplayer.tscn")
	
	initGame()

func initGame() -> void:
	get_node("Players").add_child(p.instance())
	
	var i = 1;
	while(i < options.numPlayers):
		get_node("Players").add_child(c.instance())
		i += 1
	
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta: float) -> void:
#	pass
