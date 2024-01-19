#shader vertex

#version 140
uniform vec3 position;
uniform mat4 ui_projection_matrix;
in vec3 in_position;
in vec2 in_uv;
out vec2 uv;
void main(void)
{
  uv = in_uv;
  
  gl_Position = ui_projection_matrix * vec4(position + in_position, 1);
}

#shader fragment
#version 140
uniform sampler2D active_texture;
in vec2 uv;
void main(void)
{
  gl_FragColor = texture2D(active_texture, uv);
}
