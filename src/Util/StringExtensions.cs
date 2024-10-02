using System.Diagnostics.CodeAnalysis;

namespace Nefarius.Utilities.WixSharp.Util;

/// <summary>
///     String helper stuff.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class StringExtensions
{
    /// <summary>
    ///     Partial string comparison that allows to be case-insensitive etc.
    /// </summary>
    public static bool Contains(this string? source, string toCheck, StringComparison comp)
    {
        return source?.IndexOf(toCheck, comp) >= 0;
    }
}