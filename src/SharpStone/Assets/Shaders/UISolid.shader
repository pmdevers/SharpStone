#shader vertex
#version 430 core

layout(location = 0) in vec4 in_position;
layout(location = 1) in vec4 in_color;

out vec4 vColor;

void main() {
    gl_Position = in_position;
    vColor = in_color;
}

#shader fragment
#version 430 core

layout(location = 0) out vec4 color; 

in vec4 vColor;

void main() {
    color = vColor;
}