using MP.CrossCutting.Utils.Model;

namespace MP.Core.Entities
{
    public class Pessoa : Entity
    {
        public decimal CodSituacaoPessoa { get; set; }
        public decimal Matricula { get; set; }
        public string NomePessoa { get; set; } = string.Empty;

        public DateTime? DatePeriodoFinal { get; set; }
        public virtual SituacaoPessoaMultipla SituacaoPessoa { get; set; }

    }
}
