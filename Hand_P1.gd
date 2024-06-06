extends Spatial

const CARD_SCENE = preload("res://Card.tscn")
const HAND_WIDTH = 1.5

var n = 5
var hand = self
export var spread_curve : Curve
export var height_curve : Curve
export var rotation_curve : Curve

func _ready() -> void:
	add_n_cards()
	

#Put in Card own script
#func _process(delta: float) -> void:
#	transform = transform.sphere_interpolate_with(target_transform, ANIM_SPEED * delta)
#	rotation.z = lerp(rotation.z, target_rotation, ANIM_SPEED * delta)
#
func add_n_cards()->void:
	var card
	
	for _x in n:
		card = CARD_SCENE.instance()
		add_child(card)
	
	fan_out_cards()

func fan_out_cards()->void:
	
	for card in hand.get_children():
		var hand_ratio = 0.5
		var camera:=get_viewport().get_camera()
		
		if get_child_count()>1:
			hand_ratio = float(card.get_index())/float(hand.get_child_count()) #float(hand.get_child_count()-1)
		
		var destination = hand.global_transform
		
		destination.basis = camera.global_transform.basis	#camera billboard
		destination.origin += camera.global_transform.basis * Vector3.BACK*hand_ratio*0.1 #good overlap
		
		destination.origin.x+=spread_curve.interpolate(hand_ratio) * HAND_WIDTH	#spread sideways
		destination.origin+=height_curve.interpolate(hand_ratio) * Vector3.UP	#spread up
		
		#print(spread_curve.interpolate(hand_ratio) )
		
		#print(destination.origin)
		card.global_transform.origin = destination.origin
		card.rotation.z = rotation_curve.interpolate(hand_ratio)*0.5	#0.3

