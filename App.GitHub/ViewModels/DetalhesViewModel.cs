namespace App.GitHub.ViewModels
{
    public class DetalhesViewModel : UsuarioViewModel
    {
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<ProjetoViewModel>? Repositories { get; set; }
    }
}
