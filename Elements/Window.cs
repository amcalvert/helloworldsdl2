using SDL2;
using System;

namespace MyFirstSdlGame.Elements
{
    public class Window : IDisposable
    {
        public Window(string title, int width, int height)
        {
            CreateWindow(title, width, height);

            WindowSurfacePointer = SDL.SDL_GetWindowSurface(WindowPointer);

            CreateRenderer();
        }

        private void CreateWindow(string title, int width, int height)
        {
            WindowPointer = SDL.SDL_CreateWindow(title,
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                width, height,
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            if (WindowPointer == IntPtr.Zero) throw new Exception($"Failed to initialise window - {SDL.SDL_GetError()}");
        }

        private void CreateRenderer()
        {
            WindowRendererPointer = SDL.SDL_CreateRenderer(WindowPointer, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if(WindowRendererPointer == IntPtr.Zero) throw new Exception($"Failed to initialise window renderer - {SDL.SDL_GetError()}");

            SDL.SDL_SetRenderDrawColor(WindowRendererPointer, 0xFF, 0xFF, 0xFF, 0xFF);
        }

        public IntPtr WindowPointer { get; private set; } = IntPtr.Zero;
        public IntPtr WindowSurfacePointer { get; } = IntPtr.Zero;
        public IntPtr WindowRendererPointer { get; private set; } = IntPtr.Zero;

        public void Dispose()
        {
            SDL.SDL_DestroyRenderer(WindowRendererPointer);
            SDL.SDL_DestroyWindow(WindowPointer);
            WindowPointer = IntPtr.Zero;
        }
    }
}
