using StbImageSharp;

namespace SharpStone.Core;

public record struct TextureSource(int width, int height, byte[] data) : IResource<TextureSource>
{
    public static string Directory => "Textures";

    public static string Extension => "png";

    public int Width { get; } = width;
    public int Height { get; } = height;
    public byte[] Data { get; } = data;
    //public int Channels { get; } = channels;

    public static TextureSource FromStream(Stream stream)
    {
        var ms = new MemoryStream();
        stream.CopyTo(ms);

        var bytes = ms.ToArray();

        StbImage.stbi_set_flip_vertically_on_load(1);

        var data = ImageResult.FromMemory(bytes, ColorComponents.RedGreenBlueAlpha);

        

        return new TextureSource(data.Width, data.Height, data.Data); ;
    }



}
