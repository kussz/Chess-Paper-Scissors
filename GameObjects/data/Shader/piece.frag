#version 330
#ifdef GL_ES
precision mediump float;
#endif



uniform sampler2D textureObject;
in float r;
in float pcCol;
in vec2 textureCoordinate;

void main()
{
	gl_FragColor = texture2D(textureObject, textureCoordinate);
	gl_FragColor = vec4(gl_FragColor.xyz*r,gl_FragColor.w);
if(pcCol<0.5)
	gl_FragColor = gl_FragColor * vec4(0.5,0.5,1,1);
else
	gl_FragColor = gl_FragColor * vec4(1,0.5,0.5,1);

}


