using MyFirstSdlGame.Assets;
using SDL2;
using System;

namespace helloworld
{
    public class BaseGameWindow : IDisposable
    {
        private IntPtr _window = IntPtr.Zero;
        private IntPtr _windowSurface = IntPtr.Zero;
        private bool _quitRequested;

        public BaseGameWindow()
        {
            InitSdl();
            InitWindow();
        }

        private void InitSdl()
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0) throw new Exception($"sdl count not initialise! {SDL.SDL_GetError()}");
        }

        private void InitWindow()
        {
            _window = IntPtr.Zero;
            _window = SDL.SDL_CreateWindow("Lolz",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                1080, 800,
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            if (_window == IntPtr.Zero) throw new Exception($"Failed to initialise window window {SDL.SDL_GetError()}");
        }

        public void Run()
        {
            InitSurface();

            while (!_quitRequested)
            {
                HandleEvents();
                DrawContent();
                SDL.SDL_UpdateWindowSurface(_window);
            }

            Dispose();
        }

        private void HandleEvents()
        {
            while (SDL.SDL_PollEvent(out SDL.SDL_Event sdlEvent) != 0)
            {
                switch (sdlEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        _quitRequested = true;
                        break;
                    default:
                        break;
                }
            }
        }


        private void InitSurface()
        {
            _windowSurface = SDL.SDL_GetWindowSurface(_window);
        }

        private void DrawContent()
        {
            var image = new Image(@"C:\Users\amcalvert\source\repos\games\helloworld\Assets\bmps\preview.bmp");
            image.Draw(_windowSurface);
        }

        public void Dispose()
        {
            SDL.SDL_DestroyWindow(_window);
            _window = IntPtr.Zero;
            SDL.SDL_Quit();
        }
    }
}
