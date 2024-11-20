using BUE.Inscriptions.Infrastructure.Interfaces;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class FireBaseRepository : IFireBaseRepository
    {
        protected readonly string _urlFireBasen;
        protected readonly string _firebaseSecret;
        protected readonly IConfiguration _configuration;
        private readonly FirebaseClient _client;

        public FireBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _urlFireBasen = _configuration.GetSection("AppSettings:UrlFireBase").Value;
            _firebaseSecret = _configuration.GetSection("AppSettings:FirebaseSecret").Value;
            _client = new FirebaseClient((IFirebaseConfig)new FirebaseConfig()
            {
                AuthSecret = _firebaseSecret,
                BasePath = _urlFireBasen
            });
        }

        public async Task<string> setValue(string node, string value)
        {
            try
            {
                SetResponse setResponse = await _client.SetAsync<string>(node, value);
                string result = setResponse.Body;
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
