using App.GitHub.Services.Interfaces;
using App.GitHub.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioService _gitHubApiUsers;
        private readonly IProjetoService _gitHubApiRepositories;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public HomeController(IUsuarioService gitHubApiUsers,
                              IProjetoService gitHubApiRepositories,
                              IMemoryCache cache,
                              IMapper mapper)
        {
            _gitHubApiUsers = gitHubApiUsers;
            _gitHubApiRepositories = gitHubApiRepositories;
            _cache = cache;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _mapper.Map<IEnumerable<UsuarioViewModel>>(await _gitHubApiUsers.GetAllUsersAsync());

            if (users == null) return NotFound();

            return View(users);
        }

        [Route("carregar-mais-usuarios/{since:int}")]
        [HttpGet]
        public async Task<IActionResult> LoadMoreUsers(int since)
        {
            var users = await _cache.GetOrCreateAsync(since, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return _mapper.Map<IEnumerable<UsuarioViewModel>>(await _gitHubApiUsers.GetAllUsersAsync(since));
            });

            if (users == null) return NotFound();

            return PartialView("_AllUsers", users);
        }

        [Route("detalhes-do-usuario/{login}")]
        [HttpGet]
        public async Task<IActionResult> UserDetails(string login)
        {
            var cacheKey = login;
            DetalhesViewModel userViewModel;

            if (!_cache.TryGetValue(login, out userViewModel))
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                };

                userViewModel = await GetUserDetails(login, new DetalhesViewModel());
                userViewModel.Repositories = await GetUserRepositories(login);

                _cache.Set(cacheKey, userViewModel, cacheOptions);
            }

            if (userViewModel == null) return NotFound();

            return PartialView("_UserDetails", userViewModel);
        }        

        private async Task<DetalhesViewModel> GetUserDetails(string login, DetalhesViewModel userViewModel)
        {
            userViewModel = _mapper.Map<DetalhesViewModel>(await _gitHubApiUsers.GetUserDetailsAsync(login));
            return userViewModel;
        }

        private async Task<IEnumerable<ProjetoViewModel>> GetUserRepositories(string login)
        {
            var repositories = _mapper.Map<IEnumerable<ProjetoViewModel>>(await _gitHubApiRepositories.GetUserRepositoriesAsync(login));
            return repositories;
        }
    }
}