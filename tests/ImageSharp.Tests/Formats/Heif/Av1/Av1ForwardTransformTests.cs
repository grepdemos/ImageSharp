// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.CompilerServices;
using SixLabors.ImageSharp.Formats.Heif.Av1.Transform;
using SixLabors.ImageSharp.Formats.Heif.Av1.Transform.Forward;

namespace SixLabors.ImageSharp.Tests.Formats.Heif.Av1;

/// <summary>
/// SVY: test/FwdTxfm2dTest.cc
/// </summary>
[Trait("Format", "Avif")]
public class Av1ForwardTransformTests
{
    private static readonly double[] MaximumAllowedError =
        [
            3,    // 4x4 transform
            5,    // 8x8 transform
            11,   // 16x16 transform
            70,   // 32x32 transform
            64,   // 64x64 transform
            3.9,  // 4x8 transform
            4.3,  // 8x4 transform
            12,   // 8x16 transform
            12,   // 16x8 transform
            32,   // 16x32 transform
            46,   // 32x16 transform
            136,  // 32x64 transform
            136,  // 64x32 transform
            5,    // 4x16 transform
            6,    // 16x4 transform
            21,   // 8x32 transform
            13,   // 32x8 transform
            30,   // 16x64 transform
            36,   // 64x16 transform
        ];

    [Theory]
    [MemberData(nameof(GetSizes))]
    public void AccuracyDct1dTest(int txSize)
    {
        Random rnd = new(0);
        const int testBlockCount = 1; // Originally set to: 1000
        Av1TransformSize transformSize = (Av1TransformSize)txSize;
        Av1Transform2dFlipConfiguration config = new(Av1TransformType.DctDct, transformSize);
        int width = config.TransformSize.GetWidth();

        short[] inputOfTest = new short[width];
        double[] inputReference = new double[width];
        int[] outputOfTest = new int[width];
        double[] outputReference = new double[width];
        for (int ti = 0; ti < testBlockCount; ++ti)
        {
            // prepare random test data
            for (int ni = 0; ni < width; ++ni)
            {
                inputOfTest[ni] = (short)rnd.Next((1 << 10) - 1);
                inputReference[ni] = inputOfTest[ni];
                outputReference[ni] = 0;
                outputOfTest[ni] = 255;
            }

            // calculate in forward transform functions
            new Av1DctDct4Forward2dTransformer().Transform(
                ref inputOfTest[0],
                ref outputOfTest[0],
                config.CosBitColumn,
                config.StageNumberColumn);

            // calculate in reference forward transform functions
            Av1ReferenceTransform.ReferenceDct1d(inputReference, outputReference, width);

            // Assert
            Assert.True(CompareWithError(outputReference, outputOfTest, 1));
        }
    }

    [Theory]
    [MemberData(nameof(GetCombinations))]
    public void Accuracy2dTest(int txSize, int txType, int maxAllowedError = 0)
    {
        const int bitDepth = 8;
        Random rnd = new(0);
        const int testBlockCount = 1; // Originally set to: 1000
        Av1TransformSize transformSize = (Av1TransformSize)txSize;
        Av1TransformType transformType = (Av1TransformType)txType;
        Av1Transform2dFlipConfiguration config = new(transformType, transformSize);
        int width = config.TransformSize.GetWidth();
        int height = config.TransformSize.GetHeight();
        int blockSize = width * height;
        double scaleFactor = Av1ReferenceTransform.GetScaleFactor(config, width, height);

        short[] inputOfTest = new short[blockSize];
        double[] inputReference = new double[blockSize];
        int[] outputOfTest = new int[blockSize];
        double[] outputReference = new double[blockSize];
        for (int ti = 0; ti < testBlockCount; ++ti)
        {
            // prepare random test data
            for (int ni = 0; ni < blockSize; ++ni)
            {
                inputOfTest[ni] = (short)rnd.Next((1 << 10) - 1);
                inputReference[ni] = inputOfTest[ni];
                outputReference[ni] = 0;
                outputOfTest[ni] = 255;
            }

            // calculate in forward transform functions
            Av1ForwardTransformer.Transform2d(
                inputOfTest,
                outputOfTest,
                (uint)transformSize.GetWidth(),
                transformType,
                transformSize,
                bitDepth);

            // calculate in reference forward transform functions
            Av1ReferenceTransform.ReferenceTransformFunction2d(inputReference, outputReference, transformType, transformSize, scaleFactor);

            // repack the coefficents for some tx_size
            RepackCoefficients(outputOfTest, outputReference, width, height);

            Assert.True(CompareWithError(outputReference, outputOfTest, maxAllowedError * scaleFactor), $"Forward transform 2d test with transform type: {transformType}, transform size: {transformSize} and loop: {ti}");
        }
    }

