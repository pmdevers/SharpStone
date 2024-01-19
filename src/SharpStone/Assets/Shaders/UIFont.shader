#shader vertex
#version 140
uniform vec2 position;
uniform mat4 ui_projection_matrix;
in vec3 in_position;
in vec2 in_uv;
out vec2 uv;
void main(void)
{
  uv = in_uv;
  gl_Position = ui_projection_matrix * vec4(in_position.x + position.x, in_position.y + position.y, 0, 1);
}


#shader fragment
#version 140
uniform sampler2D active_texture;
uniform vec3 color;
in vec2 uv;
void main(void)
{
  vec4 t = texture2D(active_texture, uv);
  gl_FragColor = vec4(t.rgb * color, t.a);
}
