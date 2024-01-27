using StbiSharp;

namespace SharpStone.Core;

public record struct TextureSource(int width, int height, byte[] data) : IResource<TextureSource>
{
    public static string Directory => "Textures";

    public static string Extension => "png";

    public int Width { get; } = width;
    public int Height { get; } = height;
    public byte[] Data { get; } = data;

    public static TextureSource FromStream(Stream stream)
    {
        var ms = new MemoryStream();
        stream.CopyTo(ms);

        Stbi.SetFlipVerticallyOnLoad(true);

        var data = Stbi.LoadFromMemory(ms, 0);      

        return new TextureSource(data.Width, data.Height, data.Data.ToArray()); ;
    }



}
