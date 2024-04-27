namespace DemoAppOne.Model
{
    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private double cost;
        [ObservableProperty]
        private int quantity;
        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private string productImage;
        [PrimaryKey, AutoIncrement]
        public int ProductId { get; set; }
    }
}
