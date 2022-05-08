using App.GitHub.Services.Models;
using App.GitHub.ViewModels;
using AutoMapper;

namespace App.GitHub.AutoMapper
{
    public class Config : Profile
    {
        public Config()
        {
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<Detalhes, DetalhesViewModel>();
            CreateMap<Projeto, ProjetoViewModel>();
        }
    }
}
