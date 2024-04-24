#version 330
#ifdef GL_ES
precision mediump float;
#endif

uniform mat4 mvpMatrix;
layout (location = 10) in vec4 aPosition;
layout (location = 11) in vec4 aColor;
uniform vec2 u_mouse;
uniform vec2 u_resolution;
uniform vec2 u_CellPos;
out vec2 mouse;
out vec2 resolution;
out vec4 position;
out vec4 color;
out float r;
out float r1;
out vec2 cellPos;
void main()
{
    cellPos=u_CellPos;
    color = aColor;
    resolution = u_resolution;
    position=aPosition;
    mouse=u_mouse;
    float stx = mouse.y/6;
    float sty = mouse.x/6;
    r1=sqrt((mouse.x-position.x)*(mouse.x-position.x)+(-mouse.y-position.y)*(-mouse.y-position.y));
    r=1-((mvpMatrix*aPosition).z/4);
    gl_Position = mvpMatrix*aPosition;
}