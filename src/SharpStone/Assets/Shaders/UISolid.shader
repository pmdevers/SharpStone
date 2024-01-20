#shader vertex
#version 430 core

layout(location = 0) in vec4 in_position;
layout(location = 1) in vec4 in_color;

out vec4 vColor;

uniform mat4 u_Matrix;

void main() {
    gl_Position = u_Matrix * in_position; //projection * view * in_position;
    vColor = in_color;
}

#shader fragment
#version 430 core

layout(location = 0) out vec4 color; 

in vec4 vColor;

void main() {
    color = vColor;
}