#version 330
#ifdef GL_ES
precision mediump float;
#endif


uniform float u_time;
in float r;
in vec2 mouse;
in vec2 resolution;
in vec3 position;
in vec4 color;
out vec4 outColor;

void main()
{
	gl_FragColor = color*(r);
}


