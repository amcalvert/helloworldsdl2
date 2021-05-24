using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public class Window : IDisposable
    {
        public Window(string title, int width, int height)
        {
            WindowPointer = SDL.SDL_CreateWindow(title,
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            width, height,
            SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            if (WindowPointer == IntPtr.Zero) throw new Exception($"Failed to initialise window window {SDL.SDL_GetError()}");

            WindowSurfacePointer = SDL.SDL_GetWindowSurface(WindowPointer);
        }

        public IntPtr WindowPointer { get; private set; } = IntPtr.Zero;
        public IntPtr WindowSurfacePointer { get; } = IntPtr.Zero;

        public void Dispose()
        {
            SDL.SDL_DestroyWindow(WindowPointer);
            WindowPointer = IntPtr.Zero;
        }
    }
}
