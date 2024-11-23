// <copyright file="MainModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Prism.Ioc;
using Rx.Tracker.Navigation;
using Rx.Tracker.UI.Container;

namespace Rx.Tracker.UI.Features.Main;

public class MainModule : ContainerRegistryModule
{
    protected override IContainerRegistry RegisterTypes(IContainerRegistry containerRegistry)
        => containerRegistry.RegisterForNavigation<MainPage>(Routes.Instance.MainNavigation.Name);
}