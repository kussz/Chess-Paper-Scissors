﻿#version 330
#ifdef GL_ES
precision mediump float;
#endif



uniform sampler2D textureObject;
in float r;
in float pcCol;
in vec2 textureCoordinate;

void main()
{
	float intensity = 0.7;
	gl_FragColor = texture2D(textureObject, textureCoordinate);
	gl_FragColor = vec4(gl_FragColor.xyz*(r+0.3),gl_FragColor.w);
if(pcCol<0.5)
	gl_FragColor = gl_FragColor * vec4(intensity,intensity,1,1);
else
	gl_FragColor = gl_FragColor * vec4(1,intensity,intensity,1);

}


