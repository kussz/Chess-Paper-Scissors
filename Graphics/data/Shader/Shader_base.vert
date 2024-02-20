#version 330
#extension GL_ARB_explicit_uniform_location : require

layout(location = 0) in vec3 aPosition;
in vec4 aColor;
uniform vec2 angle;
uniform vec2 rtCoeff;
uniform vec2 u_mouse;
uniform vec2 u_resolution;
uniform vec2 u_CellPos;
out vec2 mouse;
out vec2 resolution;
out vec3 position;
out vec4 color;
out float r;
out float r1;
out vec2 cellPos;
mat3 rotatez(float angle){
    return mat3(
    cos(angle), -sin(angle), 0,
    sin(angle), cos(angle), 0,
    0, 0, 1
    );
}
mat3 rotatex(float angle){
    return mat3(
    1,0,0,
    0,cos(angle),-sin(angle),
    0,sin(angle),cos(angle)
    );
}
mat3 rotatey(float angle){
    return mat3(
    cos(angle),0,sin(angle),
    0,1,0,
    -sin(angle),0,cos(angle)
    );
}
void main()
{
    cellPos=u_CellPos;
    color = aColor;
    resolution = u_resolution;
    position=aPosition;
    mouse=u_mouse;
    float stx = mouse.y/6;
    float sty = mouse.x/6;
    vec3 bPosition = position.xyz*rotatex(stx-0.2*(-angle.x+3.14/2))*rotatey(sty)*rotatez(angle.x);
    r1=sqrt((mouse.x-position.x)*(mouse.x-position.x)+(-mouse.y-position.y)*(-mouse.y-position.y));
    r=(bPosition.z+0.5);
    gl_Position = vec4(bPosition, -bPosition.z+1.3);
}