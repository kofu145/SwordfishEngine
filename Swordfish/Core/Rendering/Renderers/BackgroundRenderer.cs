using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Swordfish.ECS;
using Swordfish.Components;

namespace Swordfish.Core.Rendering.Renderers
{
    class BackgroundRenderer
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

        public BackgroundRenderer(int width, int height)
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

        public void Render(Scene scene, GameCamera camera, int width, int height)
        {
            if (scene.HasBackgroundImageSet())
            {
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                // set background image
                scene.BindTexture(TextureUnit.Texture0);
                shader.Use();

                var model = Matrix4.Identity;

                var dimensions = scene.GetBgDimensions();
                // Get which side to scale up towards
                var imageWidth = width / dimensions.X > height / dimensions.Y ? width: dimensions.X * height / dimensions.Y;
                var imageHeight = height / dimensions.Y > width / dimensions.X ? height : dimensions.Y * width / dimensions.X;

                // Order MUST be scale, rotate, translate.
                model *= Matrix4.CreateScale(imageWidth, imageHeight, 0f);
                model *= Matrix4.CreateTranslation(-imageWidth / 2, -imageHeight / 2, -10f);
                //model *= Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(transformComponent.Rotation.Z));


                shader.SetMatrix4("model", model);
                shader.SetMatrix4("view", camera.GetViewMatrix());
                shader.SetMatrix4("projection", camera.GetProjectionMatrix());

                shader.SetFloat("alpha_threshold", .5f);
                shader.SetVector3("color", new Vector3(1f, 1f, 1f));

                GL.BindVertexArray(vertexArrayObject);

                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }

        }
    }
}
