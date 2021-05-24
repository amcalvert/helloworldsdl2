using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public class Image : SurfaceElement, IDisposable
    {
        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;

        public Image(string imageLoadPath, IntPtr window, int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;

            var loadSurface = SDL_image.IMG_Load(imageLoadPath);
            if (loadSurface == IntPtr.Zero)
            {
                throw new Exception($"Filed to load image - {SDL.SDL_GetError()}");
            }

            _surface = SDL.SDL_ConvertSurfaceFormat(loadSurface, SDL.SDL_GetWindowPixelFormat(window), 0);
            if (_surface == IntPtr.Zero)
            {
                throw new Exception($"Filed to optomise image - {SDL.SDL_GetError()}");
            }

            SDL.SDL_FreeSurface(loadSurface);
        }

        public override void Draw(IntPtr drawSurface)
        {
            SDL.SDL_Rect stretchRetangle;
            stretchRetangle.x = _x;
            stretchRetangle.y = _y;
            stretchRetangle.w = _width;
            stretchRetangle.h = _height;

            SDL.SDL_BlitScaled(_surface, IntPtr.Zero, drawSurface, ref stretchRetangle);
        }
    }
}
