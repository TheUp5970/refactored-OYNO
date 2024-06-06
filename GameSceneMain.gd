extends WorldEnvironment



# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	
	#var global = Global.Get("Global")
	#var global = get_node("/root/Global")
	var gHandler : Node = load("res://GameHandler.tscn").instance()
	
	gHandler.playerCount = Global.numPlayers
	gHandler.diffLevel = Global.isDifficult
	gHandler.has_received_params = Global.gave_params
	
	#print(" gameHandlers Players:  ", gHandler.playerCount)
	#print(" gameHandlers Diff:  ", gHandler.diffLevel)
	if(gHandler.has_received_params):
		print(" Params WERE sent!")
	else:
		print(" Params were NOT sent!")
	
	print(" ->Adding ", gHandler.name, " as a Node in ", self.name)
	self.add_child(gHandler)
	print(" ->Done adding ", gHandler.name , " as a Node in ", self.name)
