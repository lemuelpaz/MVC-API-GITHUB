using App.GitHub.Services.Models;

namespace App.GitHub.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<IEnumerable<Usuario>> GetAllUsersAsync();
        public Task<IEnumerable<Usuario>> GetAllUsersAsync(int since);
        public Task<Detalhes> GetUserDetailsAsync(string login);
    }
}
