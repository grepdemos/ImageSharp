// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp.Formats.Heif.Av1.Transform.Forward;

internal class Av1Adst32Forward1dTransformer : IAv1Forward1dTransformer
{
    public void Transform(ref int input, ref int output, int cosBit, Span<byte> stageRange)
        => TransformScalar(ref input, ref output, cosBit);

    private static void TransformScalar(ref int input, ref int outputRef, int cosBit)
    {
        Span<int> temp0 = stackalloc int[32];
        Span<int> temp1 = stackalloc int[32];

        // stage 0;

        // stage 1;
        temp1[0] = Unsafe.Add(ref input, 31);
        temp1[1] = input;
        temp1[2] = Unsafe.Add(ref input, 29);
        temp1[3] = Unsafe.Add(ref input, 2);
        temp1[4] = Unsafe.Add(ref input, 27);
        temp1[5] = Unsafe.Add(ref input, 4);
        temp1[6] = Unsafe.Add(ref input, 25);
        temp1[7] = Unsafe.Add(ref input, 6);
        temp1[8] = Unsafe.Add(ref input, 23);
        temp1[9] = Unsafe.Add(ref input, 8);
        temp1[10] = Unsafe.Add(ref input, 21);
        temp1[11] = Unsafe.Add(ref input, 10);
        temp1[12] = Unsafe.Add(ref input, 19);
        temp1[13] = Unsafe.Add(ref input, 12);
        temp1[14] = Unsafe.Add(ref input, 17);
        temp1[15] = Unsafe.Add(ref input, 14);
        temp1[16] = Unsafe.Add(ref input, 15);
        temp1[17] = Unsafe.Add(ref input, 16);
        temp1[18] = Unsafe.Add(ref input, 13);
        temp1[19] = Unsafe.Add(ref input, 18);
        temp1[20] = Unsafe.Add(ref input, 11);
        temp1[21] = Unsafe.Add(ref input, 20);
        temp1[22] = Unsafe.Add(ref input, 9);
        temp1[23] = Unsafe.Add(ref input, 22);
        temp1[24] = Unsafe.Add(ref input, 7);
        temp1[25] = Unsafe.Add(ref input, 24);
        temp1[26] = Unsafe.Add(ref input, 5);
        temp1[27] = Unsafe.Add(ref input, 26);
        temp1[28] = Unsafe.Add(ref input, 3);
        temp1[29] = Unsafe.Add(ref input, 28);
        temp1[30] = Unsafe.Add(ref input, 1);
        temp1[31] = Unsafe.Add(ref input, 30);

        // stage 2
        Span<int> cospi = Av1SinusConstants.CosinusPi(cosBit);
        temp0[0] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[1], temp1[0], cospi[63], temp1[1], cosBit);
        temp0[1] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[1], temp1[1], cospi[63], temp1[0], cosBit);
        temp0[2] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[5], temp1[2], cospi[59], temp1[3], cosBit);
        temp0[3] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[5], temp1[3], cospi[59], temp1[2], cosBit);
        temp0[4] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[9], temp1[4], cospi[55], temp1[5], cosBit);
        temp0[5] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[9], temp1[5], cospi[55], temp1[4], cosBit);
        temp0[6] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[13], temp1[6], cospi[51], temp1[7], cosBit);
        temp0[7] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[13], temp1[7], cospi[51], temp1[6], cosBit);
        temp0[8] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[17], temp1[8], cospi[47], temp1[9], cosBit);
        temp0[9] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[17], temp1[9], cospi[47], temp1[8], cosBit);
        temp0[10] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[21], temp1[10], cospi[43], temp1[11], cosBit);
        temp0[11] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[21], temp1[11], cospi[43], temp1[10], cosBit);
        temp0[12] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[25], temp1[12], cospi[39], temp1[13], cosBit);
        temp0[13] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[25], temp1[13], cospi[39], temp1[12], cosBit);
        temp0[14] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[29], temp1[14], cospi[35], temp1[15], cosBit);
        temp0[15] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[29], temp1[15], cospi[35], temp1[14], cosBit);
        temp0[16] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[33], temp1[16], cospi[31], temp1[17], cosBit);
        temp0[17] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[33], temp1[17], cospi[31], temp1[16], cosBit);
        temp0[18] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[37], temp1[18], cospi[27], temp1[19], cosBit);
        temp0[19] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[37], temp1[19], cospi[27], temp1[18], cosBit);
        temp0[20] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[41], temp1[20], cospi[23], temp1[21], cosBit);
        temp0[21] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[41], temp1[21], cospi[23], temp1[20], cosBit);
        temp0[22] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[45], temp1[22], cospi[19], temp1[23], cosBit);
        temp0[23] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[45], temp1[23], cospi[19], temp1[22], cosBit);
        temp0[24] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[49], temp1[24], cospi[15], temp1[25], cosBit);
        temp0[25] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[49], temp1[25], cospi[15], temp1[24], cosBit);
        temp0[26] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[53], temp1[26], cospi[11], temp1[27], cosBit);
        temp0[27] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[53], temp1[27], cospi[11], temp1[26], cosBit);
        temp0[28] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[57], temp1[28], cospi[7], temp1[29], cosBit);
        temp0[29] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[57], temp1[29], cospi[7], temp1[28], cosBit);
        temp0[30] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[61], temp1[30], cospi[3], temp1[31], cosBit);
        temp0[31] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[61], temp1[31], cospi[3], temp1[30], cosBit);

        // stage 3
        temp1[0] = temp0[0] + temp0[16];
        temp1[1] = temp0[1] + temp0[17];
        temp1[2] = temp0[2] + temp0[18];
        temp1[3] = temp0[3] + temp0[19];
        temp1[4] = temp0[4] + temp0[20];
        temp1[5] = temp0[5] + temp0[21];
        temp1[6] = temp0[6] + temp0[22];
        temp1[7] = temp0[7] + temp0[23];
        temp1[8] = temp0[8] + temp0[24];
        temp1[9] = temp0[9] + temp0[25];
        temp1[10] = temp0[10] + temp0[26];
        temp1[11] = temp0[11] + temp0[27];
        temp1[12] = temp0[12] + temp0[28];
        temp1[13] = temp0[13] + temp0[29];
        temp1[14] = temp0[14] + temp0[30];
        temp1[15] = temp0[15] + temp0[31];
        temp1[16] = -temp0[16] + temp0[0];
        temp1[17] = -temp0[17] + temp0[1];
        temp1[18] = -temp0[18] + temp0[2];
        temp1[19] = -temp0[19] + temp0[3];
        temp1[20] = -temp0[20] + temp0[4];
        temp1[21] = -temp0[21] + temp0[5];
        temp1[22] = -temp0[22] + temp0[6];
        temp1[23] = -temp0[23] + temp0[7];
        temp1[24] = -temp0[24] + temp0[8];
        temp1[25] = -temp0[25] + temp0[9];
        temp1[26] = -temp0[26] + temp0[10];
        temp1[27] = -temp0[27] + temp0[11];
        temp1[28] = -temp0[28] + temp0[12];
        temp1[29] = -temp0[29] + temp0[13];
        temp1[30] = -temp0[30] + temp0[14];
        temp1[31] = -temp0[31] + temp0[15];

        // stage 4
        temp0[0] = temp1[0];
        temp0[1] = temp1[1];
        temp0[2] = temp1[2];
        temp0[3] = temp1[3];
        temp0[4] = temp1[4];
        temp0[5] = temp1[5];
        temp0[6] = temp1[6];
        temp0[7] = temp1[7];
        temp0[8] = temp1[8];
        temp0[9] = temp1[9];
        temp0[10] = temp1[10];
        temp0[11] = temp1[11];
        temp0[12] = temp1[12];
        temp0[13] = temp1[13];
        temp0[14] = temp1[14];
        temp0[15] = temp1[15];
        temp0[16] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[4], temp1[16], cospi[60], temp1[17], cosBit);
        temp0[17] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[4], temp1[17], cospi[60], temp1[16], cosBit);
        temp0[18] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[20], temp1[18], cospi[44], temp1[19], cosBit);
        temp0[19] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[20], temp1[19], cospi[44], temp1[18], cosBit);
        temp0[20] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[36], temp1[20], cospi[28], temp1[21], cosBit);
        temp0[21] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[36], temp1[21], cospi[28], temp1[20], cosBit);
        temp0[22] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[52], temp1[22], cospi[12], temp1[23], cosBit);
        temp0[23] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[52], temp1[23], cospi[12], temp1[22], cosBit);
        temp0[24] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[60], temp1[24], cospi[4], temp1[25], cosBit);
        temp0[25] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[60], temp1[25], cospi[4], temp1[24], cosBit);
        temp0[26] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[44], temp1[26], cospi[20], temp1[27], cosBit);
        temp0[27] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[44], temp1[27], cospi[20], temp1[26], cosBit);
        temp0[28] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[28], temp1[28], cospi[36], temp1[29], cosBit);
        temp0[29] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[28], temp1[29], cospi[36], temp1[28], cosBit);
        temp0[30] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[12], temp1[30], cospi[52], temp1[31], cosBit);
        temp0[31] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[12], temp1[31], cospi[52], temp1[30], cosBit);

        // stage 5
        temp1[0] = temp0[0] + temp0[8];
        temp1[1] = temp0[1] + temp0[9];
        temp1[2] = temp0[2] + temp0[10];
        temp1[3] = temp0[3] + temp0[11];
        temp1[4] = temp0[4] + temp0[12];
        temp1[5] = temp0[5] + temp0[13];
        temp1[6] = temp0[6] + temp0[14];
        temp1[7] = temp0[7] + temp0[15];
        temp1[8] = -temp0[8] + temp0[0];
        temp1[9] = -temp0[9] + temp0[1];
        temp1[10] = -temp0[10] + temp0[2];
        temp1[11] = -temp0[11] + temp0[3];
        temp1[12] = -temp0[12] + temp0[4];
        temp1[13] = -temp0[13] + temp0[5];
        temp1[14] = -temp0[14] + temp0[6];
        temp1[15] = -temp0[15] + temp0[7];
        temp1[16] = temp0[16] + temp0[24];
        temp1[17] = temp0[17] + temp0[25];
        temp1[18] = temp0[18] + temp0[26];
        temp1[19] = temp0[19] + temp0[27];
        temp1[20] = temp0[20] + temp0[28];
        temp1[21] = temp0[21] + temp0[29];
        temp1[22] = temp0[22] + temp0[30];
        temp1[23] = temp0[23] + temp0[31];
        temp1[24] = -temp0[24] + temp0[16];
        temp1[25] = -temp0[25] + temp0[17];
        temp1[26] = -temp0[26] + temp0[18];
        temp1[27] = -temp0[27] + temp0[19];
        temp1[28] = -temp0[28] + temp0[20];
        temp1[29] = -temp0[29] + temp0[21];
        temp1[30] = -temp0[30] + temp0[22];
        temp1[31] = -temp0[31] + temp0[23];

        // stage 6
        temp0[0] = temp1[0];
        temp0[1] = temp1[1];
        temp0[2] = temp1[2];
        temp0[3] = temp1[3];
        temp0[4] = temp1[4];
        temp0[5] = temp1[5];
        temp0[6] = temp1[6];
        temp0[7] = temp1[7];
        temp0[8] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[8], temp1[8], cospi[56], temp1[9], cosBit);
        temp0[9] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[8], temp1[9], cospi[56], temp1[8], cosBit);
        temp0[10] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[40], temp1[10], cospi[24], temp1[11], cosBit);
        temp0[11] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[40], temp1[11], cospi[24], temp1[10], cosBit);
        temp0[12] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[56], temp1[12], cospi[8], temp1[13], cosBit);
        temp0[13] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[56], temp1[13], cospi[8], temp1[12], cosBit);
        temp0[14] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[24], temp1[14], cospi[40], temp1[15], cosBit);
        temp0[15] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[24], temp1[15], cospi[40], temp1[14], cosBit);
        temp0[16] = temp1[16];
        temp0[17] = temp1[17];
        temp0[18] = temp1[18];
        temp0[19] = temp1[19];
        temp0[20] = temp1[20];
        temp0[21] = temp1[21];
        temp0[22] = temp1[22];
        temp0[23] = temp1[23];
        temp0[24] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[8], temp1[24], cospi[56], temp1[25], cosBit);
        temp0[25] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[8], temp1[25], cospi[56], temp1[24], cosBit);
        temp0[26] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[40], temp1[26], cospi[24], temp1[27], cosBit);
        temp0[27] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[40], temp1[27], cospi[24], temp1[26], cosBit);
        temp0[28] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[56], temp1[28], cospi[8], temp1[29], cosBit);
        temp0[29] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[56], temp1[29], cospi[8], temp1[28], cosBit);
        temp0[30] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[24], temp1[30], cospi[40], temp1[31], cosBit);
        temp0[31] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[24], temp1[31], cospi[40], temp1[30], cosBit);

        // stage 7
        temp1[0] = temp0[0] + temp0[4];
        temp1[1] = temp0[1] + temp0[5];
        temp1[2] = temp0[2] + temp0[6];
        temp1[3] = temp0[3] + temp0[7];
        temp1[4] = -temp0[4] + temp0[0];
        temp1[5] = -temp0[5] + temp0[1];
        temp1[6] = -temp0[6] + temp0[2];
        temp1[7] = -temp0[7] + temp0[3];
        temp1[8] = temp0[8] + temp0[12];
        temp1[9] = temp0[9] + temp0[13];
        temp1[10] = temp0[10] + temp0[14];
        temp1[11] = temp0[11] + temp0[15];
        temp1[12] = -temp0[12] + temp0[8];
        temp1[13] = -temp0[13] + temp0[9];
        temp1[14] = -temp0[14] + temp0[10];
        temp1[15] = -temp0[15] + temp0[11];
        temp1[16] = temp0[16] + temp0[20];
        temp1[17] = temp0[17] + temp0[21];
        temp1[18] = temp0[18] + temp0[22];
        temp1[19] = temp0[19] + temp0[23];
        temp1[20] = -temp0[20] + temp0[16];
        temp1[21] = -temp0[21] + temp0[17];
        temp1[22] = -temp0[22] + temp0[18];
        temp1[23] = -temp0[23] + temp0[19];
        temp1[24] = temp0[24] + temp0[28];
        temp1[25] = temp0[25] + temp0[29];
        temp1[26] = temp0[26] + temp0[30];
        temp1[27] = temp0[27] + temp0[31];
        temp1[28] = -temp0[28] + temp0[24];
        temp1[29] = -temp0[29] + temp0[25];
        temp1[30] = -temp0[30] + temp0[26];
        temp1[31] = -temp0[31] + temp0[27];

        // stage 8
        temp0[0] = temp1[0];
        temp0[1] = temp1[1];
        temp0[2] = temp1[2];
        temp0[3] = temp1[3];
        temp0[4] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[16], temp1[4], cospi[48], temp1[5], cosBit);
        temp0[5] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[16], temp1[5], cospi[48], temp1[4], cosBit);
        temp0[6] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[48], temp1[6], cospi[16], temp1[7], cosBit);
        temp0[7] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[48], temp1[7], cospi[16], temp1[6], cosBit);
        temp0[8] = temp1[8];
        temp0[9] = temp1[9];
        temp0[10] = temp1[10];
        temp0[11] = temp1[11];
        temp0[12] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[16], temp1[12], cospi[48], temp1[13], cosBit);
        temp0[13] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[16], temp1[13], cospi[48], temp1[12], cosBit);
        temp0[14] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[48], temp1[14], cospi[16], temp1[15], cosBit);
        temp0[15] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[48], temp1[15], cospi[16], temp1[14], cosBit);
        temp0[16] = temp1[16];
        temp0[17] = temp1[17];
        temp0[18] = temp1[18];
        temp0[19] = temp1[19];
        temp0[20] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[16], temp1[20], cospi[48], temp1[21], cosBit);
        temp0[21] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[16], temp1[21], cospi[48], temp1[20], cosBit);
        temp0[22] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[48], temp1[22], cospi[16], temp1[23], cosBit);
        temp0[23] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[48], temp1[23], cospi[16], temp1[22], cosBit);
        temp0[24] = temp1[24];
        temp0[25] = temp1[25];
        temp0[26] = temp1[26];
        temp0[27] = temp1[27];
        temp0[28] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[16], temp1[28], cospi[48], temp1[29], cosBit);
        temp0[29] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[16], temp1[29], cospi[48], temp1[28], cosBit);
        temp0[30] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[48], temp1[30], cospi[16], temp1[31], cosBit);
        temp0[31] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[48], temp1[31], cospi[16], temp1[30], cosBit);

        // stage 9
        temp1[0] = temp0[0] + temp0[2];
        temp1[1] = temp0[1] + temp0[3];
        temp1[2] = -temp0[2] + temp0[0];
        temp1[3] = -temp0[3] + temp0[1];
        temp1[4] = temp0[4] + temp0[6];
        temp1[5] = temp0[5] + temp0[7];
        temp1[6] = -temp0[6] + temp0[4];
        temp1[7] = -temp0[7] + temp0[5];
        temp1[8] = temp0[8] + temp0[10];
        temp1[9] = temp0[9] + temp0[11];
        temp1[10] = -temp0[10] + temp0[8];
        temp1[11] = -temp0[11] + temp0[9];
        temp1[12] = temp0[12] + temp0[14];
        temp1[13] = temp0[13] + temp0[15];
        temp1[14] = -temp0[14] + temp0[12];
        temp1[15] = -temp0[15] + temp0[13];
        temp1[16] = temp0[16] + temp0[18];
        temp1[17] = temp0[17] + temp0[19];
        temp1[18] = -temp0[18] + temp0[16];
        temp1[19] = -temp0[19] + temp0[17];
        temp1[20] = temp0[20] + temp0[22];
        temp1[21] = temp0[21] + temp0[23];
        temp1[22] = -temp0[22] + temp0[20];
        temp1[23] = -temp0[23] + temp0[21];
        temp1[24] = temp0[24] + temp0[26];
        temp1[25] = temp0[25] + temp0[27];
        temp1[26] = -temp0[26] + temp0[24];
        temp1[27] = -temp0[27] + temp0[25];
        temp1[28] = temp0[28] + temp0[30];
        temp1[29] = temp0[29] + temp0[31];
        temp1[30] = -temp0[30] + temp0[28];
        temp1[31] = -temp0[31] + temp0[29];

        // stage 10
        temp0[0] = temp1[0];
        temp0[1] = temp1[1];
        temp0[2] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[2], cospi[32], temp1[3], cosBit);
        temp0[3] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[3], cospi[32], temp1[2], cosBit);
        temp0[4] = temp1[4];
        temp0[5] = temp1[5];
        temp0[6] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[6], cospi[32], temp1[7], cosBit);
        temp0[7] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[7], cospi[32], temp1[6], cosBit);
        temp0[8] = temp1[8];
        temp0[9] = temp1[9];
        temp0[10] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[10], cospi[32], temp1[11], cosBit);
        temp0[11] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[11], cospi[32], temp1[10], cosBit);
        temp0[12] = temp1[12];
        temp0[13] = temp1[13];
        temp0[14] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[14], cospi[32], temp1[15], cosBit);
        temp0[15] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[15], cospi[32], temp1[14], cosBit);
        temp0[16] = temp1[16];
        temp0[17] = temp1[17];
        temp0[18] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[18], cospi[32], temp1[19], cosBit);
        temp0[19] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[19], cospi[32], temp1[18], cosBit);
        temp0[20] = temp1[20];
        temp0[21] = temp1[21];
        temp0[22] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[22], cospi[32], temp1[23], cosBit);
        temp0[23] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[23], cospi[32], temp1[22], cosBit);
        temp0[24] = temp1[24];
        temp0[25] = temp1[25];
        temp0[26] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[26], cospi[32], temp1[27], cosBit);
        temp0[27] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[27], cospi[32], temp1[26], cosBit);
        temp0[28] = temp1[28];
        temp0[29] = temp1[29];
        temp0[30] = Av1Dct4Forward1dTransformer.HalfButterfly(cospi[32], temp1[30], cospi[32], temp1[31], cosBit);
        temp0[31] = Av1Dct4Forward1dTransformer.HalfButterfly(-cospi[32], temp1[31], cospi[32], temp1[30], cosBit);

        // stage 11
        outputRef = temp0[0];
        Unsafe.Add(ref outputRef, 1) = -temp0[16];
        Unsafe.Add(ref outputRef, 2) = temp0[24];
        Unsafe.Add(ref outputRef, 3) = -temp0[8];
        Unsafe.Add(ref outputRef, 4) = temp0[12];
        Unsafe.Add(ref outputRef, 5) = -temp0[28];
        Unsafe.Add(ref outputRef, 6) = temp0[20];
        Unsafe.Add(ref outputRef, 7) = -temp0[4];
        Unsafe.Add(ref outputRef, 8) = temp0[6];
        Unsafe.Add(ref outputRef, 9) = -temp0[22];
        Unsafe.Add(ref outputRef, 10) = temp0[30];
        Unsafe.Add(ref outputRef, 11) = -temp0[14];
        Unsafe.Add(ref outputRef, 12) = temp0[10];
        Unsafe.Add(ref outputRef, 13) = -temp0[26];
        Unsafe.Add(ref outputRef, 14) = temp0[18];
        Unsafe.Add(ref outputRef, 15) = -temp0[2];
        Unsafe.Add(ref outputRef, 16) = temp0[3];
        Unsafe.Add(ref outputRef, 17) = -temp0[19];
        Unsafe.Add(ref outputRef, 18) = temp0[27];
        Unsafe.Add(ref outputRef, 19) = -temp0[11];
        Unsafe.Add(ref outputRef, 20) = temp0[15];
        Unsafe.Add(ref outputRef, 21) = -temp0[31];
        Unsafe.Add(ref outputRef, 22) = temp0[23];
        Unsafe.Add(ref outputRef, 23) = -temp0[7];
        Unsafe.Add(ref outputRef, 24) = temp0[5];
        Unsafe.Add(ref outputRef, 25) = -temp0[21];
        Unsafe.Add(ref outputRef, 26) = temp0[29];
        Unsafe.Add(ref outputRef, 27) = -temp0[13];
        Unsafe.Add(ref outputRef, 28) = temp0[9];
        Unsafe.Add(ref outputRef, 29) = -temp0[25];
        Unsafe.Add(ref outputRef, 30) = temp0[17];
        Unsafe.Add(ref outputRef, 31) = -temp0[1];
    }
}
