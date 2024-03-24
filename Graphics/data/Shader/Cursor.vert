#version 330
#ifdef GL_ES
precision mediump float;
#endif

uniform mat4 mvpMatrix;
in vec4 aPosition;
in vec4 aColor;
out vec4 color;
out float r;
void main()
{
	color = aColor;
	gl_Position=mvpMatrix*aPosition;
	r = 2.75-gl_Position.z;
}
