// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.IO;
using System.Threading;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.PixelFormats;
using SDBitmap = System.Drawing.Bitmap;
using SDImage = System.Drawing.Image;

namespace SixLabors.ImageSharp.Tests.TestUtilities.ReferenceCodecs
{
    public class SystemDrawingReferenceDecoder : ImageDecoder<SystemDrawingReferenceDecoderOptions>
    {
        public static SystemDrawingReferenceDecoder Instance { get; } = new SystemDrawingReferenceDecoder();

        public override IImageInfo IdentifySpecialized(SystemDrawingReferenceDecoderOptions options, Stream stream, CancellationToken cancellationToken)
        {
            using var sourceBitmap = new SDBitmap(stream);
            PixelTypeInfo pixelType = new(SDImage.GetPixelFormatSize(sourceBitmap.PixelFormat));
            return new ImageInfo(pixelType, sourceBitmap.Width, sourceBitmap.Height, new ImageMetadata());
        }

        public override Image<TPixel> DecodeSpecialized<TPixel>(SystemDrawingReferenceDecoderOptions options, Stream stream, CancellationToken cancellationToken)
        {
            using var sourceBitmap = new SDBitmap(stream);
            if (sourceBitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                return SystemDrawingBridge.From32bppArgbSystemDrawingBitmap<TPixel>(sourceBitmap);
            }

            using var convertedBitmap = new SDBitmap(
                sourceBitmap.Width,
                sourceBitmap.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var g = System.Drawing.Graphics.FromImage(convertedBitmap))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                g.DrawImage(sourceBitmap, 0, 0, sourceBitmap.Width, sourceBitmap.Height);
            }

            return SystemDrawingBridge.From32bppArgbSystemDrawingBitmap<TPixel>(convertedBitmap);
        }

        public override Image DecodeSpecialized(SystemDrawingReferenceDecoderOptions options, Stream stream, CancellationToken cancellationToken)
            => this.DecodeSpecialized<Rgba32>(options, stream, cancellationToken);
    }

    public class SystemDrawingReferenceDecoderOptions : ISpecializedDecoderOptions
    {
        public DecoderOptions GeneralOptions { get; set; } = new();
    }
}
