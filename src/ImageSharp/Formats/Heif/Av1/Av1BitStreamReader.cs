// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Heif.Av1;

internal ref struct Av1BitStreamReader(Span<byte> data)
{
    private const int WordSize = sizeof(byte);
    private const int DoubleWordSize = 2 * WordSize;
    private readonly Span<byte> data = data;
    private int wordPosition = 0;
    private int bitOffset = 0;

    public readonly int BitPosition => (this.wordPosition * WordSize) + this.bitOffset;

    public readonly int Length => this.data.Length;

    public void Reset()
    {
        this.wordPosition = 0;
        this.bitOffset = 0;
    }

    public void Skip(int bitCount)
    {
        this.bitOffset += bitCount;
        while (this.bitOffset >= WordSize)
        {
            this.bitOffset -= WordSize;
            this.wordPosition++;
        }
    }

    public uint ReadLiteral(int bitCount)
    {
        uint bits = (uint)(this.data[this.wordPosition] << this.bitOffset) >> (WordSize - bitCount);
        this.bitOffset += bitCount;
        while (this.bitOffset > WordSize)
        {
            uint nextWord = this.data[this.wordPosition + 1];
            bits |= nextWord << (DoubleWordSize - bitCount);
        }

        if (this.bitOffset >= WordSize)
        {
            this.bitOffset -= WordSize;
        }

        return bits;
    }

    internal bool ReadBoolean()
    {
        bool bit = (this.data[this.wordPosition] & (1 << (WordSize - this.bitOffset))) > 0;
        this.Skip(1);
        return bit;
    }

    public ulong ReadLittleEndianBytes128(out int length)
    {
        // See section 4.10.5 of the AV1-Specification
        DebugGuard.IsTrue((this.bitOffset & (WordSize - 1)) == 0, "Reading of Little Endian 128 value only allowed on byte alignment");

        ulong value = 0;
        length = 0;
        for (int i = 0; i < 56; i += 7)
        {
            uint leb128Byte = this.ReadLiteral(8);
            value |= (leb128Byte & 0x7FUL) << i;
            length++;
            if ((leb128Byte & 0x80U) != 0x80U)
            {
                break;
            }
        }

        return value;
    }

    public uint ReadUnsignedVariableLength()
    {
        // See section 4.10.3 of the AV1-Specification
        int leadingZerosCount = 0;
        while (leadingZerosCount < 32 && this.ReadLiteral(1) == 0U)
        {
            leadingZerosCount++;
        }

        if (leadingZerosCount == 32)
        {
            return uint.MaxValue;
        }

        uint basis = (1U << leadingZerosCount) - 1U;
        uint value = this.ReadLiteral(leadingZerosCount);
        return basis + value;
    }

    public uint ReadNonSymmetric(uint n)
    {
        // See section 4.10.7 of the AV1-Specification
        if (n <= 1)
        {
            return 0;
        }

        int w = (int)(Av1Math.MostSignificantBit(n) + 1);
        uint m = (uint)((1 << w) - n);
        uint v = this.ReadLiteral(w - 1);
        if (v < m)
        {
            return v;
        }

        return (v << 1) - m + this.ReadLiteral(1);
    }

    public int ReadSignedFromUnsigned(int n)
    {
        // See section 4.10.6 of the AV1-Specification
        int signedValue;
        uint value = this.ReadLiteral(n);
        uint signMask = 1U << (n - 1);
        if ((value & signMask) == signMask)
        {
            // Prevent overflow by casting to long;
            signedValue = (int)((long)value - (signMask << 1));
        }
        else
        {
            signedValue = (int)value;
        }

        return signedValue;
    }

    public uint ReadLittleEndian(int n)
    {
        // See section 4.10.4 of the AV1-Specification
        DebugGuard.IsTrue((this.bitOffset & (WordSize - 1)) == 0, "Reading of Little Endian value only allowed on byte alignment");

        uint t = 0;
        for (int i = 0; i < 8 * n; i += 8)
        {
            t += this.ReadLiteral(8) << i;
        }

        return t;
    }
}
