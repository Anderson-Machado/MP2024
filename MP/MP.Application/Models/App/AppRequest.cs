using System.Text.Json.Serialization;

namespace MP.Application.Models.App
{
    /// <summary>
    /// Contem informações do equipamento.
    /// </summary>
    public class AppRequest
    {
        private EType _type;
        private int _sentido;
        private int _areaDe;
        private int _areaPara;

        public EType Type
        {
            get => _type;
            set
            {
                _type = value;
                // Lógica condicional para configurar Sentido, AreaDe e AreaPara
                if (_type == EType.Entrada)
                {
                    _sentido = 1;
                    _areaDe = 1;
                    _areaPara = 2;
                }
                else if (_type == EType.Saida)
                {
                    _sentido = 2;
                    _areaDe = 2;
                    _areaPara = 1;
                }
            }
        }

       
        public int Equipamento { get; set; }
        public decimal Matricula { get; set; }
        [JsonIgnore]
        public int Sentido
        {
            get => _sentido;
            set => _sentido = value;
        }
        [JsonIgnore]
        public int AreaDe
        {
            get => _areaDe;
            set => _areaDe = value;
        }
        [JsonIgnore]
        public int AreaPara
        {
            get => _areaPara;
            set => _areaPara = value;
        }
    }

    public enum EType
    {
        Entrada,
        Saida
    }
}
