using MyFirstSdlGame.Elements;
using SDL2;
using System;
using System.Collections.Generic;

namespace helloworld
{
    public class BaseGameWindow : IDisposable
    {
        private IntPtr _window = IntPtr.Zero;
        private IntPtr _windowSurface = IntPtr.Zero;
        private bool _quitRequested;
        private IList<SurfaceElement> _activeElements = new List<SurfaceElement>();
        private IDictionary<string, SurfaceElement> _loadedElements = new Dictionary<string, SurfaceElement>();

        public BaseGameWindow()
        {
            InitSdl();
            InitWindow();
        }

        private void InitSdl()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0) throw new Exception($"sdl count not initialise! {SDL.SDL_GetError()}");
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
            LoadContent();

            _activeElements.Add(_loadedElements["default"]);
            while (!_quitRequested)
            {
                HandleEvents();
                DrawContent();
                SDL.SDL_UpdateWindowSurface(_window);
            }

            TearDownContent();
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
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        HandleKeyDown(sdlEvent);
                        break;
                    default:
                        break;
                }
            }
        }

        private void HandleKeyDown(SDL.SDL_Event keyDownEvent)
        {
            _activeElements.Clear();
            switch (keyDownEvent.key.keysym.sym)
            {
                case SDL.SDL_Keycode.SDLK_UP:
                    _activeElements.Add(_loadedElements["up"]);
                    break;
                case SDL.SDL_Keycode.SDLK_LEFT:
                    _activeElements.Add(_loadedElements["left"]);
                    break;
                case SDL.SDL_Keycode.SDLK_RIGHT:
                    _activeElements.Add(_loadedElements["right"]);
                    break;
                case SDL.SDL_Keycode.SDLK_DOWN:
                    _activeElements.Add(_loadedElements["down"]);
                    break;
                default:
                    _activeElements.Add(_loadedElements["default"]);
                    break;
            }
        }

        private void InitSurface()
        {
            _windowSurface = SDL.SDL_GetWindowSurface(_window);
        }

        private void LoadContent()
        {
            _loadedElements = new Dictionary<string, SurfaceElement>()
            {
                { "default",  new Image(@".\Assets\bmps\press.bmp") },
                { "up", new Image(@".\Assets\bmps\up.bmp") },
                { "down",  new Image(@".\Assets\bmps\down.bmp") },
                { "left",  new Image(@".\Assets\bmps\left.bmp") },
                { "right",  new Image(@".\Assets\bmps\right.bmp") }
            };
        }

        private void DrawContent()
        {
            foreach (var drawElement in _activeElements)
            {
                drawElement.Draw(_windowSurface);
            }
        }

        private void TearDownContent()
        {
            foreach (var element in _loadedElements.Values)
            {
                element.Dispose();
            }
        }

        public void Dispose()
        {
            SDL.SDL_DestroyWindow(_window);
            _window = IntPtr.Zero;
            SDL.SDL_Quit();
        }
    }
}
