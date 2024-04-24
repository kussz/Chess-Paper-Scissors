#version 330
#ifdef GL_ES
precision mediump float;
#endif

uniform mat4 mvpMatrix;
layout (location = 10) in vec4 aPosition;
layout (location = 11) in vec4 aColor;
out vec4 color;
out float r;
void main()
{
	color = aColor;
	gl_Position=mvpMatrix*aPosition;
	r = 2.75-gl_Position.z;
}