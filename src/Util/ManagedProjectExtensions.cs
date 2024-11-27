using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using CliWrap;

using Nefarius.Utilities.DeviceManagement.PnP;

using WixSharp;

namespace Nefarius.Utilities.WixSharp.Util;

/// <summary>
///     Methods extending <see cref="ManagedProject"/>.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class ManagedProjectExtensions
{
    /// <summary>
    ///     Adds Nefarius.Utilities.WixSharp and its dependencies to embedded assemblies.
    /// </summary>
    public static ManagedProject EmbedExtensions(this ManagedProject project)
    {
        project.DefaultRefAssemblies.Add(typeof(WixExt).Assembly.Location);

        return project;
    }
    
    /// <summary>
    ///     Adds CliWrap and its dependencies to embedded assemblies.
    /// </summary>
    public static ManagedProject EmbedCliWrap(this ManagedProject project)
    {
        project.DefaultRefAssemblies.Add(typeof(Cli).Assembly.Location);
        project.DefaultRefAssemblies.Add(typeof(ValueTask).Assembly.Location);
        project.DefaultRefAssemblies.Add(typeof(IAsyncDisposable).Assembly.Location);
        project.DefaultRefAssemblies.Add(typeof(Unsafe).Assembly.Location);
        project.DefaultRefAssemblies.Add(typeof(BuffersExtensions).Assembly.Location);
        project.DefaultRefAssemblies.Add(typeof(ArrayPool<>).Assembly.Location);

        return project;
    }

    /// <summary>
    ///     Adds Nefarius.Utilities.DeviceManagement and its dependencies to embedded assemblies.
    /// </summary>
    public static ManagedProject EmbedDeviceManagement(this ManagedProject project)
    {
        project.DefaultRefAssemblies.Add(typeof(Devcon).Assembly.Location);

        return project;
    }
}