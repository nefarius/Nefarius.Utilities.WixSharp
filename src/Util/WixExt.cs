using System.Diagnostics.CodeAnalysis;

using WixSharp;

namespace Nefarius.Utilities.WixSharp.Util;

/// <summary>
///     WixSharp extensions and utilities.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class WixExt
{
    /// <summary>
    ///     Recursively resolves all subdirectories and their containing files.
    /// </summary>
    public static List<Dir> GetSubDirectories(Feature feature, string directory)
    {
        List<Dir> subDirectoryInfosCollection = new();

        foreach (string subDirectory in Directory.GetDirectories(directory))
        {
            string subDirectoryName = subDirectory.Remove(0, subDirectory.LastIndexOf('\\') + 1);
            Dir newDir =
                new(feature, subDirectoryName, new Files(feature, subDirectory + @"\*.*")) { Name = subDirectoryName };
            subDirectoryInfosCollection.Add(newDir);

            // Recursively traverse nested directories
            GetSubDirectories(feature, subDirectory);
        }

        return subDirectoryInfosCollection;
    }
}