    // The max txb_width or txb_height is 32, as specified in spec 7.12.3.
    // Clear the high frequency coefficents and repack it in linear layout.
    private static void RepackCoefficients(Span<int> outputOfTest, Span<double> outputReference, int tx_width, int tx_height)
    {
        for (int i = 0; i < 2; ++i)
        {
            uint e_size = i == 0 ? (uint)sizeof(int) : sizeof(double);
            ref byte output = ref (i == 0) ? ref Unsafe.As<int, byte>(ref outputOfTest[0])
                                  : ref Unsafe.As<double, byte>(ref outputReference[0]);

            if (tx_width == 64 && tx_height == 64)
            {
                // tx_size == TX_64X64
                // zero out top-right 32x32 area.
                for (uint row = 0; row < 32; ++row)
                {
                    Unsafe.InitBlock(ref Unsafe.Add(ref output, ((row * 64) + 32) * e_size), 0, 32 * e_size);
                }

                // zero out the bottom 64x32 area.
                Unsafe.InitBlock(ref Unsafe.Add(ref output, 32 * 64 * e_size), 0, 32 * 64 * e_size);

                // Re-pack non-zero coeffs in the first 32x32 indices.
                for (uint row = 1; row < 32; ++row)
                {
                    Unsafe.CopyBlock(
                        ref Unsafe.Add(ref output, row * 32 * e_size),
                        ref Unsafe.Add(ref output, row * 64 * e_size),
                        32 * e_size);
                }
            }
            else if (tx_width == 32 && tx_height == 64)
            {
                // tx_size == TX_32X64
                // zero out the bottom 32x32 area.
                Unsafe.InitBlock(ref Unsafe.Add(ref output, 32 * 32 * e_size), 0, 32 * 32 * e_size);

                // Note: no repacking needed here.
            }
            else if (tx_width == 64 && tx_height == 32)
            {
                // tx_size == TX_64X32
                // zero out right 32x32 area.
                for (uint row = 0; row < 32; ++row)
                {
                    Unsafe.InitBlock(ref Unsafe.Add(ref output, ((row * 64) + 32) * e_size), 0, 32 * e_size);
                }

                // Re-pack non-zero coeffs in the first 32x32 indices.
                for (uint row = 1; row < 32; ++row)
                {
                    Unsafe.CopyBlock(
                        ref Unsafe.Add(ref output, row * 32 * e_size),
                        ref Unsafe.Add(ref output, row * 64 * e_size),
                        32 * e_size);
                }
            }
            else if (tx_width == 16 && tx_height == 64)
            {
                // tx_size == TX_16X64
                // zero out the bottom 16x32 area.
                Unsafe.InitBlock(ref Unsafe.Add(ref output, 16 * 32 * e_size), 0, 16 * 32 * e_size);

                // Note: no repacking needed here.
            }
            else if (tx_width == 64 &&
                       tx_height == 16)
            {
                // tx_size == TX_64X16
                // zero out right 32x16 area.
                for (uint row = 0; row < 16; ++row)
                {
                    Unsafe.InitBlock(ref Unsafe.Add(ref output, ((row * 64) + 32) * e_size), 0, 32 * e_size);
                }

                // Re-pack non-zero coeffs in the first 32x16 indices.
                for (uint row = 1; row < 16; ++row)
                {
                    Unsafe.CopyBlock(
                        ref Unsafe.Add(ref output, row * 32 * e_size),
                        ref Unsafe.Add(ref output, row * 64 * e_size),
                        32 * e_size);
                }
            }
        }
    }

    private static bool CompareWithError(Span<double> expected, Span<int> actual, double allowedError)
    {
        // compare for the result is witghin accuracy
        double maximumErrorInTest = 0;
        for (int ni = 0; ni < expected.Length; ++ni)
        {
            maximumErrorInTest = Math.Max(maximumErrorInTest, Math.Abs(actual[ni] - Math.Round(expected[ni])));
        }

        return maximumErrorInTest <= allowedError;
    }

    public static TheoryData<int> GetSizes()
    {
        TheoryData<int> sizes = [];

        // For now test only 4x4.
        sizes.Add(0);
        return sizes;
    }

    public static TheoryData<int, int, int> GetCombinations()
    {
        TheoryData<int, int, int> combinations = [];
        for (int s = 0; s < (int)Av1TransformSize.AllSizes; s++)
        {
            double maxError = MaximumAllowedError[s];
            for (int t = 0; t < (int)Av1TransformType.AllTransformTypes; t++)
            {
                Av1TransformType transformType = (Av1TransformType)t;
                Av1TransformSize transformSize = (Av1TransformSize)s;
                Av1Transform2dFlipConfiguration config = new(transformType, transformSize);
                if (config.IsAllowed())
                {
                    combinations.Add(s, t, (int)maxError);
                }

                // For now only DCT.
                break;
            }

            // For now only 4x4.
            break;
        }

        return combinations;
    }
}