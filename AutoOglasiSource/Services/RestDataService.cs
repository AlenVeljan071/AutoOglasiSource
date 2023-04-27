using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Responses;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AutoOglasiSource.Services
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient httpClient;
        private readonly string _baseApiUrl;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            httpClient = new HttpClient();
            _baseApiUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5084" : "http://localhost:5084"; /*"http://10.0.2.2:7263" : "https://localhost:7263";*/
            _url = $"{_baseApiUrl}/api";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }


        #region ACCOUNT

        public async Task<LoginModel> Login(string email, string password)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }

            try
            {
                var requestData = new { email, password };
                var requestDataJson = JsonConvert.SerializeObject(requestData);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PostAsync($"{_url}/account/login", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LoginModel>(responseJson);

                    await SecureStorage.Default.SetAsync("oauth_token", responseData.Token);
                    return new LoginModel { Success = true, AplicationUserId = responseData.AplicationUserId, Email = responseData.Email};
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new LoginModel { Success = false, ErrorMessage = errorData.Error, Token = null };

                }

            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new LoginModel { Success = false, ErrorMessage = errorResponse, Token = null };
            }

        }

        public async Task<LoginModel> SignupAsync(SignupRequest signupTrainerRequest)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                var requestDataJson = JsonConvert.SerializeObject(signupTrainerRequest);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PostAsync($"{_url}/account/signupaccount", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LoginModel>(responseJson);

                    return new LoginModel { Success = true, Email = responseData.Email };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new LoginModel { Success = false, ErrorMessage = errorData.Error, Token = null };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new LoginModel { Success = false, ErrorMessage = errorResponse, Token = null };
            }
        }


        public async Task<LoginModel> VerifyUserAsync(string email, string code)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                var requestData = new { email, code };
                var requestDataJson = JsonConvert.SerializeObject(requestData);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PutAsync($"{_url}/account/verifyuser", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LoginModel>(responseJson);
                    return new LoginModel { Success = true, Email = responseData.Email };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new LoginModel { Success = false, ErrorMessage = errorData.Error, Token = null };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new LoginModel { Success = false, ErrorMessage = errorResponse, Token = null };
            }
        }

        public async Task<LoginModel> ForgotPasswordAsync(string email)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                var requestData = new { email };
                var requestDataJson = JsonConvert.SerializeObject(requestData);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PutAsync($"{_url}/account/forgotpassword", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LoginModel>(responseJson);
                    return new LoginModel { Success = true };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new LoginModel { Success = false, ErrorMessage = errorData.Error, Token = null };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new LoginModel { Success = false, ErrorMessage = errorResponse, Token = null };
            }
        }

        public async Task<LoginModel> ResendVerifyCodeAsync(string email)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                var requestData = new { email };
                var requestDataJson = JsonConvert.SerializeObject(requestData);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PutAsync($"{_url}/account/resendsignupuser", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LoginModel>(responseJson);
                    return new LoginModel { Success = true };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new LoginModel { Success = false, ErrorMessage = errorData.Error, Token = null };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new LoginModel { Success = false, ErrorMessage = errorResponse, Token = null };
            }
        }

        public async Task<LoginModel> SetNewPassword(string email, string code, string password)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                var requestData = new { email, code, password };
                var requestDataJson = JsonConvert.SerializeObject(requestData);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PutAsync($"{_url}/account/setnewpassword", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LoginModel>(responseJson);
                    return new LoginModel { Success = true };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new LoginModel { Success = false, ErrorMessage = errorData.Error, Token = null };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new LoginModel { Success = false, ErrorMessage = errorResponse, Token = null };
            }
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {

                var apiResponse = await httpClient.GetAsync($"{_url}/account/{id}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<UserModel>(responseJson);


                    var response = new UserModel()
                    {
                        Id = responseData.Id,
                        FirstName = responseData.FirstName,
                        LastName = responseData.LastName,
                        Email = responseData.Email,
                        Address = responseData.Address,
                        FullName = responseData.FullName,
                        Rating = responseData.Rating,
                        UpCount = responseData.UpCount,
                        DownCount = responseData.DownCount,
                        Phone = responseData.Phone,
                        Avatar = responseData.Avatar

                    };

                    return response;

                }
              
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new UserModel { ErrorMessage = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new UserModel { ErrorMessage = errorResponse };
            }
        }


        #endregion

        #region Advertisement
        public async Task<CreateModel> CreateAdvertisementAsync(AdvertisementRequestModel advRequest)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

                if (oauthToken == null)
                {
                    return new CreateModel { ErrorMessage = "Please login" };
                }
                httpClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", oauthToken);

                var requestDataJson = JsonConvert.SerializeObject(advRequest);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PostAsync($"{_url}/advertisements", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<CreateModel>(responseJson);

                    return new CreateModel { Success = true, Id = responseData.Id };
                }
                else if(apiResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var errorResponse = "Please login.";
                    return new CreateModel { ErrorMessage = errorResponse };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new CreateModel { ErrorMessage = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new CreateModel { ErrorMessage = errorResponse };
            }
        }

        public async Task<AddImageModel> AddPhotoToAdvertisementAsync(AdsImage image)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

                if (oauthToken == null)
                {
                    return new AddImageModel { ErrorMessage = "No token" };
                }
                httpClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", oauthToken);

                var requestDataJson = JsonConvert.SerializeObject(image);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PostAsync($"{_url}/advertisements/image", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<AddImageModel>(responseJson);
                    return new AddImageModel { Success = true, Link = responseData.Link };
                }
                else if (apiResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var errorResponse = "Please login.";
                    return new AddImageModel { ErrorMessage = errorResponse };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new AddImageModel { ErrorMessage = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new AddImageModel { ErrorMessage = errorResponse };
            }
        }
        public async Task<ErrorData> DeletePhotoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

                if (oauthToken == null)
                {
                    return new ErrorData { Error = "Please login" };
                }
                httpClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", oauthToken);

                var apiResponse = await httpClient.DeleteAsync($"{_url}/advertisements/image/{id}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    return new ErrorData { Error = "Deleted" };
                    

                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new ErrorData { Error = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new ErrorData { Error = errorResponse };
            }
        }


        public async Task<CreateModel> UpdateAdvertisementAsync(AdvertisementEditModel advRequest)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

                if (oauthToken == null)
                {
                    return new CreateModel { ErrorMessage = "Please login" };
                }
                httpClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", oauthToken);

                advRequest.Name = "Audi";
                var requestDataJson = JsonConvert.SerializeObject(advRequest);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PutAsync($"{_url}/advertisements", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<CreateModel>(responseJson);

                    return new CreateModel { Success = true};
                }
                else if (apiResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var errorResponse = "Please login.";
                    return new CreateModel { ErrorMessage = errorResponse };
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new CreateModel { ErrorMessage = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new CreateModel { ErrorMessage = errorResponse };
            }
        }

        public async Task<ErrorData> UpdatePriceAsync(int id, decimal price)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

                if (oauthToken == null)
                {
                    return new ErrorData { Error = "Please login" };
                }
                httpClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", oauthToken);
                var request = new {Id = id, Price = price};
                var requestDataJson = JsonConvert.SerializeObject(request);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PutAsync($"{_url}/advertisements/price", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    return new ErrorData { Error = "Updated" };
                  
                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new ErrorData { Error = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new ErrorData { Error = errorResponse };
            }
        }

        public async Task<ErrorData> DeleteAdvertisementAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

                if (oauthToken == null)
                {
                    return new ErrorData { Error = "Please login" };
                }
                httpClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", oauthToken);
             
                var apiResponse = await httpClient.DeleteAsync($"{_url}/advertisements/{id}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    return new ErrorData { Error = "Deleted" };

                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new ErrorData { Error = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new ErrorData { Error = errorResponse };
            }
        }
        public async Task<List<AdvertisementListModel>> GetAdvertisementListAsync(AdvertisementSpecParams specParams)
            {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                if (specParams.PageIndex == null) specParams.PageIndex = 1;
                var apiResponse = await httpClient.GetAsync($"{_url}/advertisements/list?priceFrom={specParams.PriceFrom}&priceTo={specParams.PriceTo}&mileageFrom={specParams.MileageFrom}&mileageTo={specParams.MileageTo}&sort={specParams.Sort}&search={specParams.Search}&pageIndex={specParams.PageIndex}&applicationUserId={specParams.ApplicationUserId}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<AdvertisementListResponse>(responseJson);

                    var response = new List<AdvertisementListModel>();
                    foreach (var item in responseData.Pagination.Data)
                    {
                        var advertisement = new AdvertisementListModel()
                        {
                            Id = item.Id,
                            Year = item.Year,
                            Price = item.Price,
                            Address = item.Address,
                            Name = item.Name,
                            Image = item.Image != null ? item.Image : new ImageResponse { Link = "https://cdn.pixabay.com/photo/2012/04/13/20/37/car-33556_960_720.png" },

                        };
                        response.Add(advertisement);
                    }
                    return response;

                }
                return null;
               
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                return null;
            }

        }

        public async Task<AdvertisementModel> GetAdvertisementByIdAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {

                var apiResponse = await httpClient.GetAsync($"{_url}/advertisements/{id}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<AdvertisementModel>(responseJson);

                   
                        var response = new AdvertisementModel()
                        {
                            Id = responseData.Id,
                            ApplicationUserId = responseData.ApplicationUserId,
                            Year = responseData.Year,
                            Price = responseData.Price,
                            Address = responseData.Address,
                            Name = responseData.Name,
                            Color = responseData.Color,
                            Mileage = responseData.Mileage,
                            Abs = responseData.Abs,
                            Airbag = responseData.Airbag,
                            Alarm = responseData.Alarm,
                            AluminiumRims = responseData.AluminiumRims,
                            CreatedAt = responseData.CreatedAt,
                            ParkAssist = responseData.ParkAssist,
                            UpdatedAt = responseData.UpdatedAt,
                            CCM = responseData.CCM,
                            CentralLock = responseData.CentralLock,
                            CruiseControl = responseData.CruiseControl,
                            Damaged = responseData.Damaged,
                            DigitalClimate = responseData.DigitalClimate,
                            DpfFilter = responseData.DpfFilter,
                            ElectricMirrors = responseData.ElectricMirrors,
                            ElectricWindows = responseData.ElectricWindows,
                            Emission = responseData.Emission,
                            Fuel = responseData.Fuel,
                            Gear = responseData.Gear,
                            HorsePower = responseData.HorsePower,
                            KW = responseData.KW,
                            Navigation = responseData.Navigation,
                            Note = responseData.Note,
                            Registered = responseData.Registered,
                            RemoteUnlocking = responseData.RemoteUnlocking,
                            Seat = responseData.Seat,
                            SeatHeating = responseData.SeatHeating,
                            ServiceBook = responseData.ServiceBook,
                            Shifter = responseData.Shifter,
                            SteeringWheelControls = responseData.SteeringWheelControls,
                            Weight = responseData.Weight,
                            Drive = responseData.Drive,
                            ParkingSensors = responseData.ParkingSensors,
                            Images = responseData.Images.Count != 0 ? responseData.Images : new List<ImageResponse> { new ImageResponse { Link = "https://cdn.pixabay.com/photo/2012/04/13/20/37/car-33556_960_720.png" } }
                        };
                    
                    return response;

                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new AdvertisementModel { ErrorMessage = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new AdvertisementModel { ErrorMessage = errorResponse };
            }

        }

        public async Task<List<VehicleBrand>> GetVehicleBrandCatIdAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {

                var apiResponse = await httpClient.GetAsync($"{_url}/advertisements/vehiclebrand/{id}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<List<VehicleBrand>>(responseJson);

                    return responseData;
                }
                return null;
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                return null;
            }

        }

        public async Task<List<VehicleModel>> GetVehicleModelByBrandIdAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {

                var apiResponse = await httpClient.GetAsync($"{_url}/advertisements/vehiclemodel/{id}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseJson = await apiResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<List<VehicleModel>>(responseJson);

                    return responseData;
                }
                return null;
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                return null;
            }

        }

        #endregion

        #region Rating

        public async Task<ErrorData> RatingAsync(int entityTypeRatingId, int entityTypeId, int thumb)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No Internet access!");
            }
            try
            {
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

                if (oauthToken == null)
                {
                    return new ErrorData { Error = "Please login" };
                }
                httpClient.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", oauthToken);
                var request = new { EntityTypeRatingId = entityTypeRatingId, EntityTypeId = entityTypeId, Thumb = thumb };
                var requestDataJson = JsonConvert.SerializeObject(request);

                var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                var apiResponse = await httpClient.PostAsync($"{_url}/ratingreview/rating", requestContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    return new ErrorData();

                }
                else
                {
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    var errorData = JsonConvert.DeserializeObject<ErrorData>(errorResponse);
                    return new ErrorData { Error = errorData.Error };
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message;
                errorResponse = "The server is under maintenance. Please try again later.";
                return new ErrorData { Error = errorResponse };
            }
        }
        #endregion

    }
}
