#version 330
#extension GL_ARB_explicit_uniform_location : require
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ES
precision mediump float;
#endif

layout (location = 0) uniform mat4 mvpMatrix;
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