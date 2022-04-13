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

namespace Swordfish.Core.Rendering
{
    internal class SpriteRenderer
    {
        public SpriteRenderer()
        {
            
        } 

        public void Draw(Scene scene, int vertexArrayObject, Shader shader, GameCamera camera, int indicesLength)
        {
           


            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            var spriteEntities = scene.Entities
                .Where(e => e.HasComponent<Sprite>())
                .Where(e => e.HasComponent<Transform>());

            foreach (var entity in spriteEntities) {
                Console.WriteLine(entity.id);
                var spriteComponent = entity.GetComponent<Sprite>();
                var transformComponent = entity.GetComponent<Transform>();

                spriteComponent.BindTexture(TextureUnit.Texture0);
                shader.Use();

                var model = Matrix4.Identity;
                // Order MUST be scale, rotate, translate.
                model *= Matrix4.CreateScale(spriteComponent.Width, spriteComponent.Height, 0f);
                model *= Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(transformComponent.Rotation.Z));
                model *= Matrix4.CreateTranslation(transformComponent.Position.X, transformComponent.Position.Y, 0f);

                shader.SetMatrix4("model", model);
                shader.SetMatrix4("view", camera.GetViewMatrix());
                shader.SetMatrix4("projection", camera.GetProjectionMatrix());


                GL.BindVertexArray(vertexArrayObject);

                GL.DrawElements(PrimitiveType.Triangles, indicesLength, DrawElementsType.UnsignedInt, 0);

            }


        }
    }
}
