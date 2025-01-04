using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using System;
using System.Linq.Expressions;

[GitHubActions(
    "pull-request",
    GitHubActionsImage.MacOs12,
    AutoGenerate = true,
    OnPullRequestBranches = ["main"],
    InvokedTargets = [nameof(GitHubPullRequest)]
)]
[GitHubActions(
    "integration",
    GitHubActionsImage.UbuntuLatest,
    AutoGenerate = true,
    OnPushBranches = ["main"],
    InvokedTargets = [nameof(GitHubIntegration)]
)]
partial class Tracker
{
    /// <summary>
    /// Gets github action Pipelines target.
    /// </summary>
    Target GitHubPullRequest => _ => _
       .OnlyWhenStatic(GitHubActionsTasks.IsRunningOnGitHubActions)
       .DependsOn(Clean)
       .DependsOn(Workload)
       .DependsOn(Restore)
       .DependsOn(Build);

    /// <summary>
    /// Gets github action Pipelines target.
    /// </summary>
    Target GitHubIntegration => _ => _
       .OnlyWhenStatic(GitHubActionsTasks.IsRunningOnGitHubActions)
       .DependsOn(Clean)
       .DependsOn(Restore)
       .DependsOn(Build);
}

/// <summary>
/// Base actions build task
/// </summary>
public class GitHubActionsTasks
{
    /// <summary>
    /// Gets a value that determines if the build is running on GitHub Actions.
    /// </summary>
    public static Func<bool> IsRunningOnGitHubActions => () =>
        NukeBuild.Host is GitHubActions || Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

    /// <summary>
    /// Gets a value that determines if the build is not running on GitHub Actions.
    /// </summary>
    public static Expression<Func<bool>> IsNotRunningOnGitHubActions => () =>
        !( NukeBuild.Host is GitHubActions || Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true" );
}