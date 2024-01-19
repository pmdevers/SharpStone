#shader vertex
#version 140
uniform mat4 ui_projection_matrix;
uniform mat4 model_matrix;
attribute vec3 in_position;
attribute vec3 in_uv;
varying vec3 uv;
varying vec3 position;
void main(void)
{
  position = in_position;
  uv = in_uv;
  gl_Position = ui_projection_matrix * model_matrix * vec4(in_position, 1);
}


#shader fragment
#version 140
uniform vec3 hue;
uniform vec2 sel;
varying vec3 uv;
varying vec3 position;
void main(void)
{
  int posx = int(position.x);
  int posy = int(position.y);
  if (posx == 0 || posx == 149 || posy == 0 || posy == 149) gl_FragColor = vec4(0, 0, 0, 1);
  else
  {
    vec3 gradient = mix(vec3(1, 1, 1), hue, uv.x);
    float distance = (uv.x - sel.x) * (uv.x - sel.x) + (uv.y - sel.y) * (uv.y - sel.y);
    bool ring3 = (distance >= 0.0005 && distance < 0.001);
    bool ring2 = (distance >= 0.00025 && distance < 0.0005);
    bool ring1 = (distance >= 0.0001 && distance < 0.00025);
    gl_FragColor = (ring3 || ring1 ? vec4(0, 0, 0, 1) : (ring2 ? vec4(1, 1, 1, 1) : vec4(mix(vec3(0, 0, 0), gradient, uv.y), 1)));
  }
}