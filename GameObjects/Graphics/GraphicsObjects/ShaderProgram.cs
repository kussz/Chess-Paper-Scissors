using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameObjects.Graphics.GraphicsObjects;

public class ShaderProgram
{
    private readonly int _vertexShader = 0;
    private readonly int _fragmentShader = 0;
    private readonly int _program = 0;
    public ShaderProgram(string vertexfile, string fragmentfile)
    {
        _vertexShader = CreateShader(ShaderType.VertexShader, vertexfile);
        _fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentfile);
        _program = GL.CreateProgram();
        GL.AttachShader(_program, _vertexShader);
        GL.AttachShader(_program, _fragmentShader);
        GL.LinkProgram(_program);
        GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out var code);
        if (code != (int)All.True)
        {
            var infoLog = GL.GetProgramInfoLog(_program);
            throw new Exception($"Ошибка компиляции программы {_program}. ({infoLog})");
        }
        DeleteShader(_vertexShader);
        DeleteShader(_fragmentShader);
    }
    private int CreateShader(ShaderType shaderType, string shaderFile)
    {
        string shaderStr = File.ReadAllText(shaderFile);
        int shaderID = GL.CreateShader(shaderType);
        GL.ShaderSource(shaderID, shaderStr);
        GL.CompileShader(shaderID);
        GL.GetShader(shaderID, ShaderParameter.CompileStatus, out var code);
        Console.WriteLine(shaderID);
        if (code != (int)All.True)
        {
            var infoLog = GL.GetShaderInfoLog(shaderID);
            throw new Exception($"Ошибка компиляции шейдера {shaderID}. ({infoLog})");
        }
        return shaderID;
    }

    public void ActivateProgram()
    {
        GL.UseProgram(_program);
    }
    public void DeactivateProgram()
    {
        GL.UseProgram(0);
    }

    public void DeleteProgram()
    {
        GL.DeleteProgram(_program);
    }
    private void DeleteShader(int shader)
    {
        GL.DetachShader(_program, shader);
        GL.DeleteShader(shader);
    }

}