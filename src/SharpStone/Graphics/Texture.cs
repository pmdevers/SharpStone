namespace SharpStone.Graphics;

public enum ImageFormat
{
    None = 0,
    R8,
    RGB8,
    RGBA8,
    RGBA32F,
}

public struct TextureSpecification()
{
    public int Width = 1;
    public int Height = 1;
    public ImageFormat Formats = ImageFormat.RGBA8;
    bool GenerateMips = true;
}

public record Texture
{
    public int Width { get; }
    public int Height { get; }
    public bool Isloaded { get;}
    public TextureSpecification Specification { get; }

    public Texture(TextureSpecification specs)
    {
        Specification = specs;
    }

    public void Bind(int slot)
    { 
    }

    public void SetData<T>(T[] data)
    {

    }
    
}
