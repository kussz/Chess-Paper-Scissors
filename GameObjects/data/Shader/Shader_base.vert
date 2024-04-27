#version 330
#extension GL_ARB_explicit_uniform_location : require
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ES
precision mediump float;
#endif

layout (location = 0) uniform mat4 mvpMatrix;
layout (location = 10) in vec4 aPosition;
layout (location = 11) in vec4 aColor;
layout (location = 1) uniform vec2 u_mouse;
layout (location = 2) uniform vec2 u_CellPos;
out vec2 mouse;
out vec4 position;
out vec4 color;
out float r;
out float r1;
out vec2 cellPos;
void main()
{
    cellPos=u_CellPos;
    color = aColor;
    position=aPosition;
    mouse=u_mouse;
    float stx = mouse.y/6;
    float sty = mouse.x/6;
    r1=sqrt((mouse.x-position.x)*(mouse.x-position.x)+(-mouse.y-position.y)*(-mouse.y-position.y));
    r=1-((mvpMatrix*aPosition).z/4);
    gl_Position = mvpMatrix*aPosition;
}