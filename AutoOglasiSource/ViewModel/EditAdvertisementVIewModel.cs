using AutoOglasiSource.Constants;
using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Model.Enums;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace AutoOglasiSource.ViewModel
{
    [QueryProperty(nameof(AdvertisementEdit), "AdvertisementEdit")]
    public partial class EditAdvertisementVIewModel : BaseViewModel
    {
        public ObservableCollection<EnumValue> Emmisions { get; }
        public ObservableCollection<EnumValue> Gears { get; }
        public ObservableCollection<EnumValue> Colors { get; }
        public ObservableCollection<EnumValue> Seats { get; }
        public ObservableCollection<int> Years { get; } = new();

        IRestDataService _restDataService;
        IConnectivity _connectivity;
        public EditAdvertisementVIewModel(IRestDataService restDataService = null, IConnectivity connectivity = null)
        {

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

            ColorShifterAuto = AdvertisementEdit.Shifter == (int)Shifter.Automatic ? Color.FromRgb(169, 169, 169) : Color.FromRgb(0, 0, 0);
            IsSelectedShifterAuto = AdvertisementEdit.Shifter == (int)Shifter.Automatic;
            
            ColorShifterManual = AdvertisementEdit.Shifter == (int)Shifter.Manual ? Color.FromRgb(169, 169, 169) : Color.FromRgb(0, 0, 0);
            IsSelectedShifterManual = AdvertisementEdit.Shifter == (int)Shifter.Manual;
          
            ColorShifterSemiAuto = AdvertisementEdit.Shifter == (int)Shifter.SemiAuto ? Color.FromRgb(169, 169, 169) : Color.FromRgb(0, 0, 0);
            IsSelectedShifterSemiAuto = AdvertisementEdit.Shifter == (int)Shifter.SemiAuto;
            
        }

        [ObservableProperty]
        AdvertisementEditModel advertisementEdit = new();

        [ObservableProperty]
        AddressEntity addressReq = new();

        [ObservableProperty]
        EnumValue selectedGear = new();

        [ObservableProperty]
        EnumValue selectedShifter = new();

        [ObservableProperty]
        EnumValue selectedEmmission = new();

        [ObservableProperty]
        EnumValue selectedSeat = new();

        [ObservableProperty]
        EnumValue selectedColor = new();

        [ObservableProperty]
        int id;

        [ObservableProperty]
        bool isSelectedShifterAuto = new();
        [ObservableProperty]
        bool isSelectedShifterManual = new();
        [ObservableProperty]
        bool isSelectedShifterSemiAuto = new();

        [ObservableProperty]
        bool isSelectedFront = new();
        [ObservableProperty]
        bool isSelectedRear = new();
        [ObservableProperty]
        bool isSelectedAll = new();

        [ObservableProperty]
        bool isSelectedDiesel = new();
        [ObservableProperty]
        bool isSelectedGasoline = new();
        [ObservableProperty]
        bool isSelectedGas = new();
        [ObservableProperty]
        bool isSelectedHybrid = new();
        [ObservableProperty]
        bool isSelectedElectric = new();

        [ObservableProperty]
        bool isSelectedParkingFront = new();
        [ObservableProperty]
        bool isSelectedParkingBack = new();
        [ObservableProperty]
        bool isSelectedParkingNone = new();
        [ObservableProperty]
        bool isSelectedParkingBoth = new();

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


        [ObservableProperty]
        AdvertisementModel advertisement = new();


        [RelayCommand]
        private void ShifterSelection(string selection)
        {
            if (selection == Shifter.Automatic.ToString())
            {
                ColorShifterAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterAuto = !IsSelectedShifterAuto;
                AdvertisementEdit.Shifter = IsSelectedShifterAuto ? (int)Shifter.Automatic : (int)Shifter.Automatic;
                if (AdvertisementEdit.Shifter == (int)Shifter.Automatic) ColorShifterAuto = Color.FromRgb(169,169,169);
                IsSelectedShifterManual = false;
                ColorShifterManual = Color.FromRgb(0, 0, 0);
                IsSelectedShifterSemiAuto = false;
                ColorShifterSemiAuto = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Shifter.Manual.ToString())
            {
                ColorShifterAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterManual = !IsSelectedShifterManual;
                AdvertisementEdit.Shifter = IsSelectedShifterManual ? (int)Shifter.Manual : (int)Shifter.Manual;
                if (AdvertisementEdit.Shifter == (int)Shifter.Manual) ColorShifterManual = Color.FromRgb(169, 169, 169);
                IsSelectedShifterAuto = false;
                ColorShifterAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterSemiAuto = false;
                ColorShifterSemiAuto = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Shifter.SemiAuto.ToString())
            {
                ColorShifterSemiAuto = Color.FromRgb(0, 0, 0);
                IsSelectedShifterSemiAuto = !IsSelectedShifterSemiAuto;
                AdvertisementEdit.Shifter = IsSelectedShifterSemiAuto ? (int)Shifter.SemiAuto : (int)Shifter.SemiAuto;
                if (AdvertisementEdit.Shifter == (int)Shifter.SemiAuto) ColorShifterSemiAuto = Color.FromRgb(169, 169, 169);
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
                var drive = IsSelectedFront ? Drive.Front : Drive.Rear;
                AdvertisementEdit.Drive = drive;
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
                AdvertisementEdit.Drive = IsSelectedRear ? Drive.Rear : Drive.Front;
                if (AdvertisementEdit.Drive == Drive.Rear) ColorRear = Color.FromRgb(169, 169, 169);
                IsSelectedFront = false;
                ColorFront = Color.FromRgb(0, 0, 0);
                IsSelectedAll = false;
                ColorAll = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Drive.All.ToString())
            {
                ColorAll = Color.FromRgb(0, 0, 0);
                IsSelectedAll = !IsSelectedAll;
                AdvertisementEdit.Drive = IsSelectedAll ? Drive.All : Drive.Front;
                if (AdvertisementEdit.Drive == Drive.All) ColorAll = Color.FromRgb(169, 169, 169);
                IsSelectedFront = false;
                ColorFront = Color.FromRgb(0, 0, 0);
                IsSelectedRear = false;
                ColorRear = Color.FromRgb(0, 0, 0);
            }
        }


        [RelayCommand]
        private void FuelSelection(string selection)
        {
            if (selection == Fuel.Diesel.ToString())
            {
                ColorDiesel = Color.FromRgb(0, 0, 0);
                IsSelectedDiesel = !IsSelectedDiesel;
                AdvertisementEdit.Fuel = IsSelectedDiesel ? Fuel.Diesel : Fuel.Diesel;
                if (AdvertisementEdit.Fuel == Fuel.Diesel) ColorDiesel = Color.FromRgb(169, 169, 169);
                IsSelectedGasoline = false;
                ColorGasoline = Color.FromRgb(0, 0, 0);
                IsSelectedGas = false;
                ColorGas = Color.FromRgb(0, 0, 0);
                IsSelectedHybrid = false;
                ColorHybrid = Color.FromRgb(0, 0, 0);
                IsSelectedElectric = false;
                ColorElectric = Color.FromRgb(0, 0, 0);
            }
            else if (selection == Fuel.Gasoline.ToString())
            {
                ColorDiesel = Color.FromRgb(173, 216, 230);
                IsSelectedGasoline = !IsSelectedGasoline;
                AdvertisementEdit.Fuel = IsSelectedGasoline ? Fuel.Gasoline : Fuel.Diesel;
                if (AdvertisementEdit.Fuel == Fuel.Gasoline) ColorGasoline = Color.FromRgb(169, 169, 169);
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
                AdvertisementEdit.Fuel = IsSelectedGas ? Fuel.Gas : Fuel.Diesel;
                if (AdvertisementEdit.Fuel == Fuel.Gas) ColorGas = Color.FromRgb(169, 169, 169);
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
                AdvertisementEdit.Fuel = IsSelectedHybrid ? Fuel.Hybrid : Fuel.Diesel;
                if (AdvertisementEdit.Fuel == Fuel.Hybrid) ColorHybrid = Color.FromRgb(169, 169, 169);
                IsSelectedDiesel = false;
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
                AdvertisementEdit.Fuel = IsSelectedElectric ? Fuel.Electric : Fuel.Diesel;
                if (AdvertisementEdit.Fuel == Fuel.Electric) ColorElectric = Color.FromRgb(169, 169, 169);
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
            if (selection == ParkingSensors.None.ToString())
            {
                ColorNone = Color.FromRgb(0, 0, 0);
                IsSelectedParkingNone = !IsSelectedParkingNone;
                AdvertisementEdit.ParkingSensors = IsSelectedParkingNone ? ParkingSensors.None : ParkingSensors.None;
                if (AdvertisementEdit.ParkingSensors == ParkingSensors.None) ColorNone = Color.FromRgb(169, 169, 169);
                IsSelectedParkingBack = false;
                ColorBack = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBoth = false;
                ColorBoth = Color.FromRgb(0, 0, 0);
                IsSelectedParkingFront = false;
                ColorParkingFront = Color.FromRgb(0, 0, 0);
            }
            else if (selection == ParkingSensors.Front.ToString())
            {
                ColorParkingFront = Color.FromRgb(0, 0, 0);
                IsSelectedParkingFront = !IsSelectedParkingFront;
                AdvertisementEdit.ParkingSensors = IsSelectedFront ? ParkingSensors.Front : ParkingSensors.None;
                if (AdvertisementEdit.ParkingSensors == ParkingSensors.Front) ColorParkingFront = Color.FromRgb(169, 169, 169);
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
                AdvertisementEdit.ParkingSensors = IsSelectedParkingBack ? ParkingSensors.Back : ParkingSensors.None;
                if (AdvertisementEdit.ParkingSensors == ParkingSensors.Back) ColorBack = Color.FromRgb(169, 169, 169);
                IsSelectedParkingFront = false;
                ColorParkingFront = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBoth = false;
                ColorBoth = Color.FromRgb(0, 0, 0);
                IsSelectedParkingNone = false;
                ColorNone = Color.FromRgb(0, 0, 0);
            }
            else if (selection == ParkingSensors.Both.ToString())
            {
                ColorBoth = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBoth = !IsSelectedParkingBoth;
                AdvertisementEdit.ParkingSensors = IsSelectedParkingBoth ? ParkingSensors.Both : ParkingSensors.Both;
                if (AdvertisementEdit.ParkingSensors == ParkingSensors.Both) ColorBoth = Color.FromRgb(169, 169, 169);
                IsSelectedParkingFront = false;
                ColorParkingFront = Color.FromRgb(0, 0, 0);
                IsSelectedParkingBack = false;
                ColorBack = Color.FromRgb(0, 0, 0);
                IsSelectedParkingNone = false;
                ColorNone = Color.FromRgb(0, 0, 0);
            }

        }

        private void OnSelectedGearsChanged()
        {

            if (SelectedGear != null)
            {

                var gear = SelectedGear.Id;
                AdvertisementEdit.Gear = gear;
            }
        }

        private void OnSelectedEmmisionsChanged()
        {

            if (SelectedEmmission != null)
            {

                var emmision = SelectedEmmission.Id;
                AdvertisementEdit.Emission = emmision;
            }
        }
        private void OnSelectedSeatsChanged()
        {

            if (SelectedSeat != null)
            {

                var seats = SelectedSeat.Id;
                AdvertisementEdit.Seat = seats;
            }
        }
        private void OnSelectedColorsChanged()
        {

            if (SelectedColor != null)
            {

                var color = SelectedColor.Id;
                AdvertisementEdit.Color = color;
            }
        }

        [RelayCommand]
        public async Task EditAdvertisement()
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
               
                AdvertisementEdit.Address = AddressReq;

                var response = await _restDataService.UpdateAdvertisementAsync(AdvertisementEdit);
                if (response.Success)
                {
                    Advertisement = await _restDataService.GetAdvertisementByIdAsync(AdvertisementEdit.Id.Value);

                    await Shell.Current.GoToAsync(nameof(MyAdvertisementPage), true, new Dictionary<string, object>
                    {
                  { "Advertisement", Advertisement}
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
            var last = DateTime.UtcNow.AddYears(1).Year;
            var first = DateTime.UtcNow.AddYears(-100).Year;
            for (int i = first; i < last; i++)
            {
                list.Add(i);
            }
            return list.OrderByDescending(x => x).ToList();
        }
        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Register Attempt Result", message, "OK");

        }
    }
}

