using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public class Texture : SurfaceElement, IDisposable
    {
        public Texture(string textureLoadPath, IntPtr windowRendererPointer)
        {
            var loadSurface = SDL_image.IMG_Load(textureLoadPath);
            if (loadSurface == IntPtr.Zero)
            {
                throw new Exception($"Filed to load image - {SDL.SDL_GetError()}");
            }

            _surface = SDL.SDL_CreateTextureFromSurface(windowRendererPointer, loadSurface);
            if (_surface == IntPtr.Zero)
            {
                throw new Exception($"Filed to load texture - {SDL.SDL_GetError()}");
            }

            SDL.SDL_FreeSurface(loadSurface);
        }

        public override void DisposeElement()
        {
            SDL.SDL_DestroyTexture(_surface);
        }

        public override void Draw(IntPtr rendererPointer)
        {
            SDL.SDL_RenderCopy(rendererPointer, _surface, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
