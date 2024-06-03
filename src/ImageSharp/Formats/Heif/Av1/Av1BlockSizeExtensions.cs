// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Formats.Heif.Av1.Quantization;

namespace SixLabors.ImageSharp.Formats.Heif.Av1;

internal static class Av1BlockSizeExtensions
{
    private static readonly int[] SizeWide = [1, 1, 2, 2, 2, 4, 4, 4, 8, 8, 8, 16, 16, 16, 32, 32, 1, 4, 2, 8, 4, 16];
    private static readonly int[] SizeHigh = [1, 2, 1, 2, 4, 2, 4, 8, 4, 8, 16, 8, 16, 32, 16, 32, 4, 1, 8, 2, 16, 4];

    private static readonly Av1BlockSize[][][] SubSampled =
        [

            // ss_x == 0    ss_x == 0        ss_x == 1      ss_x == 1
            // ss_y == 0    ss_y == 1        ss_y == 0      ss_y == 1
            [[Av1BlockSize.Block4x4, Av1BlockSize.Block4x4], [Av1BlockSize.Block4x4, Av1BlockSize.Block4x4]],
            [[Av1BlockSize.Block4x8, Av1BlockSize.Block4x4], [Av1BlockSize.Invalid, Av1BlockSize.Block4x4]],
            [[Av1BlockSize.Block8x4, Av1BlockSize.Invalid], [Av1BlockSize.Block4x4, Av1BlockSize.Block4x4]],
            [[Av1BlockSize.Block8x8, Av1BlockSize.Block8x4], [Av1BlockSize.Block4x8, Av1BlockSize.Block4x4]],
            [[Av1BlockSize.Block8x16, Av1BlockSize.Block8x8], [Av1BlockSize.Invalid, Av1BlockSize.Block4x8]],
            [[Av1BlockSize.Block16x8, Av1BlockSize.Invalid], [Av1BlockSize.Block8x8, Av1BlockSize.Block8x4]],
            [[Av1BlockSize.Block16x16, Av1BlockSize.Block16x8], [Av1BlockSize.Block8x16, Av1BlockSize.Block8x8]],
            [[Av1BlockSize.Block16x32, Av1BlockSize.Block16x16], [Av1BlockSize.Invalid, Av1BlockSize.Block8x16]],
            [[Av1BlockSize.Block32x16, Av1BlockSize.Invalid], [Av1BlockSize.Block16x16, Av1BlockSize.Block16x8]],
            [[Av1BlockSize.Block32x32, Av1BlockSize.Block32x16], [Av1BlockSize.Block16x32, Av1BlockSize.Block16x16]],
            [[Av1BlockSize.Block32x64, Av1BlockSize.Block32x32], [Av1BlockSize.Invalid, Av1BlockSize.Block16x32]],
            [[Av1BlockSize.Block64x32, Av1BlockSize.Invalid], [Av1BlockSize.Block32x32, Av1BlockSize.Block32x16]],
            [[Av1BlockSize.Block64x64, Av1BlockSize.Block64x32], [Av1BlockSize.Block32x64, Av1BlockSize.Block32x32]],
            [[Av1BlockSize.Block64x128, Av1BlockSize.Block64x64], [Av1BlockSize.Invalid, Av1BlockSize.Block32x64]],
            [[Av1BlockSize.Block128x64, Av1BlockSize.Invalid], [Av1BlockSize.Block64x64, Av1BlockSize.Block64x32]],
            [[Av1BlockSize.Block128x128, Av1BlockSize.Block128x64], [Av1BlockSize.Block64x128, Av1BlockSize.Block64x64]],
            [[Av1BlockSize.Block4x16, Av1BlockSize.Block4x8], [Av1BlockSize.Invalid, Av1BlockSize.Block4x8]],
            [[Av1BlockSize.Block16x4, Av1BlockSize.Invalid], [Av1BlockSize.Block8x4, Av1BlockSize.Block8x4]],
            [[Av1BlockSize.Block8x32, Av1BlockSize.Block8x16], [Av1BlockSize.Invalid, Av1BlockSize.Block4x16]],
            [[Av1BlockSize.Block32x8, Av1BlockSize.Invalid], [Av1BlockSize.Block16x8, Av1BlockSize.Block16x4]],
            [[Av1BlockSize.Block16x64, Av1BlockSize.Block16x32], [Av1BlockSize.Invalid, Av1BlockSize.Block8x32]],
            [[Av1BlockSize.Block64x16, Av1BlockSize.Invalid], [Av1BlockSize.Block32x16, Av1BlockSize.Block32x8]]
        ];

    private static readonly Av1TransformSize[] MaxTransformSize = [
        Av1TransformSize.Size4x4, Av1TransformSize.Size4x8, Av1TransformSize.Size8x4, Av1TransformSize.Size8x8,
        Av1TransformSize.Size8x16, Av1TransformSize.Size16x8, Av1TransformSize.Size16x16, Av1TransformSize.Size16x32,
        Av1TransformSize.Size32x16, Av1TransformSize.Size32x32, Av1TransformSize.Size32x64, Av1TransformSize.Size64x32,
        Av1TransformSize.Size64x64, Av1TransformSize.Size64x64, Av1TransformSize.Size64x64, Av1TransformSize.Size64x64,
        Av1TransformSize.Size4x16, Av1TransformSize.Size16x4, Av1TransformSize.Size8x32, Av1TransformSize.Size32x8,
        Av1TransformSize.Size16x64, Av1TransformSize.Size64x16
    ];

    public static int Get4x4WideCount(this Av1BlockSize blockSize) => SizeWide[(int)blockSize];

    public static int Get4x4HighCount(this Av1BlockSize blockSize) => SizeHigh[(int)blockSize];

    /// <summary>
    /// Returns the width of the block in samples.
    /// </summary>
    public static int GetWidth(this Av1BlockSize blockSize)
        => Get4x4WideCount(blockSize) << 2;

    /// <summary>
    /// Returns of the height of the block in 4 samples.
    /// </summary>
    public static int GetHeight(this Av1BlockSize blockSize)
        => Get4x4HighCount(blockSize) << 2;

    /// <summary>
    /// Returns base 2 logarithm of the width of the block in units of 4 samples.
    /// </summary>
    public static int Get4x4WidthLog2(this Av1BlockSize blockSize)
        => Get4x4WideCount(blockSize) << 2;

    /// <summary>
    /// Returns base 2 logarithm of the height of the block in units of 4 samples.
    /// </summary>
    public static int Get4x4HeightLog2(this Av1BlockSize blockSize)
        => Get4x4HighCount(blockSize) << 2;

    /// <summary>
    /// Returns the block size of a sub sampled block.
    /// </summary>
    public static Av1BlockSize GetSubsampled(this Av1BlockSize blockSize, bool subX, bool subY)
    {
        if (blockSize == Av1BlockSize.Invalid)
        {
            return Av1BlockSize.Invalid;
        }

        return SubSampled[(int)blockSize][subX ? 1 : 0][subY ? 1 : 0];
    }

    /// <summary>
    /// Returns the largest transform size that can be used for blocks of given size.
    /// The can be either a square or rectangular block.
    /// </summary>
    public static Av1TransformSize GetMaximumTransformSize(this Av1BlockSize blockSize)
        => MaxTransformSize[(int)blockSize];
}