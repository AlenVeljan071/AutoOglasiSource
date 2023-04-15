using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace AutoOglasiSource.ViewModel
{
    public partial class AddAdvertisementViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<VehicleBrand> VehicleBrands { get; } = new();
        public ObservableCollection<VehicleModel> VehicleModels { get; } = new();
        IRestDataService _restDataService;
        IConnectivity _connectivity;
        public AddAdvertisementViewModel(IRestDataService restDataService = null, IConnectivity connectivity = null)
        {
            Categories = new ObservableCollection<Category>
            {
            new Category { Id = 1, Name = "Car" },
            new Category { Id = 2, Name = "Motorcycle" },
            new Category { Id = 3, Name = "Cargo" }
            };
            ColorFront = Color.FromHex("#D3D3D3");
            ColorRear = Color.FromHex("#D3D3D3");
            ColorAll = Color.FromHex("#D3D3D3");
            _restDataService = restDataService;
            _connectivity = connectivity;
            Task.Run(async () => await GetVehicleModels(1));
        }

        [ObservableProperty]
        Category category = new();

        [ObservableProperty]
        VehicleBrand vehicleBrand = new();

        [ObservableProperty]
        VehicleModel vehicleModel = new();

        [ObservableProperty]
        bool _isSelectedFront = new();
        [ObservableProperty]
        bool _isSelectedRear = new();
        [ObservableProperty]
        bool _isSelectedAll = new();

        [ObservableProperty]
        Drive drive = new();

        [ObservableProperty]
        Color colorFront;
        [ObservableProperty]
        Color colorRear = new();
        [ObservableProperty]
        Color colorAll = new();

        [ObservableProperty]
        bool isRefreshing;


        [RelayCommand]
        private void FrontSelection()
        {
            ColorFront = Color.FromHex("#D3D3D3");
            IsSelectedFront  = !IsSelectedFront;
            Drive = IsSelectedFront ? Drive.Front : Drive.Rear;
            if (Drive == Drive.Front) ColorFront = Color.FromHex("#A9A9A9");
            IsSelectedRear = false;
            ColorRear = Color.FromHex("#D3D3D3");
            IsSelectedAll = false;
            ColorAll = Color.FromHex("#D3D3D3");
            

        }
        [RelayCommand]
        private void RearSelection()
        {
            ColorRear = Color.FromHex("#D3D3D3");
            IsSelectedRear = !IsSelectedRear;
            Drive = IsSelectedRear ? Drive.Rear : Drive.Front;
            if (Drive == Drive.Rear) ColorRear = Color.FromHex("#A9A9A9");
            IsSelectedFront = false;
            ColorFront = Color.FromHex("#D3D3D3");
            IsSelectedAll = false;
            ColorAll = Color.FromHex("#D3D3D3");
        }
        [RelayCommand]
        private void AllSelection()
        {
            ColorAll = Color.FromHex("#D3D3D3");
            IsSelectedAll = !IsSelectedAll;
            Drive = IsSelectedAll ? Drive.All : Drive.Front;
            if (Drive == Drive.All) ColorAll = Color.FromHex("#A9A9A9");
            IsSelectedFront = false;
            ColorFront = Color.FromHex("#D3D3D3");
            IsSelectedRear = false;
            ColorRear = Color.FromHex("#D3D3D3");
        }

        [RelayCommand]
        async Task GetVehicleModels(int id)
        {
            if (IsBusy)
                return;

            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                var categoryId = Category.Id;
                var response = await _restDataService.GetVehicleBrandAndModelByCatIdAsync(categoryId);
              
                if (VehicleModels.Count != 0)
                    VehicleModels.Clear();

                foreach (var item in response)
                {
                    VehicleBrands.Add(item);
                    foreach (var model in item.Models)
                    {
                        VehicleModels.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get advertisements: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}

