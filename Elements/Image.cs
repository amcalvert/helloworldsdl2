using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public class Image : SurfaceElement, IDisposable
    {
        public Image(string imageLoadPath)
        {
            _surface = SDL.SDL_LoadBMP(imageLoadPath);
            if (_surface == IntPtr.Zero)
            {
                throw new Exception($"Filed to load image - {SDL.SDL_GetError()}");
            }
        }

        public override void Draw(IntPtr screenSurface)
        {
            SDL.SDL_BlitSurface(_surface, IntPtr.Zero, screenSurface, IntPtr.Zero);
        }
    }
}
