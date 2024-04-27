#version 330
#extension GL_ARB_explicit_uniform_location : require
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ES
precision mediump float;
#endif

layout (location = 0) uniform mat4 mvpMatrix;
uniform float pcColor;
layout (location = 10) in vec4 aPosition;
layout (location = 11) in vec2 aTexture;
out float r;
out float pcCol;
out vec2 textureCoordinate;

void main()
{
	
	pcCol = pcColor;
	textureCoordinate = aTexture;
	gl_Position=mvpMatrix*aPosition;
	r = 1-((mvpMatrix*aPosition).z/4);
}