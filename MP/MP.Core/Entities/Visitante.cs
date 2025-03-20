using MP.CrossCutting.Utils.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Core.Entities
{
    //700261
    public class Visitante : Entity
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public decimal VitaNumero { get; set; }
        public decimal VisiNumero { get; set; }
        public string Result { get; set; }
    }
}
