extends ColorRect


var playersDropdown : OptionButton
var diffDropdown : OptionButton
var msgLabel : Label
var playButton : Button

var playersNum : int = -1
var diffLvl : int = -1

signal variables_set

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var globals = load("res://more scripts/Global.gd");
	globals.connect("variables_set", self, "_on_variables_set")
	
	playersDropdown = get_node("PopupPanel/VBoxContainer/CenterContainer/VBoxContainer/PanelContainer/HBoxContainer/OptionButton")
	diffDropdown = get_node("PopupPanel/VBoxContainer/CenterContainer/VBoxContainer/PanelContainer2/HBoxContainer2/OptionButton")
	msgLabel = get_node("PopupPanel/VBoxContainer/CenterContainer/VBoxContainer/CenterContainer/Label2")
	playButton = get_node("PopupPanel/VBoxContainer/CenterContainer/VBoxContainer/CenterContainer2/PanelContainer/Button")
	
	playersDropdown.connect("item_selected", self, "_on_players_selected")
	diffDropdown.connect("item_selected", self, "_on_diff_selected")
	playButton.connect("pressed", self, "_on_press_play")
	
	# Add items with labels and optional IDs
	playersDropdown.add_item("SELECT", -1)  # Add the initial item
	playersDropdown.select(-1)  # Select the initial item
	
	playersDropdown.add_item("2")
	playersDropdown.add_item("3")
	playersDropdown.add_item("4")
	
	
	diffDropdown.add_item("SELECT", -1)  # Add the initial item
	diffDropdown.select(-1)  # Select the initial item
	
	diffDropdown.add_item("easy")
	diffDropdown.add_item("hard")
	

func _on_players_selected(index : int) -> void:
		playersNum = index
		print("index is ", index)
	
func _on_diff_selected(index : int) -> void:
		diffLvl = index
		print("index is ", index)
	
func _on_press_play() -> void:
	#var gameHandlerScene = preload("res://GameHandler.tscn")
	
	
	msgLabel.text = ""
	
	if playersNum <= 0:
		
		msgLabel.text = "Please Select Players!"
	
	elif diffLvl <= 0:
		
		msgLabel.text = "Please Select Difficulty!"
	
	
	elif playersNum > 0 and diffLvl > 0:
		
		print("1 Variable set Function Called")
		Global.set_variables(playersNum +1, diffLvl -1)
		print("Variable set Function Returned")
		#yield(get_tree().create_timer(1.0), "timeout")

func _on_variables_set() -> void:
		Global.goto_scene("res://GameSceneMain.tscn")
		print('sceneswitch')
