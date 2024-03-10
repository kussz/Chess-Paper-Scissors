#version 330
#ifdef GL_ES
precision mediump float;
#endif


uniform float u_time;
in vec2 mouse;
in vec2 resolution;
in vec4 position;
in vec4 color;
out vec4 outColor;
in float r;
in float r1;
in vec2 cellPos;

void main()
{
    int normcordx=int(round((position.x+0.9)*5));
    int normcordy=int(round((position.y+0.9)*5));
    vec2 normouse=mouse*2;
    float hip=sqrt((normouse.x+0.1-position.x)*(normouse.x+0.1-position.x)+(-normouse.y+0.1-position.y)*(-normouse.y+0.1-position.y));
    float normcubex=int(round((normouse.x)*5))+0.5;
    float normcubey=int(round((-normouse.y)*5))+0.5;
    vec2 normCellPos=vec2(cellPos.x+1,cellPos.y+1);
    if((normcordx%2+normcordy%2)%2==0)
    {
        gl_FragColor=vec4(0.9,0.9,0.99,1);//*r*2*(2-r1*1.1);
    }
    else
    {
        gl_FragColor=vec4(0.1,0.1,0.1,1);
    }
    if(normCellPos.x/5-0.9>position.x-0.1&&normCellPos.x/5-0.9<position.x+0.1&&normCellPos.y/5-0.9>-position.y-0.1&&normCellPos.y/5-0.9<-position.y+0.1)
        gl_FragColor=vec4(1,0,0,0.2);
    if(hip<0.03)
    {
        gl_FragColor=vec4(1,1,1,1);
    }
    gl_FragColor=vec4(gl_FragColor.xyz*r*2,1);
}


