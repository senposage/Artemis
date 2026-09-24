using Artemis.Core;
using HPPH;
using HPPH.SkiaSharp;
using RGB.NET.Presets.Extensions;
using SkiaSharp;
using Xunit;

namespace Artemis.Core.Tests;

public class SKTextureReadbackTests
{
    [Theory]
    [InlineData(0, 0, 5, 3)]
    [InlineData(1, 1, 3, 1)]
    [InlineData(1, 1, 1, 1)]
    [InlineData(2, 1, 1, 1)]
    public void ReadbackAveragesMatchPreviousImageConversion(int x, int y, int width, int height)
    {
        using var surface = SKSurface.Create(new SKImageInfo(5, 3));
        using var bitmap = new SKBitmap(new SKImageInfo(5, 3, SKColorType.Bgra8888, SKAlphaType.Unpremul));

        surface.Canvas.Clear(SKColors.Transparent);
        using (var paint = new SKPaint { Color = new SKColor(240, 20, 40, 128) })
            surface.Canvas.DrawRect(SKRect.Create(1, 1, 1, 1), paint);
        using (var paint = new SKPaint { Color = new SKColor(20, 160, 230) })
            surface.Canvas.DrawRect(SKRect.Create(2, 1, 1, 1), paint);
        surface.Canvas.Flush();

        Assert.True(surface.ReadPixels(bitmap.Info, bitmap.GetPixels(), bitmap.RowBytes, 0, 0));
        using SKImage snapshot = surface.Snapshot();
        IImage previousImage = snapshot.ToImage();
        var expected = previousImage[x, y, width, height].Average().ToColor();

        Assert.Equal(expected, SKTexture.AveragePixels(bitmap, SKRectI.Create(x, y, width, height)));
    }
}
