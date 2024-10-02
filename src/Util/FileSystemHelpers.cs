using System.Diagnostics.CodeAnalysis;

namespace Nefarius.Utilities.WixSharp.Util;

/// <summary>
///     Filesystem helpers.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class FileSystemHelpers
{
    /// <summary>
    ///     Creates and returns a new temporary directory.
    /// </summary>
    public static string GetTemporaryDirectory()
    {
        string tempPath = Path.GetTempPath();
        string tempDirectory = Path.Combine(tempPath, Path.GetRandomFileName());
        Directory.CreateDirectory(tempDirectory);
        return tempDirectory;
    }
}