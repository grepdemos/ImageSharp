﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormatUtilities.cs" company="James South">
//   Copyright (c) James South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Utility methods for working with supported image formats.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Imaging.Formats
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using ImageProcessor.Configuration;

    /// <summary>
    /// Utility methods for working with supported image formats.
    /// </summary>
    public static class FormatUtilities
    {
        /// <summary>
        /// Gets the correct <see cref="ISupportedImageFormat"/> from the given stream.
        /// <see cref="http://stackoverflow.com/questions/55869/determine-file-type-of-an-image"/>
        /// </summary>
        /// <param name="stream">
        /// The <see cref="System.IO.Stream"/> to read from.
        /// </param>
        /// <returns>
        /// The <see cref="ISupportedImageFormat"/>.
        /// </returns>
        public static ISupportedImageFormat GetFormat(Stream stream)
        {
            // Reset the position of the stream to ensure we're reading the correct part.
            stream.Position = 0;

            IEnumerable<ISupportedImageFormat> supportedImageFormats =
                ImageProcessorBootstrapper.Instance.SupportedImageFormats;

            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, buffer.Length);

            foreach (ISupportedImageFormat supportedImageFormat in supportedImageFormats)
            {
                byte[][] headers = supportedImageFormat.FileHeaders;

                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (byte[] header in headers)
                {
                    if (header.SequenceEqual(buffer.Take(header.Length)))
                    {
                        stream.Position = 0;

                        // Return a new instance as we want to use instance properties.
                        return Activator.CreateInstance(supportedImageFormat.GetType()) as ISupportedImageFormat;
                    }
                }
            }

            stream.Position = 0;
            return null;
        }

        /// <summary>
        /// Returns a value indicating whether the given image is indexed.
        /// </summary>
        /// <param name="image">
        /// The <see cref="System.Drawing.Image"/> to test.
        /// </param>
        /// <returns>
        /// The true if the image is indexed; otherwise, false.
        /// </returns>
        public static bool IsIndexed(Image image)
        {
            // Test value of flags using bitwise AND.
            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            return (image.PixelFormat & PixelFormat.Indexed) != 0;
        }

        /// <summary>
        /// Returns a value indicating whether the given image is indexed.
        /// </summary>
        /// <param name="image">
        /// The <see cref="System.Drawing.Image"/> to test.
        /// </param>
        /// <returns>
        /// The true if the image is animated; otherwise, false.
        /// </returns>
        public static bool IsAnimated(Image image)
        {
            return ImageAnimator.CanAnimate(image);
        }
    }
}
