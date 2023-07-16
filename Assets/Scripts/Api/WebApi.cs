using Api.Services;

namespace Api
{
    public class WebApi : Singleton<WebApi>
    {
        public string JwtToken { get; set; }
        public AuthenticationApi AuthenticationAPI;

        protected override void Awake()
        {
            CreateApiServices();
        }

        private void CreateApiServices()
        {
            AuthenticationAPI = new AuthenticationApi();
        }
    }
}