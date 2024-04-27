#version 330
#extension GL_ARB_explicit_uniform_location : require
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ES
precision mediump float;
#endif

layout (location = 0) uniform mat4 mvpMatrix;
layout (location = 10) in vec4 aPosition;
layout (location = 1) uniform vec2 u_mouse;
out vec2 mouse;
out vec4 position;
void main()
{
    position=aPosition;
    mouse=u_mouse;
    gl_Position = mvpMatrix*aPosition;
}
