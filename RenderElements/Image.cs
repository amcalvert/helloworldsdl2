using SDL2;
using System;

namespace MyFirstSdlGame.Assets
{
    public class Image : IDisposable
    {
        private IntPtr _imageSurface = IntPtr.Zero; 

        public Image(string imageLoadPath)
        {
            _imageSurface = SDL.SDL_LoadBMP(imageLoadPath);
            if(_imageSurface == IntPtr.Zero)
            {
                throw new Exception($"Filed to load image - {SDL.SDL_GetError()}");
            }
        }

        public void Draw(IntPtr screenSurface)
        {
            SDL.SDL_BlitSurface(_imageSurface, IntPtr.Zero, screenSurface, IntPtr.Zero);
        }

        public void Dispose()
        {
            SDL.SDL_FreeSurface(_imageSurface);
            _imageSurface = IntPtr.Zero;
        }
    }
}
