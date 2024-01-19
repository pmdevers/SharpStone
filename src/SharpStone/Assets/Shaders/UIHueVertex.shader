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
varying vec3 uv;
varying vec3 position;
uniform float hue;
float HUE2RGB(float p, float q, float t)
{
  if (t < 0.0) t += 1.0;
  if (t > 1.0) t -= 1.0;
  if (t < 1.0 / 6.0) return p + (q - p) * 6.0 * t;
  if (t < 1.0 / 2.0) return q;
  if (t < 2.0 / 3.0) return p + (q - p) * (2.0 / 3.0 - t) * 6.0;
  return p;
}
vec3 HSL2RGB(float h, float s, float l)
{
  float r, g, b;
  if (s == 0.0) r = g = b = l;
  else
  {
    float q = (l < 0.5 ? l * (1.0 + s) : l + s - l * s);
    float p = 2.0 * l - q;
    r = HUE2RGB(p, q, h + 1.0 / 3.0);
    g = HUE2RGB(p, q, h);
    b = HUE2RGB(p, q, h - 1.0 / 3.0);
  }
  return vec3(r, g, b);
}
void main(void)
{
  int posx = int(position.x);
  int posy = int(position.y);
  if (posx == 10 || posx == 25) gl_FragColor = vec4(0, 0, 0, 1);
  else if (position.x >= 10.0 && position.x <= 25.0) gl_FragColor = (posy == 0 || posy == 149 ? vec4(0, 0, 0, 1) : vec4(HSL2RGB(uv.y, 1.0, 0.5), 1));
  else if (position.x < 8.0)
  {
    float distance = abs(position.y - hue);
    if (int(6.0 - position.x) == int(distance)) gl_FragColor = vec4(0, 0, 0, 1);
    else if (6.0 - position.x > distance) gl_FragColor = (int(position.x) == 0 ? vec4(0, 0, 0, 1) : vec4(HSL2RGB(hue / 150.0, 1.0, 0.5), 1));
    else gl_FragColor = vec4(0, 0, 0, 0);
  }
  else gl_FragColor = vec4(0, 0, 0, 0);
}