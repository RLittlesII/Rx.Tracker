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
        this._navigationService = navigationService;
        this._globalExceptionHandler = coreRegistration.ExceptionHandler;
    }

    public Task<NavigationState> Navigate(Func<Routes, Uri> routes)
        => this._navigationService.NavigateAsync(routes.Invoke(Routes)).ContinueWith(this.HandleFailedNavigationResult);

    public Task<NavigationState> Navigate<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new()
    {
        var invoke = routes.Invoke(new TRoute());
        return this._navigationService.NavigateAsync(invoke).ContinueWith(this.HandleFailedNavigationResult);
    }

    public Task<NavigationState> Back(uint backwards) => this._navigationService.GoBackAsync().ContinueWith(this.HandleFailedNavigationResult);

    public Task<NavigationState> Back<TRoute>(Func<TRoute, Uri> routes)
        where TRoute : new() => throw new NotImplementedException();

    private static readonly Routes Routes = new Routes();

    private NavigationState HandleFailedNavigationResult(Task<INavigationResult> navigationResult)
    {
        switch (navigationResult.Result)
        {
            case { Success: false, Exception: not null } result:
                this._globalExceptionHandler.OnNext(result.Exception);
                return NavigationState.Failed;
            default:
                return navigationResult.Result.Success ? NavigationState.Succeeded : NavigationState.Ethers;
        }
    }

    private readonly INavigationService _navigationService;
    private readonly IObserver<Exception> _globalExceptionHandler;
}