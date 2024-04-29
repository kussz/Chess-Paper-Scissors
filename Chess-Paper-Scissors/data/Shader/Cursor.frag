#version 330
#ifdef GL_ES
precision mediump float;
#endif



in vec2 mouse;
in vec4 position;


void main()
{
    vec2 normouse=mouse*2;
    float hip=sqrt((normouse.x+0.1-position.x)*(normouse.x+0.1-position.x)+(-normouse.y+0.1-position.y)*(-normouse.y+0.1-position.y));
    gl_FragColor = vec4(0,0,0,0);
    if(hip<0.03)
    {
        gl_FragColor=vec4(1,1,1,1);
    }
}


