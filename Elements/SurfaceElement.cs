using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public abstract class SurfaceElement : IDisposable
    {
        protected IntPtr _surface = IntPtr.Zero;

        public abstract void Draw(IntPtr screenSurface);

        public void Dispose()
        {
            SDL.SDL_FreeSurface(_surface);
            _surface = IntPtr.Zero;
        }
    }
}
