using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Swordfish.ECS;
using Swordfish.Components;
using System.Linq;
using OpenTK.Audio;

namespace Swordfish.Core.Rendering.Renderers
{
    internal class SpriteRenderer
    {

        private int vertexBufferObject;
        private int vertexArrayObject;

        private Shader shader;

        private int elementBufferObject;

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

        uniform float alpha_threshold;
        uniform sampler2D texture0;
        uniform vec3 color;

        void main()
        {
            outputColor = vec4(color, 1.0) * texture(texture0, texCoord);
            
            if(outputColor.a <= alpha_threshold)
                discard;

        }";

        private readonly float[] quadVertices =
        {
            // Position         Texture coordinates
             1.0f, 1.0f, 0.0f, 1.0f, 1.0f, // top right
             1.0f, 0.0f, 0.0f, 1.0f, 0.0f, // bottom right
             0.0f, 0.0f, 0.0f, 0.0f, 0.0f, // bottom left
             0.0f, 1.0f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] indices =
        {
            // Note that indices start at 0!
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };

        public SpriteRenderer(int width, int height)
        {
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * sizeof(float), quadVertices, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader = new Shader(vertShader, fragShader, false);

            shader.Use();

            var vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            
        } 

        public void Draw(Scene scene, GameCamera camera)
        {
           
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.)
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            

            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);


            var spriteEntities = scene.Entities
                .Where(e => e.HasComponent<Sprite>())
                .Where(e => e.HasComponent<Transform>());

            foreach (var entity in spriteEntities) {
                // Console.WriteLine(entity.id);
                var spriteComponent = entity.GetComponent<Sprite>();
                var transformComponent = entity.GetComponent<Transform>();

                spriteComponent.BindTexture(TextureUnit.Texture0);
                shader.Use();

                var model = Matrix4.Identity;
                // Order MUST be scale, rotate, translate.
                
                model *= Matrix4.CreateScale(spriteComponent.Width * transformComponent.Scale.X, spriteComponent.Height * transformComponent.Scale.Y, 0f);

                model *= Matrix4.CreateTranslation(-spriteComponent.Origin.X * transformComponent.Scale.X, -spriteComponent.Origin.Y * transformComponent.Scale.Y, 0f);

                model *= Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(transformComponent.Rotation.X));
                model *= Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(transformComponent.Rotation.Y));
                model *= Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(transformComponent.Rotation.Z));

                model *= Matrix4.CreateTranslation(spriteComponent.Origin.X * transformComponent.Scale.X, spriteComponent.Origin.Y * transformComponent.Scale.Y, 0f);
                model *= Matrix4.CreateTranslation(
                    transformComponent.Position.X + -spriteComponent.Origin.X * transformComponent.Scale.X,
                    transformComponent.Position.Y + -spriteComponent.Origin.Y * transformComponent.Scale.Y,
                    transformComponent.Position.Z
                    );



                shader.SetMatrix4("model", model);
                shader.SetMatrix4("view", camera.GetViewMatrix());
                shader.SetMatrix4("projection", camera.GetProjectionMatrix());

                shader.SetFloat("alpha_threshold", .5f);
                shader.SetVector3("color", spriteComponent.Color);


                GL.BindVertexArray(vertexArrayObject);

                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            }


        }

        public void OnUnload()
        {
            // Delete all the resources.
            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);

            GL.DeleteProgram(shader.Handle);
        }
    }
}
