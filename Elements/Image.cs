using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public class Image : SurfaceElement, IDisposable
    {
        public Image(string imageLoadPath, IntPtr windowSurface)
        {
            var loadSurface = SDL.SDL_LoadBMP(imageLoadPath);
            if (loadSurface == IntPtr.Zero)
            {
                throw new Exception($"Filed to load image - {SDL.SDL_GetError()}");
            }

            _surface = SDL.SDL_ConvertSurface(loadSurface, windowSurface, 0);
            if (_surface == IntPtr.Zero)
            {
                throw new Exception($"Filed to optomise image - {SDL.SDL_GetError()}");
            }

            SDL.SDL_FreeSurface(loadSurface);
        }

        public override void Draw(IntPtr screenSurface)
        {
            SDL.SDL_BlitSurface(_surface, IntPtr.Zero, screenSurface, IntPtr.Zero);
        }
    }
}
