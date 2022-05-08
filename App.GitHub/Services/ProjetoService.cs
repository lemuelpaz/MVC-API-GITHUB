using App.GitHub.Services.Interfaces;
using App.GitHub.Services.Models;
using RestSharp;

namespace App.GitHub.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly RestClient _client;

        public ProjetoService()
        {
            var options = new RestClientOptions("https://api.github.com/")
            {
                UserAgent = "AppGitHubZek/v1"
            };

            _client = new RestClient(options)
                .AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json");
        }        

        public async Task<IEnumerable<Projeto>> GetUserRepositoriesAsync(string login)
        {
            var request = new RestRequest("users/{username}/repos", Method.Get)
                .AddParameter("per_page", "6")
                .AddUrlSegment("username", login);
            var response = await _client.ExecuteGetAsync<IEnumerable<Projeto>>(request);
            IEnumerable<Projeto>? repositories = response.Data;

            return repositories;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
