using Nuke.Common;

partial class Tracker
{
    Target iPhoneRelease => definition => definition
       .OnlyWhenStatic(() => Configuration == Configuration.Release)
       .DependsOn(Build)
       .Executes(() => { });
}