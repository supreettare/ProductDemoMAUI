namespace DemoAppOne.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateBackAsync()
        {
            if (Shell.Current.Navigation.NavigationStack.Count > 1)
                return Shell.Current.GoToAsync("..");
            else
                return Task.CompletedTask;
        }

        public Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
        {
            if (parameters != null)
                return Shell.Current.GoToAsync(route, parameters);
            else
                return Shell.Current.GoToAsync(route);
        }

        public async Task NavigateToRootAsync()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
