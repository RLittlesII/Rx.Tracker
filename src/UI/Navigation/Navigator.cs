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
        var invoke = routes.Invoke(new TRoute());
        return _navigationService.NavigateAsync(invoke).ContinueWith(HandleFailedNavigationResult);
    }

    /// <inheritdoc />
    public Task<NavigationState> Modal<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new()
    {
        var invoke = routes.Invoke(new TRoute());
        return _navigationService.NavigateAsync(invoke, (KnownNavigationParameters.UseModalNavigation, true)).ContinueWith(HandleFailedNavigationResult);
    }

    public Task<NavigationState> Back(uint backwards) => _navigationService.GoBackAsync().ContinueWith(HandleFailedNavigationResult);

    public Task<NavigationState> Back<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new() => throw new NotImplementedException();

    /// <inheritdoc />
    public Task<NavigationState> Dismiss() =>
        _navigationService.GoBackAsync((KnownNavigationParameters.UseModalNavigation, true)).ContinueWith(HandleFailedNavigationResult);

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