# Navigation

The `INavigator` is an attempt at a thin api surface using a custom route provided by the consumer. We explicitly want to return to the application a strongly
typed completion represented in `NavigationState`. We do not want the ViewModel responsibly for handling exceptions from navigation. We just need the ViewModel
to respond to the `NavigationState`.