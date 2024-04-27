namespace DemoAppOne.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy; 

        public bool IsNotBusy => isBusy;
        
        protected IDemoAppOneDatabase _demoAppOneDatabase;
        protected INavigationService _navigationService;

        public BaseViewModel(IDemoAppOneDatabase demoAppOneDatabase, 
                             INavigationService navigationService)
        {
            _demoAppOneDatabase = demoAppOneDatabase;
            _navigationService = navigationService;
        }
    }
}
