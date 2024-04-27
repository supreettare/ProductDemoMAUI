namespace DemoAppOne.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null);
        Task NavigateBackAsync();
        Task NavigateToRootAsync();
    }
}
