// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Heif.Av1;

internal static class Av1LookupTables
{
    // The Subsampled_Size table in the spec (Section 5.11.38. Get plane residual size function).
    public static Av1BlockSize[,,] SubSampledSize =
    {
        { { Av1BlockSize.Block4x4,      Av1BlockSize.Block4x4 },    { Av1BlockSize.Block4x4,    Av1BlockSize.Block4x4 } },
        { { Av1BlockSize.Block4x8,      Av1BlockSize.Block4x4 },    { Av1BlockSize.Invalid,     Av1BlockSize.Block4x4 } },
        { { Av1BlockSize.Block8x4,      Av1BlockSize.Invalid },     { Av1BlockSize.Block4x4,    Av1BlockSize.Block4x4 } },
        { { Av1BlockSize.Block8x8,      Av1BlockSize.Block8x4 },    { Av1BlockSize.Block4x8,    Av1BlockSize.Block4x4 } },
        { { Av1BlockSize.Block8x16,     Av1BlockSize.Block8x8 },    { Av1BlockSize.Invalid,     Av1BlockSize.Block4x8 } },
        { { Av1BlockSize.Block16x8,     Av1BlockSize.Invalid },     { Av1BlockSize.Block8x8,    Av1BlockSize.Block8x4 } },
        { { Av1BlockSize.Block16x16,    Av1BlockSize.Block16x8 },   { Av1BlockSize.Block8x16,   Av1BlockSize.Block8x8 } },
        { { Av1BlockSize.Block16x32,    Av1BlockSize.Block16x16 },  { Av1BlockSize.Invalid,     Av1BlockSize.Block8x16 } },
        { { Av1BlockSize.Block32x16,    Av1BlockSize.Invalid },     { Av1BlockSize.Block16x16,  Av1BlockSize.Block16x8 } },
        { { Av1BlockSize.Block32x32,    Av1BlockSize.Block32x16 },  { Av1BlockSize.Block16x32,  Av1BlockSize.Block16x16 } },
        { { Av1BlockSize.Block32x64,    Av1BlockSize.Block32x32 },  { Av1BlockSize.Invalid,     Av1BlockSize.Block16x32 } },
        { { Av1BlockSize.Block64x32,    Av1BlockSize.Invalid },     { Av1BlockSize.Block32x32,  Av1BlockSize.Block32x16 } },
        { { Av1BlockSize.Block64x64,    Av1BlockSize.Block64x32 },  { Av1BlockSize.Block32x64,  Av1BlockSize.Block32x32 } },
        { { Av1BlockSize.Block64x128,   Av1BlockSize.Block64x64 },  { Av1BlockSize.Invalid,     Av1BlockSize.Block32x64 } },
        { { Av1BlockSize.Block128x64,   Av1BlockSize.Invalid },     { Av1BlockSize.Block64x64,  Av1BlockSize.Block64x32 } },
        { { Av1BlockSize.Block128x128,  Av1BlockSize.Block128x64 }, { Av1BlockSize.Block64x128, Av1BlockSize.Block64x64 } },
        { { Av1BlockSize.Block4x16,     Av1BlockSize.Block4x8 },    { Av1BlockSize.Invalid,     Av1BlockSize.Block4x8 } },
        { { Av1BlockSize.Block16x4,     Av1BlockSize.Invalid },     { Av1BlockSize.Block8x4,    Av1BlockSize.Block8x4 } },
        { { Av1BlockSize.Block8x32,     Av1BlockSize.Block8x16 },   { Av1BlockSize.Invalid,     Av1BlockSize.Block4x16 } },
        { { Av1BlockSize.Block32x8,     Av1BlockSize.Invalid },     { Av1BlockSize.Block16x8,   Av1BlockSize.Block16x4 } },
        { { Av1BlockSize.Block16x64,    Av1BlockSize.Block16x32 },  { Av1BlockSize.Invalid,     Av1BlockSize.Block8x32 } },
        { { Av1BlockSize.Block64x16,    Av1BlockSize.Invalid },     { Av1BlockSize.Block32x16,  Av1BlockSize.Block32x8 } },
    };
}
