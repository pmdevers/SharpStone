#shader vertex
#version 450 core

layout(location = 0) in vec4 in_position;
layout(location = 1) in vec4 in_color;

layout(std140, binding = 0) uniform Camera
{
	mat4 u_ViewProjection;
};

out vec4 vColor;

uniform mat4 u_Matrix;

void main() {
    gl_Position = u_ViewProjection * in_position; //projection * view * in_position;
    vColor = in_color;
}

#shader fragment
#version 450 core

layout(location = 0) out vec4 color; 

in vec4 vColor;

void main() {
    color = vColor;
}