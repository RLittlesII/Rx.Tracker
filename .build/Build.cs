using Nuke.Common;
using Nuke.Common.Tools.DotNet;

partial class Tracker : NukeBuild
{
    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Tracker>(finder => finder.Build);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    Target Clean => definition => definition
       .OnlyWhenStatic(() => IsLocalBuild)
       .Before(Restore)
       .Executes(() => DotNetTasks.DotNetClean())
       .ProceedAfterFailure();

    Target Workload => definition => definition
       .DependsOn(Clean)
       .Before(Restore)
       .Executes(() => DotNetTasks.DotNetWorkloadInstall(configurator => configurator.SetVerbosity(DotNetVerbosity.detailed).AddWorkloadId("maui")));

    Target Restore => definition => definition
       .DependsOn(Clean)
       .Executes(() => DotNetTasks.DotNetRestore(configurator => configurator.EnableForce().EnableNoCache()));

    Target Compile => definition => definition
       .DependsOn(Restore)
       .Executes(() => DotNetTasks.DotNetBuild());

    Target Test => definition => definition
       .DependsOn(Compile)
       .Executes(() => DotNetTasks.DotNetTest());

    Target Build => definition => definition
       .DependsOn(Test)
       .Executes();

    Target Release => definition => definition
       .OnlyWhenStatic(() => Configuration == Configuration.Release)
       .DependsOn(Build)
       .Executes();
}