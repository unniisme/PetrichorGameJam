class_name CrystalArea extends Trigger

var animationSprite : AnimatedSprite2D

@export var animation_frequence = 1
@export var animation_amplitude = 1
var animation_time = 0

func _ready():
	super()
	animationSprite = $AnimatedSprite2D
	
func _process(delta):
	animationSprite.position.y = sin(animation_time*animation_frequence*TAU)*animation_amplitude
	animation_time += delta
	animation_time = animation_time - int(animation_time)

func _on_body_entered(body):
	if(GameManager.GetLevel().IncrementMorphCharges()):
		queue_free()
