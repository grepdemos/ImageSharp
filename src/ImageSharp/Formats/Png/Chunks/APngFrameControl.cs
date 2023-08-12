// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers.Binary;

namespace SixLabors.ImageSharp.Formats.Png.Chunks;

internal readonly struct APngFrameControl
{
    public const int Size = 26;

    public APngFrameControl(
        int sequenceNumber,
        int width,
        int height,
        int xOffset,
        int yOffset,
        short delayNumber,
        short delayDenominator,
        APngDisposeOperation disposeOperation,
        APngBlendOperation blendOperation)
    {
        this.SequenceNumber = sequenceNumber;
        this.Width = width;
        this.Height = height;
        this.XOffset = xOffset;
        this.YOffset = yOffset;
        this.DelayNumber = delayNumber;
        this.DelayDenominator = delayDenominator;
        this.DisposeOperation = disposeOperation;
        this.BlendOperation = blendOperation;
    }

    /// <summary>
    /// Gets the sequence number of the animation chunk, starting from 0
    /// </summary>
    public int SequenceNumber { get; }

    /// <summary>
    /// Gets the width of the following frame
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the following frame
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the X position at which to render the following frame
    /// </summary>
    public int XOffset { get; }

    /// <summary>
    /// Gets the Y position at which to render the following frame
    /// </summary>
    public int YOffset { get; }

    /// <summary>
    /// Gets the frame delay fraction numerator
    /// </summary>
    public short DelayNumber { get; }

    /// <summary>
    /// Gets the frame delay fraction denominator
    /// </summary>
    public short DelayDenominator { get; }

    /// <summary>
    /// Gets the type of frame area disposal to be done after rendering this frame
    /// </summary>
    public APngDisposeOperation DisposeOperation { get; }

    /// <summary>
    /// Gets the type of frame area rendering for this frame
    /// </summary>
    public APngBlendOperation BlendOperation { get; }

    /// <summary>
    /// Validates the APng fcTL.
    /// </summary>
    /// <exception cref="NotSupportedException">
    /// Thrown if the image does pass validation.
    /// </exception>
    public void Validate(PngHeader hdr)
    {
        if (this.XOffset < 0)
        {
            throw new NotSupportedException($"Invalid XOffset. Expected >= 0. Was '{this.XOffset}'.");
        }

        if (this.YOffset < 0)
        {
            throw new NotSupportedException($"Invalid YOffset. Expected >= 0. Was '{this.YOffset}'.");
        }

        if (this.Width <= 0)
        {
            throw new NotSupportedException($"Invalid Width. Expected > 0. Was '{this.Width}'.");
        }

        if (this.Height <= 0)
        {
            throw new NotSupportedException($"Invalid Height. Expected > 0. Was '{this.Height}'.");
        }

        if (this.XOffset + this.Width > hdr.Width)
        {
            throw new NotSupportedException($"Invalid XOffset or Width. The sum > PngHeader.Width. Was '{this.XOffset + this.Width}'.");
        }

        if (this.YOffset + this.Height > hdr.Height)
        {
            throw new NotSupportedException($"Invalid YOffset or Height. The sum > PngHeader.Height. Was '{this.YOffset + this.Height}'.");
        }
    }

    /// <summary>
    /// Writes the fcTL to the given buffer.
    /// </summary>
    /// <param name="buffer">The buffer to write to.</param>
    public void WriteTo(Span<byte> buffer)
    {
        BinaryPrimitives.WriteInt32BigEndian(buffer[..4], this.SequenceNumber);
        BinaryPrimitives.WriteInt32BigEndian(buffer[4..8], this.Width);
        BinaryPrimitives.WriteInt32BigEndian(buffer[8..12], this.Height);
        BinaryPrimitives.WriteInt32BigEndian(buffer[12..16], this.XOffset);
        BinaryPrimitives.WriteInt32BigEndian(buffer[16..20], this.YOffset);
        BinaryPrimitives.WriteInt32BigEndian(buffer[20..22], this.DelayNumber);
        BinaryPrimitives.WriteInt32BigEndian(buffer[12..24], this.DelayDenominator);

        buffer[24] = (byte)this.DisposeOperation;
        buffer[25] = (byte)this.BlendOperation;
    }

    /// <summary>
    /// Parses the APngFrameControl from the given data buffer.
    /// </summary>
    /// <param name="data">The data to parse.</param>
    /// <returns>The parsed fcTL.</returns>
    public static APngFrameControl Parse(ReadOnlySpan<byte> data)
        => new(
            sequenceNumber: BinaryPrimitives.ReadInt32BigEndian(data[..4]),
            width: BinaryPrimitives.ReadInt32BigEndian(data[4..8]),
            height: BinaryPrimitives.ReadInt32BigEndian(data[8..12]),
            xOffset: BinaryPrimitives.ReadInt32BigEndian(data[12..16]),
            yOffset: BinaryPrimitives.ReadInt32BigEndian(data[16..20]),
            delayNumber: BinaryPrimitives.ReadInt16BigEndian(data[20..22]),
            delayDenominator: BinaryPrimitives.ReadInt16BigEndian(data[22..24]),
            disposeOperation: (APngDisposeOperation)data[24],
            blendOperation: (APngBlendOperation)data[25]);
}
