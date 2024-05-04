shader_type spatial;
render_mode unshaded;

uniform samplerCube cubemap;

void fragment() {
	ALBEDO = texture(cubemap, (INV_VIEW_MATRIX * vec4(VIEW, 1)).xyz - CAMERA_POSITION_WORLD).xyz;
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
