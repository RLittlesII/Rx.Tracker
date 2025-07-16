using System;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveMarbles.Mvvm;
using Rx.Tracker.Navigation;

namespace Rx.Tracker.UI.Navigation;

public class Navigator : INavigator
{
    public Navigator(INavigationService navigationService, ICoreRegistration coreRegistration)
    {
        _navigationService = navigationService;
        _globalExceptionHandler = coreRegistration.ExceptionHandler;
    }

    /// <inheritdoc />
    public Task<NavigationState> Navigate<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new()
    {
        var uri = routes.Invoke(new TRoute());
        return _navigationService.NavigateAsync(uri).ContinueWith(HandleFailedNavigationResult);
    }

    /// <inheritdoc />
    public Task<NavigationState> Navigate<TRoute>(Func<TRoute, Uri> routes, Action<IArguments> arguments)
        where TRoute : new()
    {
        var args = new NavigatorArguments();
        arguments.Invoke(args);
        var uri = routes.Invoke(new TRoute());
        return _navigationService.NavigateAsync(uri, ToParameters(args)).ContinueWith(HandleFailedNavigationResult);
    }

    /// <inheritdoc />
    public Task<NavigationState> Modal<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new()
    {
        var invoke = routes.Invoke(new TRoute());
        return _navigationService.NavigateAsync(invoke, (KnownNavigationParameters.UseModalNavigation, true)).ContinueWith(HandleFailedNavigationResult);
    }

    /// <inheritdoc />
    public Task<NavigationState> Back() => Back(1);

    /// <inheritdoc />
    public Task<NavigationState> Back(uint backwards) => _navigationService.GoBackAsync().ContinueWith(HandleFailedNavigationResult);

    /// <inheritdoc />
    public Task<NavigationState> Back<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new() => throw new NotImplementedException();

    /// <inheritdoc />
    public Task<NavigationState> Dismiss()
        => _navigationService.GoBackAsync().ContinueWith(HandleFailedNavigationResult);

    /// <inheritdoc />
    public Task<NavigationState> Dismiss(Action<IArguments> arguments)
    {
        var args = new NavigatorArguments();
        arguments.Invoke(args);
        return _navigationService.GoBackAsync(ToParameters(args)).ContinueWith(HandleFailedNavigationResult);
    }

    private static NavigationParameters ToParameters(IArguments arguments)
    {
        var parameters = new NavigationParameters();
        foreach (var argument in arguments)
        {
            parameters.Add(argument.Key, argument.Value);
        }

        return parameters;
    }

    private NavigationState HandleFailedNavigationResult(Task<INavigationResult> navigationResult)
    {
        switch (navigationResult.Result)
        {
            case { Success: false, Exception: not null } result:
                _globalExceptionHandler.OnNext(result.Exception);
                return NavigationState.Failed;
            default:
                return navigationResult.Result.Success ? NavigationState.Succeeded : NavigationState.Ethers;
        }
    }

    private readonly INavigationService _navigationService;
    private readonly IObserver<Exception> _globalExceptionHandler;
}