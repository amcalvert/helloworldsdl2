using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public abstract class SurfaceElement : IDisposable
    {
        protected IntPtr _surface = IntPtr.Zero;

        public abstract void Draw(IntPtr screenSurface);

        public abstract void DisposeElement();

        public void Dispose()
        {
            DisposeElement();
            _surface = IntPtr.Zero;
        }
    }
}
