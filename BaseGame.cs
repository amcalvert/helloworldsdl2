﻿using MyFirstSdlGame.Elements;
using SDL2;
using System;
using System.Collections.Generic;

namespace helloworld
{
    public class BaseGame : IDisposable
    {
        private bool _quitRequested;
        private Window _window;
        private IList<SurfaceElement> _activeElements = new List<SurfaceElement>();
        private IDictionary<string, SurfaceElement> _loadedElements = new Dictionary<string, SurfaceElement>();

        private const int SCREEN_WIDTH = 1080;
        private const int SCREEN_HEIGHT = 800;

        public BaseGame()
        {
            InitSdl();
            InitImageLoading();
        }

        private void InitSdl()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0) throw new Exception($"sdl could not initialise! {SDL.SDL_GetError()}");
        }
        private void InitImageLoading()
        {
            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG) throw new Exception($"sdl image could not initialise! {SDL_image.IMG_GetError()}");
        }

        public void Run()
        {
            _window = new Window("Lolz Game", SCREEN_WIDTH, SCREEN_HEIGHT);

            LoadContent();

            _activeElements.Add(_loadedElements["default"]);
            while (!_quitRequested)
            {
                HandleEvents();
                DrawContent();
                SDL.SDL_UpdateWindowSurface(_window.WindowPointer);
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

        private void LoadContent()
        {
            _loadedElements = new Dictionary<string, SurfaceElement>()
            {
                { "default",  new Image(@".\Assets\pngs\press.png", _window.WindowPointer, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT) },
                { "up", new Image(@".\Assets\pngs\up.png", _window.WindowPointer, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT) },
                { "down",  new Image(@".\Assets\pngs\down.png", _window.WindowPointer, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT) },
                { "left",  new Image(@".\Assets\pngs\left.png", _window.WindowPointer, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT) },
                { "right",  new Image(@".\Assets\pngs\right.png", _window.WindowPointer, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT) }
            };
        }

        private void DrawContent()
        {
            foreach (var drawElement in _activeElements)
            {
                drawElement.Draw(_window.WindowSurfacePointer);
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
            _window.Dispose();
            SDL.SDL_Quit();
        }
    }
}