using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Graphics;
using SharpStone.Gui.Controls;
using SharpStone.Maths;
using System.Numerics;

namespace SharpStone.Gui;

public interface IColorPalette
{
    Color Color50 { get; }
    Color Color100 { get; }
    Color Color200 { get; }
    Color Color300 { get; }
    Color Color400 { get; }
    Color Color500 { get; }
    Color Color600 { get; }
    Color Color700 { get; }
    Color Color800 { get; }
    Color Color900 { get; }

}

public class SourAppleCandy : IColorPalette
{
    public Color Color50 => Color.White;
    public Color Color100 => Color.FromHEX("#F8FFF2");
    public Color Color200 => Color.FromHEX("#DEFFBD");
    public Color Color300 => Color.FromHEX("#CAFD86");
    public Color Color400 => Color.FromHEX("#B9F950");
    public Color Color500 => Color.FromHEX("#ACF01A");
    public Color Color600 => Color.FromHEX("#8FBD09"); 
    public Color Color700 => Color.FromHEX("#708B02");
    public Color Color800 => Color.FromHEX("#4C5900");
    public Color Color900 => Color.FromHEX("#232600");
}
public class IceClimber : IColorPalette
{
    public Color Color50 => Color.White;
    public Color Color100 => Color.FromHEX("#F2FEFF");
    public Color Color200 => Color.FromHEX("#BEFFFE");
    public Color Color300 => Color.FromHEX("#8AFDF5");
    public Color Color400 => Color.FromHEX("#55F7E3");
    public Color Color500 => Color.FromHEX("#21EDC8");
    public Color Color600 => Color.FromHEX("#0BBB91");
    public Color Color700 => Color.FromHEX("#028A61");
    public Color Color800 => Color.FromHEX("#005839");
    public Color Color900 => Color.FromHEX("#002616");
}
public class GrayScale : IColorPalette
{
    public Color Color50 => Color.White;
    public Color Color100 => Color.FromHEX("#FBFCFA");
    public Color Color200 => Color.FromHEX("#EAEDE7");
    public Color Color300 => Color.FromHEX("#DADED5");
    public Color Color400 => Color.FromHEX("#CACFC3");
    public Color Color500 => Color.FromHEX("#BBBFB2");
    public Color Color600 => Color.FromHEX("#96998D");
    public Color Color700 => Color.FromHEX("#717369");
    public Color Color800 => Color.FromHEX("#4C4D45");
    public Color Color900 => Color.FromHEX("#262622");
}

public enum ElevationLevel
{
    Level0 = 0,
    Level1 = 5,
    Level2 = 7,
    Level3 = 8,
    Level4 = 9,
    Level6 = 11,
    Level8 = 12,
    Level12 = 14,
    Level16 = 15,
    Level24 = 16,
}

public class Theme(
    IColorPalette primary,
    IColorPalette primaryVariant, 
    IColorPalette secondary, 
    
    Color background,
    Color surface,
    
    Color error,
    
    Color onPrimary,
    Color onSecondary,
    Color onBackground,
    Color onSurface,
    Color onError)
{
    public Color Primary { get; } = primary.Color200;
    public Color PrimaryVariant { get; } = primaryVariant.Color700;
    public Color Secondary { get; } = secondary.Color200;
    public Color SecondaryVariant { get; } = secondary.Color200;
    public Color Background { get; } = background;
    public Color Surface { get; } = surface;

    public Color Error { get; } = error;
    public Color OnPrimary { get; } = onPrimary;
    public Color OnSecondary { get; } = onSecondary;
    public Color OnBackground { get; } = onBackground;
    public Color OnSurface { get; } = onSurface;
    public Color OnError { get; } = onError;
    
    public static Theme Default =
        new(
            new SourAppleCandy(), 
            new IceClimber(), 
            new GrayScale(),
            
            Color.FromHEX("#121212"),
            Color.FromHEX("#121212"),

            Color.FromHEX("#CF6679"),

            Color.FromHEX("#00000"),
            Color.FromHEX("#00000"),
            Color.FromHEX("#FFFFFF"),
            Color.FromHEX("#FFFFFF"),
            Color.FromHEX("#000000"));
}


public class UserInterface
{
    private static Texture2D _texture;
    private static uint _uniqueId = 0;
    public static bool Visible { get; set; }
    public static Vector4 Margin { get; set; } = new Vector4(10);
    public static OrthographicCamera Camera { get; private set; }
    public static ControlContainer Main { get; private set; }
    public static Theme Theme { get; set; } = Theme.Default;

    public static float Width { get; private set; }
    public static float Height { get; private set; }

    public static Matrix4x4 QuadTranslation => Matrix4x4.CreateTranslation(0.5f, 0.5f, 1);
    internal static bool Init()
    {
        Width = Window.Width;
        Height = Window.Height;
        //Main = new ControlContainer(Margin.X, Margin.Y, Window.Width - Margin.X - Margin.Z, Window.Height - Margin.Y - Margin.W);
        _texture = Texture2D.Create("SharpStone");

        Main = new ControlContainer(10, 10, Width - 20, Height - 20);
        Camera = new OrthographicCamera(Width, Height);
        Camera.ProjectionView = Tranforms.TopLeftCorner;
        Camera.RecalculateViewMatix();
        Visible = true;

        Main.OnResize();

        return true;
    }

    public static void DrawQuad(Vector2 position, Vector2 size, Color color)
    {
        Renderer.DrawQuad(position, size, _texture, 1, color);
    }

    public static void Add(BaseControl control)
    {
        Main.Controls.Add(control);
    }

    public static void Clear()
    {
        Main = new ControlContainer(10, 10, Width - 20, Height -20);
        Main.OnResize();
    }

    public static void Update()
    {
        if (!Visible) return;
        
        Main.Update();

        RenderCommand.SetViewPort(0,0, Window.Width, Window.Height);
        Renderer.BeginScene(Camera);
        Main.Draw();
        Renderer.EndScene();
    }

    public static bool Shutdown()
    {
        return true;
    }

    public static void OnEvent(Event e)
    {
        if (!Visible) return;
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowResizedEvent>(OnWindowResized);
    }

    private static bool OnWindowResized(WindowResizedEvent @event)
    {
        Main.Size = new Vector2(@event.Width, @event.Height);
        Main.OnResize();
        return false;
    }

    public static uint GetUniqueId() => _uniqueId++;
}
