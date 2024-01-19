using SharpStone.Core;
using SharpStone.Platform.OpenGL;
using SharpStone.Platform.SDL2;
using static SharpStone.Platform.OpenGL.GL;
using static SharpStone.Logging;
using static SharpStone.Platform.SDL2.SDL;
using System.Runtime.InteropServices;
using SharpStone.Events;
using static SharpStone.Application;

namespace SharpStone.Window;
internal unsafe class SDL2Window : IWindow
{
    private nint _window;
    private nint _glContext;
    private EventCallback? _eventCallback;

    public string Title { get; set; } = nameof(SDL2Window);

    public int Width { get; set; }

    public int Height { get; set; } 

    public bool Fullscreen { get; set; }
    public bool HighDpi { get; set; }

    public bool Init(WindowArgs args)
    {

#pragma warning disable CA1806 // Do not ignore method results

        SDL.Init(SDL_INIT_VIDEO);

        GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_FLAGS, (int)SDL_GLcontext.SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG);

        GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);
        GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
        GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 2);

        GL_SetAttribute(SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
        GL_SetAttribute(SDL_GLattr.SDL_GL_DEPTH_SIZE, 24);
        GL_SetAttribute(SDL_GLattr.SDL_GL_ALPHA_SIZE, 8);
        GL_SetAttribute(SDL_GLattr.SDL_GL_STENCIL_SIZE, 8);
#pragma warning restore CA1806 // Do not ignore method results

        var flags = SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
        if (Fullscreen) flags |= SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
        if (HighDpi) flags |= SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;

        _window = CreateWindow(args.Title,
            SDL_WINDOWPOS_CENTERED,
            SDL_WINDOWPOS_CENTERED,
            args.Width,
            args.Height,
            flags);

        Logger.Assert<SDL2Window>(_window != IntPtr.Zero, "Could not create window!");

        _glContext = GL_CreateContext(_window);
        Logger.Assert<SDL2Window>(_glContext != IntPtr.Zero, "Could not create context!");

        Width = args.Width;
        Height = args.Height;

        LoadGetString(GL_GetProcAddress);
        
        var version = glGetString(StringName.Version);
        var sversion = Marshal.PtrToStringAnsi((IntPtr)version);
        Logger.Info<SDL2Window>($"OpenGL Version: {sversion}.");

        LoadAllFunctions(GL_GetProcAddress);
        
        GL_SwapWindow(_window);

        return true;
    }

    public void Update()
    {
        while (SDL_PollEvent(out SDL_Event e) > 0)
        {
            switch (e.type)
            {
                case SDL_EventType.SDL_QUIT:
                    Instance.OnEvent(new WindowCloseEvent());
                    break;
                case SDL_EventType.SDL_WINDOWEVENT:
                    switch (e.window.windowEvent)
                    {
                        case SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                        case SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                            Instance.OnEvent(new WindowResizedEvent(e.window.data1, e.window.data2));
                            break;
                        default
                            : break;
                    }
                    break;
            }
        }
        GL_SwapWindow(_window);
    }

    

    public bool Shutdown()
    {
        GL_DeleteContext(_glContext);
        return true;
    }
}
