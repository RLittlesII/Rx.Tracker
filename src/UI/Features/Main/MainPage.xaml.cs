using System;
using Microsoft.Maui.Accessibility;

namespace Rx.Tracker.UI.Features.Main;

public partial class MainPage
{
    public MainPage() => InitializeComponent();

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _count++;

        CounterBtn.Text = _count == 1 ? $"Clicked {_count} time" : $"Clicked {_count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private int _count;
}