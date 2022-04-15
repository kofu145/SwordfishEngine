using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using System.Linq;
using Swordfish.Components;
using Swordfish.Components.UI;
using OpenTK.Mathematics;

namespace Swordfish.Core.Rendering.Renderers
{
    internal class TextRenderer
    {
        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private Shader shader;

        private string vertShader = @"#version 330 core

        layout(location = 0) in vec3 aPosition;

        layout(location = 1) in vec2 aTexCoord;
        out vec2 texCoord;

        //uniform mat4 u_MVP;

        uniform mat4 model;
        uniform mat4 view;
        uniform mat4 projection;

        void main(void)
        {
            texCoord = aTexCoord;

            gl_Position = vec4(aPosition, 1.0) * model * view * projection;
        }";

        private string fragShader = @"#version 330

        out vec4 outputColor;

        in vec2 texCoord;

        uniform sampler2D texture0;
        uniform vec3 textColor;

        void main()
        {
            vec4 sampled = vec4(1.0, 1.0, 1.0, texture(texture0, texCoord).r);
            outputColor = vec4(textColor, 1.0) * sampled;
        }";

        private readonly float[] quadVertices =
        {
            // Position         Texture coordinates
             1.0f, 1.0f, 0.0f, 1.0f, 0.0f, // top right
             1.0f, 0.0f, 0.0f, 1.0f, 1.0f, // bottom right
             0.0f, 0.0f, 0.0f, 0.0f, 1.0f, // bottom left
             0.0f, 1.0f, 0.0f, 0.0f, 0.0f  // top left
        };
        private readonly uint[] indices =
        {
            // Note that indices start at 0!
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };

        public TextRenderer()
        {
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * sizeof(float), quadVertices, BufferUsageHint.DynamicDraw);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            GL.BindVertexArray(vertexArrayObject);

            shader = new Shader(vertShader, fragShader, false);
            shader.Use();


        }

        public void Render(Scene scene, CharTexture charTexture, GameCamera camera)
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            var textEntities = scene.Entities
                .Where(e => e.HasComponent<Label>())
                .Where(e => e.HasComponent<Transform>());

            foreach (var entity in textEntities)
            {
                var labelComponent = entity.GetComponent<Label>();
                var transformComponent = entity.GetComponent<Transform>();
                float totalWidth = 0;

                shader.Use();

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindVertexArray(vertexArrayObject);

                // initial for loop to grab bitmap length for full string (for centering)
                foreach(var c in labelComponent.Text)
                {
                    if (charTexture.characters.ContainsKey(c) == false && c == ' ')
                        Console.WriteLine("true");
                    //Console.WriteLine(c);
                    
                    Character ch = charTexture.characters[c];
                    
                    totalWidth += c is ' ' ? 14f : 0f;
                    totalWidth += ch.Bearing.X + ch.Size.X * labelComponent.FontSize;

                }

                float char_x = 0.0f;
                foreach (var c in labelComponent.Text)
                {
                    if (charTexture.characters.ContainsKey(c) == false)
                        continue;
                    Character ch = charTexture.characters[c];
                    var x = c;
                    float w = ch.Size.X * labelComponent.FontSize;
                    float h = ch.Size.Y * labelComponent.FontSize;
                    float xrel = char_x + ch.Bearing.X * labelComponent.FontSize;
                    float yrel = (ch.Size.Y - ch.Bearing.Y) * labelComponent.FontSize;

                    // advance cursors for next glyph (advance is number of 1/64 pixels)
                    char_x += (ch.Advance >> 6) * labelComponent.FontSize; // Bitshift by 6 to get value in pixels (2^6 = 64 (divide amount of 1/64th pixels by 64 to get amount of pixels))

                    var model = Matrix4.Identity;
                    // Order MUST be scale, rotate, translate.
                    model *= Matrix4.CreateScale(new Vector3(w, h, 1.0f));
                    model *= Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(transformComponent.Rotation.Z));
                    model *= Matrix4.CreateTranslation(new Vector3(xrel, yrel, 0.0f));
                    // origin
                    model *= Matrix4.CreateTranslation(new Vector3(transformComponent.Position.X-totalWidth/2, transformComponent.Position.Y, 0.0f));


                    shader.SetMatrix4("model", model);
                    shader.SetMatrix4("view", camera.GetViewMatrix());
                    shader.SetMatrix4("projection", camera.GetProjectionMatrix());

                    shader.SetVector3("textColor", new Vector3(0.5f, 0.8f, 0.2f));

                    shader.SetInt("texture0", 0);
                    // Render glyph texture over quad
                    GL.BindTexture(TextureTarget.Texture2D, ch.TextureID);


                    // Render quad
                    GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

                }

                GL.BindVertexArray(0);
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }    

            

            
        }

        private void Draw()
        {

        }
    }
}
