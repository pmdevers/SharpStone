namespace SharpStone.Maths;

public struct Color
{
#pragma warning disable IDE1006 // Naming Styles
    private const float _byteMax = byte.MaxValue;

    public float R, G, B, A;
#pragma warning restore IDE1006 // Naming Styles
    public Color(float r, float g, float b, float a = 1.0f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(uint rgba)
    {
        A = (rgba & 0xff) / _byteMax;
        B = ((rgba >> 8) & 0xff) / _byteMax;
        G = ((rgba >> 16) & 0xff) / _byteMax;
        R = ((rgba >> 24) & 0xff) / _byteMax;
    }

    public Color(byte r, byte g, byte b, byte a = byte.MaxValue)
    {
        R = r / _byteMax;
        G = g / _byteMax;
        B = b / _byteMax;
        A = a / _byteMax;
    }

    public static Color FromRGBA(uint rgba) => new(rgba);
    public static Color FromRGB(uint rgb)
    {
        var rgba = rgb << 8 | 0xff;
        return new(rgba);
    }
    public static Color FromRGBA(int red, int green, int blue,int alpha = 255)
        => new(red / 255.0f, green / 255.0f, blue / 255.0f, alpha / 255.0f);

    public static Color FromHEX(string hex)
        => FromRGB(Convert.ToUInt32(hex.TrimStart('#'), 16));

    public static Color White => new(1f, 1f, 1f);
    public static Color Red => new(1f, 0f, 0f);
    public static Color Green => new(0f, 1f, 0f);
    public static Color Blue => new(0f, 0f, 1f);
    public static Color Magenta => new(1f, 0f, 1f);
    public static Color Yellow => new(1f, 1f, 0f);
    public static Color Black => new(0f, 0f, 0f);
    public static Color Transparent => new(0f, 0f, 0f, 0f);
    public static Color CornflowerBlue => FromRGBA(100, 149, 237);
}
