// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp.Formats.Heif.Av1.Transform.Forward;

internal class Av1Identity4Forward1dTransformer : IAv1Forward1dTransformer
{
    public void Transform(ref int input, ref int output, int cosBit, Span<byte> stageRange)
        => TransformScalar(ref input, ref output);

    private static void TransformScalar(ref int input, ref int output)
    {
        output = Av1Math.RoundShift((long)input * Av1Forward2dTransformerBase.NewSqrt2, Av1Forward2dTransformerBase.NewSqrt2BitCount);
        Unsafe.Add(ref output, 1) = Av1Math.RoundShift((long)Unsafe.Add(ref input, 1) * Av1Forward2dTransformerBase.NewSqrt2, Av1Forward2dTransformerBase.NewSqrt2BitCount);
        Unsafe.Add(ref output, 2) = Av1Math.RoundShift((long)Unsafe.Add(ref input, 2) * Av1Forward2dTransformerBase.NewSqrt2, Av1Forward2dTransformerBase.NewSqrt2BitCount);
        Unsafe.Add(ref output, 3) = Av1Math.RoundShift((long)Unsafe.Add(ref input, 3) * Av1Forward2dTransformerBase.NewSqrt2, Av1Forward2dTransformerBase.NewSqrt2BitCount);
    }
}
