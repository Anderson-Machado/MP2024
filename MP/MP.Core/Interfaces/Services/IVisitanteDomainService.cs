using MP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Core.Interfaces.Services
{
    public interface IVisitanteDomainService
    {
        Task<Visitante> GetVisitanteByMatricula(decimal codVisitante);
    }
}
