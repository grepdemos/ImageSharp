// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Heic;

/// <summary>
/// Compression algorithms possible inside an HEIF (High Efficiency File Format) based file.
/// </summary>
public enum HeicCompressionMethod
{
    /// <summary>
    /// High Efficiency Video Coding
    /// </summary>
    Hevc,

    /// <summary>
    /// Legact JPEG
    /// </summary>
    LegacyJpeg,

    /// <summary>
    /// JPEG 2000
    /// </summary>
    Jpeg2000,

    /// <summary>
    /// JPEG-XR
    /// </summary>
    JpegXR,

    /// <summary>
    /// JPEG-XS
    /// </summary>
    JpegXS,

    /// <summary>
    /// AOMedia's Video 1 coding
    /// </summary>
    Av1,

    /// <summary>
    /// Advanced Video Coding
    /// </summary>
    Avc,
}
