﻿using AutoOglasiSource.Constants;
using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Model.Enums;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AutoOglasiSource.ViewModel
{
    public partial class AddAdvertisementViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<EnumValue> Emmisions { get; }
        public ObservableCollection<EnumValue> Gears { get; }
        public ObservableCollection<EnumValue> Colors { get; }
        public ObservableCollection<EnumValue> Seats { get; }
        public ObservableCollection<int> Years { get; } = new();
        public ObservableCollection<VehicleBrand> VehicleBrands { get; } = new();
        public ObservableCollection<VehicleModel> VehicleModels { get; set; }  = new();
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

            Gears = new ObservableCollection<EnumValue>(DictionaryExtensions.GetValues(Constants.Constants.Gears));
            Emmisions = new ObservableCollection<EnumValue>(DictionaryExtensions.GetValues(Constants.Constants.Emissions));
            Seats = new ObservableCollection<EnumValue>(DictionaryExtensions.GetValues(Constants.Constants.Seats));
            Colors = new ObservableCollection<EnumValue>(DictionaryExtensions.GetValues(Constants.Constants.Colors));

            ColorDiesel = Color.FromRgb(0, 0, 0);
            ColorGasoline = Color.FromRgb(0, 0, 0);
            ColorGas = Color.FromRgb(0, 0, 0);
            ColorHybrid = Color.FromRgb(0, 0, 0);
            ColorElectric = Color.FromRgb(0, 0, 0);

            ColorFront = Color.FromRgb(0, 0, 0);
            ColorRear = Color.FromRgb(0, 0, 0);
            ColorAll = Color.FromRgb(0, 0, 0);

            ColorNone = Color.FromRgb(0, 0, 0);
            ColorParkingFront = Color.FromRgb(0, 0, 0);
            ColorBack = Color.FromRgb(0, 0, 0);
            ColorBoth = Color.FromRgb(0, 0, 0);

            ColorShifterAuto = Color.FromRgb(0, 0, 0);
            ColorShifterManual = Color.FromRgb(0, 0, 0);
            ColorShifterSemiAuto = Color.FromRgb(0, 0, 0);


            _restDataService = restDataService;
            _connectivity = connectivity;
           
            Years = new ObservableCollection<int>(Year());
        }

        [ObservableProperty]
        AdvertisementRequestModel advertisement = new();
      
        [ObservableProperty]
        AddressEntity addressReq = new();


        private EnumValue _selectedGear;
        public EnumValue SelectedGear
        {
            get { return _selectedGear; }
            set
            {
                _selectedGear = value;
                OnSelectedGearsChanged();
            }
        }
        private EnumValue _selectedShifter;
        public EnumValue SelectedShifter
        {
            get { return _selectedShifter; }
            set
            {
                _selectedShifter = value;
                OnSelectedGearsChanged();
            }
        }
        private EnumValue _selectedEmmission;
        public EnumValue SelectedEmmission
        {
            get { return _selectedEmmission; }
            set
            {
                _selectedEmmission = value;
                OnSelectedEmmisionsChanged();
            }
        }
        private EnumValue _selectedSeat;
        public EnumValue SelectedSeat
        {
            get { return _selectedSeat; }
            set
            {
                _selectedSeat = value;
                OnSelectedSeatsChanged();
            }
        }
        private EnumValue _selectedColor;
        public EnumValue SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                OnSelectedColorsChanged();
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnSelectedCategoryChanged();
            }
        }

        private VehicleBrand _selectedBrand;
        public VehicleBrand SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                _selectedBrand = value;
                OnSelectedBrandChanged();
            }
        }
        private VehicleModel _selectedModel;
        public VehicleModel SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                OnSelectedModelChanged();
            }
        }

        [ObservableProperty]
        int id;

        [ObservableProperty]
        Category category = new();

        [ObservableProperty]
        VehicleBrand vehicleBrand = new();

        [ObservableProperty]
        VehicleModel vehicleModel = new();

        [ObservableProperty]
        bool _isSelectedCategory = new();


        [ObservableProperty]
        bool _isSelectedShifterAuto = new();
        [ObservableProperty]
        bool _isSelectedShifterManual = new();
        [ObservableProperty]
        bool _isSelectedShifterSemiAuto = new();

        [ObservableProperty]
        bool _isSelectedFront = new();
        [ObservableProperty]
        bool _isSelectedRear = new();
        [ObservableProperty]
        bool _isSelectedAll = new();

        [ObservableProperty]
        bool _isSelectedDiesel = new();
        [ObservableProperty]
        bool _isSelectedGasoline = new();
        [ObservableProperty]
        bool _isSelectedGas = new();
        [ObservableProperty]
        bool _isSelectedHybrid = new();
        [ObservableProperty]
        bool _isSelectedElectric = new();

        [ObservableProperty]
        bool _isSelectedParkingFront = new();
        [ObservableProperty]
        bool _isSelectedParkingBack = new();
        [ObservableProperty]
        bool _isSelectedParkingNone = new();
        [ObservableProperty]
        bool _isSelectedParkingBoth = new();

        [ObservableProperty]
        Color colorShifterAuto;
        [ObservableProperty]
        Color colorShifterManual = new();
        [ObservableProperty]
        Color colorShifterSemiAuto = new();

        [ObservableProperty]
        Color colorDiesel = new();
        [ObservableProperty]
        Color colorGasoline = new();
        [ObservableProperty]
        Color colorGas = new();
        [ObservableProperty]
        Color colorHybrid = new();
        [ObservableProperty]
        Color colorElectric = new();

        [ObservableProperty]
        Color colorFront;
        [ObservableProperty]
        Color colorRear = new();
        [ObservableProperty]
        Color colorAll = new();

        [ObservableProperty]
        Color colorNone = new();
        [ObservableProperty]
        Color colorParkingFront = new();
        [ObservableProperty]
        Color colorBack = new();
        [ObservableProperty]
        Color colorBoth = new();

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        private void ShifterSelection(string selection)
        {
            if (selection == Shifter.Automatic.ToString())
            {
                ColorShifterAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterAuto = !IsSelectedShifterAuto;
                Advertisement.Shifter = IsSelectedShifterAuto ? (int)Shifter.Automatic : (int)Shifter.Manual;
                if (Advertisement.Shifter == (int)Shifter.Automatic) ColorShifterAuto = Color.FromRgb(169, 169, 169);
                IsSelectedShifterManual = false;
                ColorShifterManual = Color.FromRgb(0, 0, 0);
                IsSelectedShifterSemiAuto = false;
                ColorShifterSemiAuto = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Shifter.Manual.ToString())
            {
                ColorShifterAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterManual = !IsSelectedShifterManual;
                Advertisement.Shifter = IsSelectedShifterManual ? (int)Shifter.Manual : (int)Shifter.Manual;
                if (Advertisement.Shifter == (int)Shifter.Manual) ColorShifterManual = Color.FromRgb(169, 169, 169);
                IsSelectedShifterAuto = false;
                ColorShifterAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterSemiAuto = false;
                ColorShifterSemiAuto = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Shifter.SemiAuto.ToString())
            {
                ColorShifterSemiAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterSemiAuto = !IsSelectedShifterSemiAuto;
                Advertisement.Shifter = IsSelectedShifterSemiAuto ? (int)Shifter.SemiAuto : (int)Shifter.Manual;
                if (Advertisement.Shifter == (int)Shifter.SemiAuto) ColorShifterSemiAuto = Color.FromRgb(169, 169, 169);
                IsSelectedShifterManual = false;
                ColorShifterManual = Color.FromRgb(0, 0, 0);
                IsSelectedShifterAuto = false;
                ColorShifterAuto = Color.FromRgb(0, 0, 0);
            }
        }


        [RelayCommand]
        private void DriveSelection(string selection)
        {
            if (selection == Drive.Front.ToString())
            {
                ColorFront = Color.FromRgb(0, 0, 0);
                IsSelectedFront = !IsSelectedFront;
                var drive = IsSelectedFront ? Drive.Front : Drive.Front;
                Advertisement.Drive = drive;
                if (drive == Drive.Front) ColorFront = Color.FromRgb(169, 169, 169);
                IsSelectedRear = false;
                ColorRear = Color.FromRgb(0, 0, 0);
                IsSelectedAll = false;
                ColorAll = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Drive.Rear.ToString())
            {
                ColorRear = Color.FromRgb(0, 0, 0);
                IsSelectedRear = !IsSelectedRear;
                Advertisement.Drive = IsSelectedRear ? Drive.Rear : Drive.Front;
                if (Advertisement.Drive == Drive.Rear) ColorRear = Color.FromRgb(169, 169, 169);
                IsSelectedFront = false;
                ColorFront = Color.FromRgb(0, 0, 0);
                IsSelectedAll = false;
                ColorAll = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Drive.All.ToString())
            {
                ColorAll = Color.FromRgb(0, 0, 0);
                IsSelectedAll = !IsSelectedAll;
                Advertisement.Drive = IsSelectedAll ? Drive.All : Drive.Front;
                if (Advertisement.Drive == Drive.All) ColorAll = Color.FromRgb(169, 169, 169);
                IsSelectedFront = false;
                ColorFront = Color.FromRgb(0, 0, 0);
                IsSelectedRear = false;
                ColorRear = Color.FromRgb(0, 0, 0);
            }
        }
       
       
        [RelayCommand]
        private void FuelSelection(string selection)
        {
            if(selection == Fuel.Diesel.ToString())
            {
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedDiesel = !IsSelectedDiesel;
                Advertisement.Fuel = IsSelectedDiesel ? Fuel.Diesel : Fuel.Diesel;
                if (Advertisement.Fuel == Fuel.Diesel) ColorDiesel = Color.FromRgb(169, 169, 169);
                IsSelectedGasoline = false;
                ColorGasoline = Color.FromRgb(0, 0, 0);
                IsSelectedGas = false;
                ColorGas = Color.FromRgb(0, 0, 0);
                IsSelectedHybrid = false;
                ColorHybrid = Color.FromRgb(0, 0, 0);
                IsSelectedElectric = false;
                ColorElectric= Color.FromRgb(0, 0, 0);
            }
            else if (selection == Fuel.Gasoline.ToString())
            {
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedGasoline = !IsSelectedGasoline;
                Advertisement.Fuel = IsSelectedGasoline ? Fuel.Gasoline : Fuel.Diesel;
                if (Advertisement.Fuel == Fuel.Gasoline) ColorGasoline = Color.FromRgb(169, 169, 169);
                IsSelectedDiesel = false;
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedGas = false;
                ColorGas = Color.FromRgb(0, 0, 0);
                IsSelectedHybrid = false;
                ColorHybrid = Color.FromRgb(0, 0, 0);
                IsSelectedElectric = false;
                ColorElectric = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Fuel.Gas.ToString())
            {
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedGas = !IsSelectedGas;
                Advertisement.Fuel = IsSelectedGas ? Fuel.Gas : Fuel.Diesel;
                if (Advertisement.Fuel == Fuel.Gas) ColorGas = Color.FromRgb(169, 169, 169);
                IsSelectedDiesel = false;
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedGasoline = false;
                ColorGasoline = Color.FromRgb(0, 0, 0);
                IsSelectedHybrid = false;
                ColorHybrid = Color.FromRgb(0, 0, 0);
                IsSelectedElectric = false;
                ColorElectric = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Fuel.Hybrid.ToString())
            {
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedHybrid = !IsSelectedHybrid;
                Advertisement.Fuel = IsSelectedHybrid ? Fuel.Hybrid : Fuel.Diesel;
                if (Advertisement.Fuel == Fuel.Hybrid) ColorHybrid = Color.FromRgb(169, 169, 169);
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedGasoline = false;
                ColorGasoline = Color.FromRgb(0, 0, 0);
                IsSelectedGas = false;
                ColorGas = Color.FromRgb(0, 0, 0);
                IsSelectedElectric = false;
                ColorElectric = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Fuel.Electric.ToString())
            {
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedElectric = !IsSelectedElectric;
                Advertisement.Fuel = IsSelectedElectric ? Fuel.Electric : Fuel.Diesel;
                if (Advertisement.Fuel == Fuel.Electric) ColorElectric = Color.FromRgb(169, 169, 169);
                IsSelectedDiesel = false;
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedGasoline = false;
                ColorGasoline = Color.FromRgb(0, 0, 0);
                IsSelectedGas = false;
                ColorGas = Color.FromRgb(0, 0, 0);
                IsSelectedHybrid = false;
                ColorHybrid = Color.FromRgb(0, 0, 0);
            }

        }

        [RelayCommand]
        private void ParkingSensorsSelection(string selection)
        {
            if (selection == ParkingSensors.Front.ToString())
            {
                ColorParkingFront = Color.FromRgb(0, 0, 0);
                IsSelectedParkingFront = !IsSelectedParkingFront;
                Advertisement.ParkingSensors = IsSelectedParkingFront ? ParkingSensors.Front : ParkingSensors.None;
                if (Advertisement.ParkingSensors == ParkingSensors.Front) ColorParkingFront = Color.FromRgb(169, 169, 169);
                IsSelectedParkingBack = false;
                ColorBack = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBoth = false;
                ColorBoth = Color.FromRgb(0, 0, 0);
                IsSelectedParkingNone = false;
                ColorNone = Color.FromRgb(0, 0, 0);
            }
            else if (selection == ParkingSensors.Back.ToString())
            {
                ColorBack = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBack = !IsSelectedParkingBack;
                Advertisement.ParkingSensors = IsSelectedParkingBack ? ParkingSensors.Back : ParkingSensors.None;
                if (Advertisement.ParkingSensors == ParkingSensors.Back) ColorBack = Color.FromRgb(169, 169, 169);
                IsSelectedParkingFront = false;
                ColorParkingFront = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBoth = false;
                ColorBoth = Color.FromRgb(0, 0, 0);
                IsSelectedParkingNone = false;
                ColorNone = Color.FromRgb(0, 0, 0);
            }
            else if (selection == ParkingSensors.None.ToString())
            {
                ColorNone = Color.FromRgb(0, 0, 0);
                IsSelectedParkingNone = !IsSelectedParkingNone;
                Advertisement.ParkingSensors = IsSelectedParkingNone ? ParkingSensors.None : ParkingSensors.None;
                if (Advertisement.ParkingSensors == ParkingSensors.None) ColorNone = Color.FromRgb(169, 169, 169);
                IsSelectedParkingBack = false;
                ColorBack = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBoth = false;
                ColorBoth = Color.FromRgb(0, 0, 0);
                IsSelectedParkingFront = false;
                ColorParkingFront = Color.FromRgb(0, 0, 0);
            }
            else if (selection == ParkingSensors.Both.ToString())
            {
                ColorBoth = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBoth = !IsSelectedParkingBoth;
                Advertisement.ParkingSensors = IsSelectedParkingBoth ? ParkingSensors.Both : ParkingSensors.None;
                if (Advertisement.ParkingSensors == ParkingSensors.Both) ColorBoth = Color.FromRgb(169, 169, 169);
                IsSelectedParkingFront = false;
                ColorParkingFront = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBack = false;
                ColorBack = Color.FromRgb(0, 0, 0);
                IsSelectedParkingNone = false;
                ColorNone = Color.FromRgb(0, 0, 0);
            }

        }




        async Task GetVehicleBrands(int id)
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
                
                var response = await _restDataService.GetVehicleBrandCatIdAsync(id);
              
                if (VehicleModels.Count != 0)
                    VehicleModels.Clear();

                foreach (var item in response)
                    VehicleBrands.Add(item);

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

                var response = await _restDataService.GetVehicleModelByBrandIdAsync(id);

                foreach (var item in response)
                    VehicleModels.Add(item);

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
        private async void OnSelectedCategoryChanged()
        {

            if (SelectedCategory != null)
            {
                VehicleBrands.Clear();
                int categoryId = SelectedCategory.Id;
                await GetVehicleBrands(categoryId);
                Advertisement.SubCategoryId = categoryId;
            }
        }

        private async void OnSelectedBrandChanged()
        {

            if (SelectedBrand != null)
            {
                VehicleModels.Clear();
                int brand = SelectedBrand.Id;
                await GetVehicleModels(brand);
                Advertisement.VehicleBrandId = brand;
            }
        }

        private void OnSelectedModelChanged()
        {

            if (SelectedModel != null)
            {
                int modelId = SelectedModel.Id;
                Advertisement.VehicleModelId = modelId;
            }
        }
        private  void OnSelectedGearsChanged()
        {

            if (SelectedGear != null)
            {

                var gear = SelectedGear.Id;
                Advertisement.Gear = gear;
            }
        }

        private  void OnSelectedEmmisionsChanged()
        {

            if (SelectedEmmission != null)
            {

                var emmision = SelectedEmmission.Id;
                Advertisement.Emission = emmision;
            }
        }
        private  void OnSelectedSeatsChanged()
        {

            if (SelectedSeat != null)
            {

                var seats = SelectedSeat.Id;
                Advertisement.Seat = seats;
            }
        }
        private void OnSelectedColorsChanged()
        {

            if (SelectedColor != null)
            {

                var color = SelectedColor.Id;
                Advertisement.Color = color;    
            }
        }

        [RelayCommand]
        public async Task CreateAdvertisement()
        {
            if (IsBusy)
                return;


            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayLoginMessage("No connectivity!,Please check internet and try again.");
                    return;
                }

                IsBusy = true;
                Advertisement = Advertisement;
                Advertisement.Address = AddressReq;
               
                var response = await _restDataService.CreateAdvertisementAsync(Advertisement);
                if (response.Success)
                {
                    Id= response.Id;
                  //  await Shell.Current.GoToAsync(nameof(AddPhotosPage));
                    await Shell.Current.GoToAsync(nameof(AddPhotosPage), true, new Dictionary<string, object>
                {
                  { "Id", Id}
                });
                }
                else
                {
                    await DisplayLoginMessage(response.ErrorMessage);
                }
            }

            catch (Exception ex)
            {
                await DisplayLoginMessage(ex.Message);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }


        
        public List<int> Year()
        {
          var list = new List<int>();
          var last =  DateTime.UtcNow.AddYears(1).Year;
          var first = DateTime.UtcNow.AddYears(-100).Year;
            for (int i = first; i < last; i++)
            {
                list.Add(i);
            }
            return list.OrderByDescending(x => x).ToList();
        }

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Add ads result", message, "OK");
           
        }
    }
}

