using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Application.Models.LogAcesso
{
    public class LogAcessoModel
    {
        /// <summary>
        /// NU_CREDENCIAL
        /// </summary>
        public decimal Credencial { get; set; }

        /// <summary>
        /// CD_EQUIPAMENTO
        /// </summary>
        public decimal Equipamento { get; set; }
        /// <summary>
        /// DT_REQUISICAO
        /// </summary>
        public DateTime DataRequisicao { get; set; }
        /// <summary>
        /// TP_SENTIDO_CONSULTA
        /// </summary>
        public decimal SendidoConsulta { get; set; }
        /// <summary>
        /// TP_EVENTO
        /// </summary>
        public decimal Evento { get; set; }
        /// <summary>
        /// CD_AREA_ORIGEM
        /// </summary>
        public decimal CodAreaOrigem { get; set; }
        /// <summary>
        /// CD_AREA_DESTINO
        /// </summary>
        public decimal CodAreaDestino { get; set; }
        /// <summary>
        /// NU_FUNCAO
        /// </summary>
        public decimal Funcao { get; set; }
        /// <summary>
        /// CD_GRUPO
        /// </summary>
        public decimal CodGrupo { get; set; }

        /// <summary>
        /// NU_MATRICULA
        /// </summary>
        public decimal Matricula { get; set; }
        /// <summary>
        /// CD_VISITANTE
        /// </summary>
        public decimal CodVisitante { get; set; }
        /// <summary>
        /// NU_DATA_REQUISICAO
        /// </summary>
        public int NuDataRequisicao { get; set; }
        /// <summary>
        /// NU_HORA_REQUISICAO
        /// </summary>
        public int NuHoraRequisicao { get; set; }
        /// <summary>
        /// DT_PERSISTENCIA
        /// </summary>
        public DateTime DataPersistencia { get; set; }
        /// <summary>
        /// DS_EQUIPAMENTO
        /// </summary>
        public string DsEquipamento { get; set; }
        /// <summary>
        /// DS_AREA_ORIGEM
        /// </summary>
        public string DsAreaOrigem { get; set; }
        /// <summary>
        /// DS_AREA_DESTINO
        /// </summary>
        public string DsAreaDestino { get; set; }
        /// <summary>
        /// DS_GRUPO
        /// </summary>
        public string DsGrupo { get; set; }
        /// <summary>
        /// NM_PESSOA
        /// </summary>
        public string Nome { get; set; }
    }
}
