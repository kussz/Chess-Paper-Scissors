#version 330
#ifdef GL_ES
precision mediump float;
#endif



in float r;
in vec4 color;

void main()
{
	gl_FragColor = vec4(color.xyz*(r),color.w);
}


