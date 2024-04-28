#version 330
#ifdef GL_ES
precision mediump float;
#endif


uniform float u_time;
in vec2 mouse;
in vec4 position;
in vec4 color;
out vec4 outColor;
in float r;
in float r1;
in vec2 cellPos;

bool checkSquare(float i, float j)
{
    if(i/5-0.9>position.x-0.1&&i/5-0.9<position.x+0.1&&j/5-0.9>-position.y-0.1&&j/5-0.9<-position.y+0.1)
        return (true);
    return (false);
}


void main()
{
    int normcordx=int(round((position.x+0.9)*5));
    int normcordy=int(round((position.y+0.9)*5));
    vec2 normouse=mouse*2;
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
    if(checkSquare(normCellPos.x,normCellPos.y))
        gl_FragColor=vec4((gl_FragColor.x+0.5)*0.5,(gl_FragColor.y+0.5)*0.6,(gl_FragColor.z+0.5)*0.5,0.1);


    gl_FragColor=vec4(gl_FragColor.xyz*r*2,1);
}


