using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Features.Medicine.ViewModels;
using Rx.Tracker.Mediation;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

// [AutoFixture(typeof(AddMedicineViewModel))]
internal sealed partial class AddMedicineViewModelFixture : ITestFixtureBuilder
{
    public static implicit operator AddMedicineViewModel(AddMedicineViewModelFixture fixture) => fixture.Build();

    public AddMedicineViewModelFixture WithNavigator(Rx.Tracker.Navigation.INavigator navigator) => this.With(ref this._navigator, navigator);

    public AddMedicineViewModelFixture WithCqrs(ICqrs cqrs) => this.With(ref this._cqrs, cqrs);

    public AddMedicineViewModelFixture WithFactory(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory) => this.With(ref this._loggerFactory, loggerFactory);

    private AddMedicineViewModel Build() => new AddMedicineViewModel(this._navigator, this._cqrs, this._loggerFactory);

    private Rx.Tracker.Navigation.INavigator _navigator = Substitute.For<Rx.Tracker.Navigation.INavigator>();
    private ICqrs _cqrs = Substitute.For<ICqrs>();
    private Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory = Substitute.For<Microsoft.Extensions.Logging.ILoggerFactory>();
}