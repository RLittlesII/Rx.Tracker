using System;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Prism.Navigation;

namespace Rx.Tracker.UI.Features.Components;

public abstract class ScreenBase<T> : ContentPage, IInitializeAsync
    where T : ViewModelBase
{
    Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

    protected virtual Task Initialize(INavigationParameters parameters) => Task.CompletedTask;

    private async Task InitializeAsync(INavigationParameters parameters)
    {
        await Initialize(parameters);

        using var disposable = ((T)BindingContext).InitializeCommand.Execute(Unit.Default).Subscribe();
    }
}