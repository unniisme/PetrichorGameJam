shader_type canvas_item;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;
uniform sampler2D mask;
uniform bool invert_mask = false;

void vertex() {
    // No changes to vertex shader for a maskable shader
}

void fragment() {
	vec4 color = texture(SCREEN_TEXTURE, SCREEN_UV);
	float alpha = texture(TEXTURE, UV).a;
	if (invert_mask)
	{
		alpha = 1.0 - alpha;
	}
	color.a = alpha;
	COLOR = color;
}
