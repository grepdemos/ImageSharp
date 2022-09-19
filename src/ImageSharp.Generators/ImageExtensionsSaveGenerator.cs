﻿// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ImageSharp.Generators;

[Generator(LanguageNames.CSharp)]
internal class ImageExtensionsSaveGenerator : IIncrementalGenerator
{
    private const string FileHeader = @"// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

// <auto-generated />";

    private static readonly string[] ImageFormats =
    {
        "Bmp",
        "Gif",
        "Jpeg",
        "Pbm",
        "Png",
        "Tga",
        "Webp",
        "Tiff"
    };

    public void Initialize(IncrementalGeneratorInitializationContext context) => context.RegisterPostInitializationOutput(GenerateExtensionsMethods);

    private static void GenerateExtensionsMethods(IncrementalGeneratorPostInitializationContext ctx)
    {
        StringBuilder stringBuilder = new(FileHeader);
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("using SixLabors.ImageSharp.Advanced;");
        foreach (string format in ImageFormats)
        {
            stringBuilder.Append("using SixLabors.ImageSharp.Formats.").Append(format).AppendLine(";");
        }

        stringBuilder.AppendLine();
        stringBuilder.AppendLine("namespace SixLabors.ImageSharp;");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(@"/// <summary>
/// Extension methods for the <see cref=""Image""/> type.
/// </summary>
public static partial class ImageExtensions
{");

        ctx.CancellationToken.ThrowIfCancellationRequested();

        foreach (string format in ImageFormats)
        {
            stringBuilder.AppendLine($@"
    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""path"">The file path to save the image to.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the path is null.</exception>
    public static void SaveAs{format}(this Image source, string path) => SaveAs{format}(source, path, null);

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""path"">The file path to save the image to.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the path is null.</exception>
    /// <returns>A <see cref=""Task""/> representing the asynchronous operation.</returns>
    public static Task SaveAs{format}Async(this Image source, string path) => SaveAs{format}Async(source, path, null);

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""path"">The file path to save the image to.</param>
    /// <param name=""cancellationToken"">The token to monitor for cancellation requests.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the path is null.</exception>
    /// <returns>A <see cref=""Task""/> representing the asynchronous operation.</returns>
    public static Task SaveAs{format}Async(this Image source, string path, CancellationToken cancellationToken)
        => SaveAs{format}Async(source, path, null, cancellationToken);

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""path"">The file path to save the image to.</param>
    /// <param name=""encoder"">The encoder to save the image with.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the path is null.</exception>
    public static void SaveAs{format}(this Image source, string path, {format}Encoder encoder) =>
        source.Save(
            path,
            encoder ?? source.GetConfiguration().ImageFormatsManager.FindEncoder({format}Format.Instance));

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""path"">The file path to save the image to.</param>
    /// <param name=""encoder"">The encoder to save the image with.</param>
    /// <param name=""cancellationToken"">The token to monitor for cancellation requests.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the path is null.</exception>
    /// <returns>A <see cref=""Task""/> representing the asynchronous operation.</returns>
    public static Task SaveAs{format}Async(this Image source, string path, {format}Encoder encoder, CancellationToken cancellationToken = default) =>
        source.SaveAsync(
            path,
            encoder ?? source.GetConfiguration().ImageFormatsManager.FindEncoder({format}Format.Instance),
            cancellationToken);

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""stream"">The stream to save the image to.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the stream is null.</exception>
    public static void SaveAs{format}(this Image source, Stream stream)
        => SaveAs{format}(source, stream, null);

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""stream"">The stream to save the image to.</param>
    /// <param name=""cancellationToken"">The token to monitor for cancellation requests.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the stream is null.</exception>
    /// <returns>A <see cref=""Task""/> representing the asynchronous operation.</returns>
    public static Task SaveAs{format}Async(this Image source, Stream stream, CancellationToken cancellationToken = default)
        => SaveAs{format}Async(source, stream, null, cancellationToken);

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""stream"">The stream to save the image to.</param>
    /// <param name=""encoder"">The encoder to save the image with.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the stream is null.</exception>
    public static void SaveAs{format}(this Image source, Stream stream, {format}Encoder encoder)
        => source.Save(
            stream,
            encoder ?? source.GetConfiguration().ImageFormatsManager.FindEncoder({format}Format.Instance));

    /// <summary>
    /// Saves the image to the given stream with the {format} format.
    /// </summary>
    /// <param name=""source"">The image this method extends.</param>
    /// <param name=""stream"">The stream to save the image to.</param>
    /// <param name=""encoder"">The encoder to save the image with.</param>
    /// <param name=""cancellationToken"">The token to monitor for cancellation requests.</param>
    /// <exception cref=""System.ArgumentNullException"">Thrown if the stream is null.</exception>
    /// <returns>A <see cref=""Task""/> representing the asynchronous operation.</returns>
    public static Task SaveAs{format}Async(this Image source, Stream stream, {format}Encoder encoder, CancellationToken cancellationToken = default) =>
        source.SaveAsync(
            stream,
            encoder ?? source.GetConfiguration().ImageFormatsManager.FindEncoder({format}Format.Instance),
            cancellationToken);");
        }

        stringBuilder.Append("}");

        SourceText sourceText = SourceText.From(stringBuilder.ToString(), Encoding.UTF8);
        ctx.AddSource("ImageExtensions.Save.g.cs", sourceText);
    }
}
