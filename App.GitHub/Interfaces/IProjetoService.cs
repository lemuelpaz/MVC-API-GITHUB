using App.GitHub.Services.Models;

namespace App.GitHub.Services.Interfaces
{
    public interface IProjetoService
    {
        public Task<IEnumerable<Projeto>> GetUserRepositoriesAsync(string login);
    }
}
