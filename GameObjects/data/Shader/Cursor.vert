#version 330
#ifdef GL_ES
precision mediump float;
#endif

uniform mat4 mvpMatrix;
layout (location = 10) in vec4 aPosition;
uniform vec2 u_mouse;
out vec2 mouse;
out vec4 position;
void main()
{
    position=aPosition;
    mouse=u_mouse;
    gl_Position = mvpMatrix*aPosition;
}
