using MP.CrossCutting.Utils.Model;

namespace MP.Core.Entities
{
    public class Visita : Entity
    {
        public decimal CodVisitante { get; set; }
        public DateTime? DataBaixaCredencial { get; set; }
        public string Observacao { get; set; }

    }
}
