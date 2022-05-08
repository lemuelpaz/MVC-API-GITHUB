using System.ComponentModel;

namespace App.GitHub.ViewModels
{
    public class ProjetoViewModel
    {
        public long? Id { get; set; }

        [DisplayName("Nome")]
        public string? Name { get; set; }

        [DisplayName("Url")]
        public Uri? HtmlUrl { get; set; }
    }
}
