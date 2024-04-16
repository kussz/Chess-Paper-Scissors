#version 330
#ifdef GL_ES
precision mediump float;
#endif

uniform mat4 mvpMatrix;
uniform float pcColor;
in vec4 aPosition;
layout (location = 14) in vec2 aTexture;
out float r;
out float pcCol;
out vec2 textureCoordinate;

void main()
{
	
	pcCol = pcColor;
	textureCoordinate = aTexture;
	gl_Position=mvpMatrix*aPosition;
	r = 2.75-gl_Position.z;
}