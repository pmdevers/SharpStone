using SharpStone.Core;
using SharpStone.Platform.OpenGL;
using static SharpStone.Platform.OpenGL.GL;

namespace SharpStone.Graphics;
public unsafe class Texture2D : IDisposable
{
    private uint _id;

    public int Width { get; set; }
    public int Height { get; set; }
    public bool Isloaded { get;set; }

    public byte[] Data { get; set; }

    public static Texture2D Create(string name)
    {
        var source = ResourceManager.GetResource<TextureSource>(name);
        return new Texture2D(source);
    }

    public static Texture2D Create(TextureSpecification specs)
    {
        return new Texture2D(specs);
    }

    private Texture2D(TextureSpecification specs)
    {
        _id = glCreateTexture();
        Width = specs.Width;
        Height = specs.Height;

        glTextureStorage2D(_id, 1, InternalFormat.Rgba8, Width, Height);

    }
    private Texture2D(TextureSource source)
    {
        _id = glCreateTexture();
        
        Width = source.Width;
        Height = source.Height;
        Data = source.Data;
        glTextureStorage2D(_id, 1, InternalFormat.Rgba8, source.Width, source.Width);

        //glTextureParameteri(_id, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        //glTextureParameteri(_id, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.LinearDetailSgis);

        //glTextureParameteri(_id, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        //glTextureParameteri(_id, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        SetData(source.data);
    }

    public void Bind(uint slot)
    {
        glBindTextureUnit(slot, _id);
    }

    public void Unbind()
    {

    }

    public void SetData<T>(T[] data)
    {
        fixed (void* ptrData = data)
        {
            glTextureSubImage2D(_id, 0, 0, 0,
                Width,
                Height,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                ptrData);
        }
    }

    public void Dispose()
    {
        if(_id > 0)
        {
            fixed (uint* ptrId = &_id)
            { 
                glDeleteTextures(1, ptrId);
            }
            _id = 0;
        }
    }
}