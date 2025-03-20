namespace MP.Application.Models.Pessoa
{
    public class PessoaModel
    {
        public string CodSituacaoPessoa { get; set; } = string.Empty;
        public decimal Matricula { get; set; }
        public string NomePessoa { get; set; } = string.Empty;
        public bool HasValidAccess { get; set; } = true;
    }
}
