using SharpStone.Platform.OpenGL;
using StbImageWriteSharp;
using StbTrueTypeSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using static StbTrueTypeSharp.StbTrueType;

namespace SharpStone.Tests;
internal class FontTests
{
    [Test]
    public unsafe void DoTest()
    {
        byte[] font = File.ReadAllBytes("./Assets/Fonts/Orion.ttf");
        byte[] bitmap;
        stbtt_pack_context context;
        Dictionary<int, GlyphInfo> glyphs = [];
        int bitmapWidth, bitmapHeight;
        int fontPixelHeight = 32;

        bitmapWidth = 256;
        bitmapHeight = 256;
        bitmap = new byte[bitmapWidth * bitmapHeight];
        context = new stbtt_pack_context();

        fixed (byte* pixelsPtr = bitmap)
        {
            stbtt_PackBegin(context, pixelsPtr, bitmapWidth, bitmapHeight, bitmapWidth, 1, null);
        }

        
        var fontInfo = CreateFont(font, 0);
        var scaleFactor = stbtt_ScaleForPixelHeight(fontInfo, fontPixelHeight);

        int ascent, descent, linegap;
        stbtt_GetFontVMetrics(fontInfo, &ascent, &descent, &linegap);

        IEnumerable<CharacterRange> characterRanges = [CharacterRange.BasicLatin, CharacterRange.Katakana];

        foreach (var range in characterRanges)
        {
            var cd = new stbtt_packedchar[range.End - range.Start + 1];
            fixed(stbtt_packedchar* cdPtr = cd)
            {
                stbtt_PackFontRange(context, fontInfo.data, 0, fontPixelHeight,
                    range.Start,
                    range.End - range.Start + 1,
                    cdPtr);
            }

            for(int i = 0; i < cd.Length; ++i)
            {
                var yOff = cd[i].yoff;
                yOff += ascent * scaleFactor;

                var glyphInfo = new GlyphInfo
                {
                    X = cd[i].x0,
                    Y = cd[i].y0,
                    Width = cd[i].x1 - cd[i].x0,
                    Height = cd[i].y1 - cd[i].y0,
                    XOffset = (int)cd[i].xoff,
                    YOffset = (int)Math.Round(yOff),
                    XAdvance = (int)Math.Round(cd[i].xadvance)
                };

                glyphs[i + range.Start] = glyphInfo;
            }
        }

        var imageWriter = new ImageWriter();
        using (var stream = File.OpenWrite("output.png"))
        {
            imageWriter.WritePng(bitmap, bitmapWidth, bitmapHeight, ColorComponents.Grey, stream);
        }
    }

    public struct GlyphInfo
    {
        public int X, Y, Width, Height;
        public int XOffset, YOffset;
        public int XAdvance;
    }

    public struct CharacterRange
    {
        public static readonly CharacterRange BasicLatin = new CharacterRange(0x0020, 0x007F);
        public static readonly CharacterRange Latin1Supplement = new CharacterRange(0x00A0, 0x00FF);
        public static readonly CharacterRange LatinExtendedA = new CharacterRange(0x0100, 0x017F);
        public static readonly CharacterRange LatinExtendedB = new CharacterRange(0x0180, 0x024F);
        public static readonly CharacterRange Cyrillic = new CharacterRange(0x0400, 0x04FF);
        public static readonly CharacterRange CyrillicSupplement = new CharacterRange(0x0500, 0x052F);
        public static readonly CharacterRange Hiragana = new CharacterRange(0x3040, 0x309F);
        public static readonly CharacterRange Katakana = new CharacterRange(0x30A0, 0x30FF);
        public static readonly CharacterRange Greek = new CharacterRange(0x0370, 0x03FF);
        public static readonly CharacterRange CjkSymbolsAndPunctuation = new CharacterRange(0x3000, 0x303F);
        public static readonly CharacterRange CjkUnifiedIdeographs = new CharacterRange(0x4e00, 0x9fff);
        public static readonly CharacterRange HangulCompatibilityJamo = new CharacterRange(0x3130, 0x318f);
        public static readonly CharacterRange HangulSyllables = new CharacterRange(0xac00, 0xd7af);

        public int Start { get; }

        public int End { get; }

        public int Size => End - Start + 1;

        public CharacterRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        public CharacterRange(int single) : this(single, single)
        {
        }
    }
}


