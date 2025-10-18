namespace Event_Management.Services
{
    public class GoogleMapsService
    {
        private readonly string _apiKey;

        public GoogleMapsService(IConfiguration configuration)
        {
            _apiKey = configuration["GoogleMaps:ApiKey"];
        }

        public async Task<string> GetLocationDataAsync(string address)
        {
            using var httpClient = new HttpClient();
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={_apiKey}";
            var response = await httpClient.GetStringAsync(url);
            return response;
        }
    }
}
