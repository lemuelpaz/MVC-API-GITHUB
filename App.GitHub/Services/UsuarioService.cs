using App.GitHub.Services.Interfaces;
using App.GitHub.Services.Models;
using RestSharp;

namespace App.GitHub.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly RestClient _client;

        public UsuarioService()
        {
            var options = new RestClientOptions("https://api.github.com/")
            {
                UserAgent = "AppGitHubZek/v1"
            };

            _client = new RestClient(options)
                .AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json");
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersAsync()
        {
            var request = new RestRequest("users", Method.Get)
                .AddParameter("per_page", "6");
            var response = await _client.ExecuteAsync<IEnumerable<Usuario>>(request);
            IEnumerable<Usuario>? users = response.Data;

            return users;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersAsync(int since)
        {
            var request = new RestRequest("users", Method.Get)
                .AddParameter("per_page", "6")
                .AddParameter("since", since);
            var response = await _client.ExecuteAsync<IEnumerable<Usuario>>(request);
            IEnumerable<Usuario>? users = response.Data;

            return users;
        }

        public async Task<Detalhes> GetUserDetailsAsync(string login)
        {
            var request = new RestRequest("users/{username}", Method.Get)
                .AddUrlSegment("username", login);
            var response = await _client.ExecuteGetAsync<Detalhes>(request);
            Detalhes? user = response.Data;

            return user;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
